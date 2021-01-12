using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils
{
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
