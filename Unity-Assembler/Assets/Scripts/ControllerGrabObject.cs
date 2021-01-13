using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ControllerGrabObject : MonoBehaviour
{
    public SteamVR_Input_Sources mHandType;
    public SteamVR_Behaviour_Pose mControllerPose;
    public SteamVR_Action_Boolean mGrabAction;

    private GameObject collidingObject;
    public GameObject objectInHand { get; private set; }

    private void SetCollidingObject(Collider col)
    {
        if (collidingObject != null || !col.GetComponent<Rigidbody>())
        {
            return;
        }
        collidingObject = col.gameObject;
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
        if (collidingObject == other.gameObject)
        {
            return;
        }
        collidingObject = null;
    }

    private void GrabObject()
    {
        objectInHand = collidingObject;
        collidingObject = null;
        var joint = AddFixedJoint();
        objectInHand.GetComponent<Rigidbody>().isKinematic = false;
        joint.connectedBody = objectInHand.GetComponent<Rigidbody>();
    }

    private FixedJoint AddFixedJoint()
    {
        FixedJoint fx = gameObject.AddComponent<FixedJoint>();
        fx.breakForce = 20000;
        fx.breakTorque = 20000;
        return fx;
    }

    private void ReleaseObject()
    {
        if (GetComponent<FixedJoint>())
        {
            GetComponent<FixedJoint>().connectedBody = null;
            Destroy(GetComponent<FixedJoint>());
            objectInHand.GetComponent<Rigidbody>().velocity = mControllerPose.GetVelocity();
            objectInHand.GetComponent<Rigidbody>().angularVelocity = mControllerPose.GetAngularVelocity();
        }
        objectInHand = null;
    }



    // Update is called once per frame
    void Update()
    {
        if (mGrabAction.GetLastStateDown(mHandType))
        {
            if (collidingObject)
            {
                GrabObject();
            }
        }

        if (mGrabAction.GetLastStateUp(mHandType))
        {
            if (objectInHand)
            {
                ReleaseObject();
            }
        }

    }
}
