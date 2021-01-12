using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ProductAssemblyCollider : MonoBehaviour
{
    public SteamVR_Input_Sources mHandType;
    public SteamVR_Action_Boolean mGrabAction;

    private GameObject collidingObject = null;

    private Material placedPartMaterial;

    // Start is called before the first frame update
    void Start()
    {
        placedPartMaterial = (Material) Resources.Load("CycleWhite");
    }

    void Update()
    {
        //if trigger is released, check if object is colliding
        if (mGrabAction.GetLastStateUp(mHandType))
        {
            if (collidingObject != null)
            {
                PlaceObject();
            }
        }
    }

    private void PlaceObject()
    {
        Utils.SetObjectMaterial(transform, placedPartMaterial);
        Utils.SetObjectColor(transform, new Color(1f, 1f, 1f, 1.0f));
        Destroy(collidingObject);
        collidingObject = null;
    }

    private void SetCollidingObject(Collider col)
    {
        if (collidingObject || !col.GetComponent<Rigidbody>())
        {
            return;
        }
        //check if part names match, filtering the unity added "(Clone)"
        else if (col.transform.name.Replace("(Clone)", "").Equals(transform.name.Replace("(Clone)", "")))
        {
            collidingObject = col.gameObject;

            // set Color to green
            Utils.SetObjectColor(transform, new Color(0f, 1f, 0f, 0.4f));
        }
        
    }

    public void OnTriggerEnter(Collider other)
    {
        SetCollidingObject(other);
    }

    public void OnTriggerStay(Collider other)
    {
        SetCollidingObject(other);
    }

    public void OnTriggerExit(Collider other)
    {
        if (!collidingObject)
        {
            return;
        }

        collidingObject = null;

        // set Color to white
        Utils.SetObjectColor(transform, new Color(1f, 1f, 1f, 0.4f));
    }


}
