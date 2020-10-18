using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private float hInput;
    private float vInput;
    private float steerAngle;
    private bool isBreaking;
    private float brakeForce = 0f;

    private WheelCollider frontLeftWheelCollider;
    private WheelCollider frontRightWheelCollider;
    private WheelCollider rearLeftWheelCollider;
    private WheelCollider rearRightWheelCollider;
    private Transform frontLeftWheelTransform;
    private Transform frontRightWheelTransform;
    private Transform rearLeftWheelTransform;
    private Transform rearRightWheelTransform;
    
    public float maxSteeringAngle = 30f;
    public float motorForce = 1000f;
    

    private void Start()
    {
        frontLeftWheelCollider = GameObject.Find("f_l Colider").GetComponent<WheelCollider>();
        frontRightWheelCollider = GameObject.Find("f_r Colider").GetComponent<WheelCollider>();
        rearLeftWheelCollider = GameObject.Find("r_l Colider").GetComponent<WheelCollider>();
        rearRightWheelCollider = GameObject.Find("r_r Colider").GetComponent<WheelCollider>();
        frontLeftWheelTransform = GameObject.Find("f_l").GetComponent<Transform>();
        frontRightWheelTransform = GameObject.Find("f_r").GetComponent<Transform>();
        rearLeftWheelTransform = GameObject.Find("r_l").GetComponent<Transform>();
        rearRightWheelTransform = GameObject.Find("r_r").GetComponent<Transform>();
    }
    private void FixedUpdate()
    {
          
        input();
        motor();
        steering();
        updateWheels();
        
    }

    private void input()
    {
        hInput = Input.GetAxis("Horizontal");
        vInput = Input.GetAxis("Vertical");
      //  Debug.Log("hInput: " + hInput + " vInput: " + vInput);
        isBreaking = Input.GetKey(KeyCode.Space);
    }


    private void motor()
    {
        frontLeftWheelCollider.motorTorque = vInput * motorForce;
        frontRightWheelCollider.motorTorque = vInput * motorForce;

        brakeForce = isBreaking ? 6500f : 0f;
        frontLeftWheelCollider.brakeTorque = brakeForce;
        frontRightWheelCollider.brakeTorque = brakeForce;
        rearLeftWheelCollider.brakeTorque = brakeForce;
        rearRightWheelCollider.brakeTorque = brakeForce;
    }


    private void steering()
    {
        steerAngle = maxSteeringAngle * hInput;
        frontLeftWheelCollider.steerAngle = steerAngle;
        frontRightWheelCollider.steerAngle = steerAngle;
    }


    private void updateWheels()
    {
        updateWheelPos(frontRightWheelCollider, frontRightWheelTransform);
        updateWheelPos(frontLeftWheelCollider, frontLeftWheelTransform);
       
        updateWheelPos(rearLeftWheelCollider, rearLeftWheelTransform);
        updateWheelPos(rearRightWheelCollider, rearRightWheelTransform);
    }

    private void updateWheelPos(WheelCollider collider, Transform trans)
    {
        Vector3 pos;
        Quaternion rot;
        collider.GetWorldPose(out pos, out rot);
       // Debug.Log("rot: "+rot+" pos: "+pos);
        trans.rotation = rot;
       
        trans.position = pos;
    }

}