using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Valve.VR;

/// <summary>
/// Pointer used to Interact with UI and the PartPicker
/// </summary>
public class VR_UIPointer : MonoBehaviour
{
    public float mDefaultLength = 5.0f;
    public GameObject mDot;
    public VRInputModule mInputmodule;

    public bool enablePartpicker = false;
    public SteamVR_Input_Sources mTargetSource;
    public SteamVR_Action_Boolean mClickAction;


    private LineRenderer mLinerenderer = null;
    public List<Part> mSelectedParts { get; private set; }
    public GameObject mPartpickerScreen;
    private GameObject PartPickerModel;
    private PartpickerScreen partpicker;

    public void Awake()
    {
        mLinerenderer = GetComponent<LineRenderer>();
        if (mPartpickerScreen != null)
        {
            partpicker = mPartpickerScreen.GetComponent<PartpickerScreen>();
        }
    }

    public void Update()
    {
        PointerEventData data = mInputmodule.GetData();

        // set pointer length to mDefaultLength if no collision
        float targetLength = data.pointerCurrentRaycast.distance == 0 ? mDefaultLength : data.pointerCurrentRaycast.distance;

        RaycastHit hit = CreateRaycast(targetLength);

        Vector3 endPosition = transform.position + (transform.forward * targetLength);

        if (hit.collider != null)
            endPosition = hit.point;

        mDot.transform.position = endPosition;

        mLinerenderer.SetPosition(0, transform.position);
        mLinerenderer.SetPosition(1, endPosition);


        if (enablePartpicker)
        {
            if (mClickAction.GetStateDown(mTargetSource))
            {
                // check if hit part is actually a model part
                if (hit.collider != null && hit.collider.gameObject.transform.parent.GetComponent<ProductAssemblyController>() != null)
                {
                    ProcessPartPicker(hit.collider.gameObject);
                }
            }
        }
    }

    /// <summary>
    /// create RaycastHit using given max length
    /// </summary>
    /// <param name="maxLength">max length</param>
    /// <returns>hit</returns>
    private RaycastHit CreateRaycast(float maxLength)
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);
        Physics.Raycast(ray, out hit, maxLength);

        return hit;
    }

    /// <summary>
    /// processes selection ans deselection of modelparts
    /// </summary>
    /// <param name="partGameObject">clicked part</param>
    private void ProcessPartPicker(GameObject partGameObject)
    {
        Part tmp = Part.Parts[partGameObject.transform.GetSiblingIndex()];

        if (PartPickerModel == null)
        {
            PartPickerModel = partGameObject.transform.parent.gameObject;
        }
        if (mSelectedParts == null)
        {
            mSelectedParts = new List<Part>();
        }
        // if part was already selected, remove from selection
        if (mSelectedParts.Contains(tmp))
        {
            // remove item from selection
            Utils.SetObjectColor(partGameObject.transform, Color.white);

            mSelectedParts.Remove(tmp);
            partpicker.RemoveItemFromList(tmp);
        }
        // otherwise add item to selection
        else
        {
            Utils.SetObjectColor(partGameObject.transform, Color.red);
            mSelectedParts.Add(tmp);
            partpicker.AddNewItemToList(tmp);
        }
    }

    /// <summary>
    /// deselect all selected parts
    /// </summary>
    public void DeselectParts()
    {
        foreach (Part part in mSelectedParts)
        {
            Utils.SetObjectColor(PartPickerModel.transform.GetChild(part.PartID), Color.white);
        }
        mSelectedParts = null;
    }
}
