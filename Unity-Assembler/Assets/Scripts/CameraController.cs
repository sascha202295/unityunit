using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public float RotaionSpeed = 1;
    private Transform target, Camera;
    float mouseX, mouseY;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        target = GameObject.Find("Target").GetComponent<Transform>();
        Camera = this.transform;
    }

    private void LateUpdate()
    {
        CamControl();
    }
    void CamControl()
    {

        mouseX += Input.GetAxis("Mouse X") * RotaionSpeed;
        mouseY -= Input.GetAxis("Mouse Y") * RotaionSpeed;

        mouseY = Mathf.Clamp(mouseY, -60, 60);

        Camera.LookAt(target);

        target.rotation = Quaternion.Euler(mouseY, mouseX, 0);

    }
}
