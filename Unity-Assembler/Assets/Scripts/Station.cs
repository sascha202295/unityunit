using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Station : MonoBehaviour
{
    List<GameObject> stations;
    public List<GameObject> TestList;
    List<GameObject> modelObjects;
    List<GameObject> modelMontagePlan;
    public GameObject modelPodest;
    public Material material;
    
    public GameObject chest;
    private GameObject station;
    private GameObject stationModelPodest;
    private int numberOfStations = 0;
    private const int maxNumberOfStations = 50;
    // Start is called before the first frame update
    void Start()
    {
        stations =  new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backslash))
        {
            createStation(new Vector3(5 * numberOfStations, 0, 0), TestList);
        }
    }

    public void createStation(Vector3 position, List<GameObject> gameObjects)
    {
        if (maxNumberOfStations > numberOfStations)
        {
            modelObjects = gameObjects;
            station = new GameObject("Station" + numberOfStations);
            station.transform.position = position;
            stationModelPodest = Instantiate(modelPodest);
            stationModelPodest.name = station.name + " - Model";
            stationModelPodest.transform.parent = station.transform;
            stationModelPodest.transform.localPosition = position + new Vector3(0, 0.2f, 0);
            stationModelPodest.AddComponent<ModelPlan>();
            ModelPlan mp = stationModelPodest.GetComponent<ModelPlan>();
            mp.setMaterial(material);
            mp.createModel(gameObjects);
            modelMontagePlan = new List<GameObject>();

            stations.Add(station);
            numberOfStations++;
        } else
        {
            // TODO: maybe show dialog max number of stations
        }
    }

    void addToModelPlan(GameObject gameObject)
    {
        modelMontagePlan.Add(gameObject);
    }

    void removeFromModelPlan(GameObject gameObject)
    {
        modelMontagePlan.Remove(gameObject);
    }
}
