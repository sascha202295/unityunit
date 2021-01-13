using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils
{
    /***
     * sets the color of the given Transform and its direct childrens Renderers to the given Color 
     */
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

    /***
     * sets the material of the given Transform and its direct childrens Renderers to the given material 
     */
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

    /***
     * Destroys all MeshCollider components of the given Transform
     */
    public static void RemoveAllMeshColliders(Transform transform)
    {
        MeshCollider[] colliders = transform.GetComponents<MeshCollider>();
        foreach (MeshCollider collider in colliders)
        {
            GameObject.Destroy(collider);
        }
    }
}
