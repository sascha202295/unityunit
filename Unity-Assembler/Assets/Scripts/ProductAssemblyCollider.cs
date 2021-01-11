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
        setObjectMaterial(transform, placedPartMaterial);
        setObjectColor(transform, new Color(1f, 1f, 1f, 1.0f));
        Destroy(collidingObject);
        collidingObject = null;
    }

    private void SetCollidingObject(Collider col)
    {
        if (collidingObject || !col.GetComponent<Rigidbody>())
        {
            return;
        }
        
        if(col.transform.parent != null && col.transform.parent.name == transform.name)
        {
            collidingObject = col.transform.parent.gameObject;

            // set Color to green
            setObjectColor(transform, new Color(0f, 1f, 0f, 0.4f));
        } 
        else if(col.transform.name == transform.name)
        {
            collidingObject = col.gameObject;

            // set Color to green
            setObjectColor(transform, new Color(0f, 1f, 0f, 0.4f));
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
        setObjectColor(transform, new Color(1f, 1f, 1f, 0.4f));
    }

    private void setObjectColor(Transform transform, Color color)
    {
        if (transform.childCount > 0)
        {
            foreach (Transform child in transform)
            {
                if (child.gameObject.GetComponent<Renderer>().material != null)
                {
                    child.gameObject.GetComponent<Renderer>().material.color = color;
                }
            }
        }
        else
        {
            if (transform.gameObject.GetComponent<Renderer>().material != null)
            {
                transform.gameObject.GetComponent<Renderer>().material.color = color;
            }
        }
    }

    private void setObjectMaterial(Transform transform, Material material)
    {
        if (transform.childCount > 0)
        {
            foreach (Transform child in transform)
            {
                child.gameObject.GetComponent<Renderer>().material = material;
            }
        }
        else
        {
            transform.gameObject.GetComponent<Renderer>().material = material;
        }
    }
}
