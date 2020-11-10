using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductInit : MonoBehaviour
{   static int PartsNumber;
    // Start is called before the first frame update
    void Start()
    {
        // here will initialized the parts of prduct with empty gameobject that
        // has Rigidbody &  Rigidbody &  CollisionDetector attached

        // Catch all childs of the product main object
        foreach (Transform child in transform)
        {
            // child.gameObject.AddComponent<Rigidbody>().useGravity=false;
            PartsNumber++;
            GameObject EmptyObj = new GameObject(child.name+ "_collider");
            EmptyObj.transform.position = child.position;
            EmptyObj.transform.parent = child;
            EmptyObj.AddComponent<Rigidbody>().useGravity = false;
            EmptyObj.AddComponent<MeshCollider>().convex=true;
            EmptyObj.GetComponent<MeshCollider>().isTrigger=true;
            EmptyObj.GetComponent<MeshCollider>().sharedMesh=child.GetComponent<MeshFilter>().mesh;
            EmptyObj.AddComponent<CollisionDetector>();
        }

        Debug.Log("ALl number: " + PartsNumber++);
    }
 
}
