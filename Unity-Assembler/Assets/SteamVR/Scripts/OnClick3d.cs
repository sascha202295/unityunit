using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnClick3d : MonoBehaviour
{
    public GameObject definedButton;
    public UnityEvent OnClick = new UnityEvent();
    public GameObject rotateGmaeObject;
    public int rotateDirection = 1;

    // Use this for initialization
    void Start()
    {
        definedButton = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        var ray = GameObject.Find("Camera3_").GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
        RaycastHit Hit;

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out Hit) && Hit.collider.gameObject == gameObject)
            {
    
                  rotateGmaeObject.transform.Rotate(new Vector3(0, rotateDirection * 60 * Time.deltaTime, 0),Space.World);
              
                OnClick.Invoke();
            }
        }
    }
}
