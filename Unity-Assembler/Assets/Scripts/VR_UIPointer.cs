using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Valve.VR;

public class VR_UIPointer : MonoBehaviour
{
    public float mDefaultLength = 5.0f;
    public GameObject mDot;
    public VRInputModule mInputmodule;

    public bool enablePartpicker = false;
    public SteamVR_Input_Sources mTargetSource;
    public SteamVR_Action_Boolean mClickAction;


    private LineRenderer mLinerenderer = null;
    public List<GameObject> mSelectedParts { get; private set; }
    public GameObject mPartpickerScreen;
    private PartpickerScreen partpicker;

    public void Awake()
    {
        mLinerenderer = GetComponent<LineRenderer>();
        partpicker = mPartpickerScreen.GetComponent<PartpickerScreen>();
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
                if (hit.collider != null && hit.collider.gameObject.transform.root.GetComponent<ProductAssemblyController>() != null)
                {
                    ProcessPartPicker(hit.collider.gameObject);
                }
            }
        }
    }

    private RaycastHit CreateRaycast(float length)
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);
        Physics.Raycast(ray, out hit, mDefaultLength);

        return hit;
    }

    private void ProcessPartPicker(GameObject part)
    {
        if (mSelectedParts == null)
        {
            mSelectedParts = new List<GameObject>();
        }
        if (mSelectedParts.Contains(part))
        {
            // remove item from selection
            SetObjectColor(part.transform, Color.white);

            mSelectedParts.Remove(part);
            partpicker.RemoveItemFromList(part);
        }
        else
        {
            // add item to selection
            SetObjectColor(part.transform, Color.red);
            mSelectedParts.Add(part);
            partpicker.AddNewItemToList(part);
        }
    }

    public void DeselectParts()
    {
        foreach (GameObject part in mSelectedParts)
        {
            SetObjectColor(part.transform, Color.white);
        }
        mSelectedParts = null;
    }

    public static void SetObjectColor(Transform mTransform, Color color)
    {
        if (mTransform.childCount > 0)
        {
            foreach (Transform child in mTransform)
            {
                if (child.gameObject.GetComponent<Renderer>().material != null)
                {
                    child.gameObject.GetComponent<Renderer>().material.color = color;
                }
            }
        }
        else
        {
            if (mTransform.gameObject.gameObject.GetComponent<Renderer>().material != null)
            {
                mTransform.gameObject.GetComponent<Renderer>().material.color = color;
            }
        }
    }

    public static void SetObjectMaterial(Transform mTransform, Material mMaterial)
    {
        if (mTransform.childCount > 0)
        {
            foreach (Transform child in mTransform)
            {
                child.gameObject.GetComponent<Renderer>().material = mMaterial;
            }
        }
        else
        {
            mTransform.gameObject.GetComponent<Renderer>().material = mMaterial;
        }
    }
}
