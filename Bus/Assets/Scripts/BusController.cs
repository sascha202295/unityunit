using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BusController : MonoBehaviour
{

    public float speed = 100;
    public float turnStrength = 180;
    // Update is called once per frame
    void Update()
    {
        busMovement();
    }
    void busMovement()
    {
        float hor = Input.GetAxis("Horizontal");    
        float ver = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(hor,0f,ver)*speed*Time.deltaTime;
        Vector3 rotation = new Vector3(0f, ver * turnStrength * Time.deltaTime,0f);
        transform.Translate(movement, Space.Self);
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + rotation);

    }
}
