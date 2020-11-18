using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class VR_UIPointer : MonoBehaviour
{
    public float mDefaultLength = 5.0f;
    public GameObject mDot;
    public VRInputModule mInputmodule;

    private LineRenderer mLinerenderer = null;
    
    void Awake()
    {
        mLinerenderer = GetComponent<LineRenderer>();
    }
    
    void Update()
    {
        PointerEventData data = mInputmodule.GetData();
        float targetLength = data.pointerCurrentRaycast.distance == 0 ? mDefaultLength : data.pointerCurrentRaycast.distance;

        RaycastHit hit = CreateRaycast(targetLength);

        Vector3 endPosition = transform.position + (transform.forward * targetLength);

        if (hit.collider != null)
            endPosition = hit.point;

        mDot.transform.position = endPosition;

        mLinerenderer.SetPosition(0, transform.position);
        mLinerenderer.SetPosition(1, endPosition);
    }

    private RaycastHit CreateRaycast(float length)
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);
        Physics.Raycast(ray, out hit, mDefaultLength);

        return hit;
    }
}
