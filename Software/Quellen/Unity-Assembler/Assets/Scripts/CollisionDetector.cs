using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    private void OnCollisionEnter(Collision col)
    {
       // Debug.Log( col.gameObject.name);

        Debug.Log("collision!!");
    }

    // OnTriggerEnter is called to detect overlap
    private void OnTriggerEnter(Collider other)
    {  
        Debug.Log("trrigger!!");
    }
}
