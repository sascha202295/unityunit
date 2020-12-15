using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ModelPlan : MonoBehaviour
{

    List<GameObject> modelGameObjects;
    private Material material;
    private Vector3 modelStandPosition;
    private GameObject modelObject;
    // Start is called before the first frame update
    void Start()
    {
        //createModel();
    }

    void Update()
    {

    }


    public void createModel(List<GameObject> gameObjects)
    {
        gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        modelStandPosition = gameObject.transform.position;

        modelObject = new GameObject("ModelPlan");
        modelObject.AddComponent<ProductAssemblyController>();
        ProductAssemblyController pac = modelObject.GetComponent<ProductAssemblyController>();
        pac.mAddProductAssemblyCollider = true;
        pac.mGrabAction = SteamVR_Actions.actions_GrabPinch;
        modelObject.transform.rotation = gameObject.transform.rotation;
        modelObject.transform.parent = gameObject.transform;
        modelObject.transform.position = modelStandPosition;    
        
        modelGameObjects = gameObjects;
        modelGameObjects.ForEach(delegate (GameObject gameObject)
        {
            GameObject tmpGameObject = Instantiate(gameObject);
            tmpGameObject.transform.position = modelStandPosition;
            tmpGameObject.transform.parent = modelObject.transform;
            MeshRenderer tmpMeshRenderer = tmpGameObject.GetComponent<MeshRenderer>();
            if (tmpMeshRenderer == null)
            {
                for (int i = 0; i < tmpGameObject.transform.childCount; i++)
                {
                    tmpGameObject.transform.GetChild(i).gameObject.GetComponent<MeshRenderer>().material = material;                    
                }
            }
            else
            {
                tmpMeshRenderer.material = material;
            }
        });

        modelObject.transform.localPosition = new Vector3(0, 0.4f, -0.2f);
        modelObject.transform.localScale = new Vector3(4.0f / 3.0f, 4, 0.4f);
    }

    public void setMaterial(Material material)
    {
        this.material = material;
    }
}
