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
        if (Input.GetKeyDown(KeyCode.M))
        {
            createStation(new Vector3(5 * (numberOfStations + 1) + 10, 0, 0), TestList);
        }
    }

    public void createStation(Vector3 position, List<GameObject> gameObjects)
    {
        Debug.LogWarning("listsize = " + gameObjects.Count);
        if (maxNumberOfStations > numberOfStations)
        {
            modelObjects = gameObjects;
            station = new GameObject("Station" + numberOfStations);
            station.transform.position = position;
            stationModelPodest = Instantiate((GameObject) Resources.Load("ModelPrep"));
            stationModelPodest.name = station.name + " - Model";
            stationModelPodest.transform.parent = station.transform;
            stationModelPodest.transform.localPosition = new Vector3(0, 0.2f, 0);
            stationModelPodest.AddComponent<ModelPlan>();
            ModelPlan mp = stationModelPodest.GetComponent<ModelPlan>();
            mp.setMaterial((Material)Resources.Load("CycleTransparent"));
            mp.createModel(gameObjects);
            modelMontagePlan = new List<GameObject>();
            PartsOnTable(position, station);
            stations.Add(station);
            numberOfStations++;
        } else
        {
            // TODO: maybe show dialog max number of stations
        }
    }

    private void PartsOnTable(Vector3 position, GameObject station)
    {
        GameObject table_small = (GameObject) Resources.Load("Pref_Station_Small");
        for (int i =0; i < modelObjects.Count; i++)
        {
            GameObject tmpTable = Instantiate(table_small);
            tmpTable.AddComponent<Rigidbody>();
            tmpTable.GetComponent<Rigidbody>().isKinematic = true;
            tmpTable.AddComponent<BoxCollider>();
            tmpTable.transform.position = position + (i + 1) * new Vector3(1, 0, 0) + new Vector3(0.5f, 0.8f, 3f);
            tmpTable.transform.parent = station.transform;
            
            GameObject tmpGameObject = Instantiate(modelObjects[i], position + (i + 1) * new Vector3(1, 0, 0) + new Vector3(0.5f, 1f, 2.5f), Quaternion.Euler(0, 0, 90));
            tmpGameObject.transform.parent = station.transform;
            tmpGameObject.AddComponent<Rigidbody>();
            tmpGameObject.GetComponent<Rigidbody>().useGravity = true;
            tmpGameObject.GetComponent<Rigidbody>().isKinematic = true;
            tmpGameObject.GetComponent<MeshCollider>().isTrigger = false;
            //tmpGameObject.AddComponent<BoxCollider>();
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
