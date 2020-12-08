using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelPlan : MonoBehaviour
{

    public List<GameObject> modelGameObjects;

    private Vector3 modelStandPosition;
    private GameObject modelObject;
    // Start is called before the first frame update
    void Start()
    {
        modelStandPosition = gameObject.transform.position;
        modelStandPosition.y += 1;
        modelObject = new GameObject("ModelPlan");
        modelObject.AddComponent<ProductAssemblyController>();
        modelObject.transform.parent = gameObject.transform;
        modelObject.transform.position = modelStandPosition;
        Instantiate(modelObject);
        modelGameObjects.ForEach(delegate (GameObject gameObject)
        {
            GameObject tmpGameObject = Instantiate(gameObject);
            tmpGameObject.transform.position = modelStandPosition;
            tmpGameObject.transform.parent = modelObject.transform;
            // tmpGameObject.AddComponent<MeshRenderer>();
        });

        modelObject.transform.rotation = gameObject.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
