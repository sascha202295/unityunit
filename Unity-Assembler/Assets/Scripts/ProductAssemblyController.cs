﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ProductAssemblyController : MonoBehaviour
{
    public bool mAddProductAssemblyCollider = false;
    public SteamVR_Input_Sources mHandType;
    public SteamVR_Action_Boolean mGrabAction;

    void Start()
    {
        // add MeshColliders to every child component, if child has subparts, add collider to parent
        foreach (Transform child in transform)
        {
            if (child.childCount > 0)
            {
                if (child.GetComponents<MeshCollider>().Length != child.childCount)
                {
                    foreach (Transform grandchild in child)
                    {
                        if (grandchild.GetComponent<MeshFilter>() != null)
                        {
                            AddMeshColliderTriggerTo(child.gameObject, grandchild.GetComponent<MeshFilter>().sharedMesh);
                        }
                    }
                }
            }
            else if (child.GetComponent<MeshFilter>() != null)
            {
                
                if (child.GetComponents<MeshCollider>().Length != 1)
                    AddMeshColliderTriggerTo(child.gameObject, child.GetComponent<MeshFilter>().sharedMesh);
            }
            if (mAddProductAssemblyCollider)
            {
                ProductAssemblyCollider tmp = child.gameObject.AddComponent<ProductAssemblyCollider>();
                tmp.mHandType = this.mHandType;
                tmp.mGrabAction = this.mGrabAction;
            }
        }
    }

    private void AddMeshColliderTriggerTo(GameObject gObject, Mesh mesh)
    {
        MeshCollider collider = gObject.AddComponent<MeshCollider>();
        collider.sharedMesh = mesh;
        collider.convex = true;
        collider.isTrigger = true;
    }
}
