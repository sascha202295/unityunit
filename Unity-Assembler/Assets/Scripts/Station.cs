using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Station : MonoBehaviour
{
    List<GameObject> modelObjects;
    List<GameObject> modelMontagePlan;
    public GameObject modelPodest;
    public GameObject station;
    public GameObject chest;
    // Start is called before the first frame update
    void Start()
    {
        List<Vector3> stations =  new List<Vector3>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void createStation(Vector3 position)
    {
        station.transform.position = position;
        Instantiate(modelPodest);
        modelPodest.transform.position = position + new Vector3(-2, 0, 1);
        modelPodest.AddComponent<ModelPlan>();
    }
}
