using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Movement : MonoBehaviour
{
    public float speed = 100.0f;


    private void Start()
    {
        Debug.Log("load movment");
    }

    // Update is called once per frame
    void Update()
    {
        MoveCharacter(new Vector3(Input.GetAxis("Horizontal"),0, Input.GetAxis("Vertical")));
 

    }
    void MoveCharacter(Vector3 direction)
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }
}
