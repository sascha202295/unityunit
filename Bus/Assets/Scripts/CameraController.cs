using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public float RotaionSpeed = 1;
    public Transform target, bus;
    float mouseX, mouseY;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        camControl();
    }
    void camControl() {
    
    mouseX += Input.GetAxis("Mouse X")*RotaionSpeed;
    mouseY -= Input.GetAxis("Mouse Y")*RotaionSpeed;
    
    mouseY = Mathf.Clamp(mouseY, -35, 60);
        transform.LookAt(target);

        if (Input.GetKey(KeyCode.CapsLock))
        {
            target.rotation = Quaternion.Euler(mouseY, mouseX, 0);
            bus.rotation = Quaternion.Euler(0, mouseX, 0);
        }
        else {
            target.rotation = Quaternion.Euler(mouseY, mouseX, 0);

        }

    }
}
