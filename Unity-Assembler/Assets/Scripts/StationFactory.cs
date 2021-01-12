using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StationFactory
{
    static List<GameObject> stations;
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

    public StationFactory()
    {
        stations = new List<GameObject>();
    }

    public GameObject CreateStation(Vector3 position, List<GameObject> gameObjects)
    {
        Debug.LogWarning("listsize = " + gameObjects.Count);
        if (maxNumberOfStations > numberOfStations)
        {
            modelObjects = gameObjects;
            station = new GameObject("Station" + numberOfStations);
            station.transform.position = position;
            stationModelPodest = GameObject.Instantiate((GameObject)Resources.Load("ModelPrep"));
            stationModelPodest.name = station.name + " - Model";
            stationModelPodest.transform.parent = station.transform;
            stationModelPodest.transform.localPosition = new Vector3(-1.25f, 0.07f, -0.75f);
            stationModelPodest.AddComponent<ModelPlan>();
            ModelPlan mp = stationModelPodest.GetComponent<ModelPlan>();
            mp.setMaterial((Material)Resources.Load("CycleTransparent"));
            mp.createModel(gameObjects);
            modelMontagePlan = new List<GameObject>();
            PartsOnTable(position, station);
            stations.Add(station);
            numberOfStations++;

            
        }
        else
        {
            // TODO: maybe show dialog max number of stations
        }
        return station;
    }

    private void PartsOnTable(Vector3 position, GameObject station)
    {
        GameObject table_small = (GameObject)Resources.Load("Pref_Station_Small");
        for (int i = 0; i < modelObjects.Count; i++)
        {
            GameObject tmpTable = GameObject.Instantiate(table_small);
            tmpTable.AddComponent<Rigidbody>();
            tmpTable.GetComponent<Rigidbody>().isKinematic = true;
            tmpTable.AddComponent<BoxCollider>();
            tmpTable.transform.position = position + (i + 1) * new Vector3(1, 0, 0) + new Vector3(-0.75f, 0.8f, 1.25f);
            tmpTable.transform.parent = station.transform;

            GameObject tmpGameObject = GameObject.Instantiate(modelObjects[i], position + (i + 1) * new Vector3(1, 0, 0) + new Vector3(-0.75f, 1f, 0.75f), Quaternion.Euler(0, 0, 90));

            tmpGameObject.transform.parent = station.transform;
            tmpGameObject.AddComponent<Rigidbody>();
            tmpGameObject.GetComponent<Rigidbody>().useGravity = true;
            tmpGameObject.GetComponent<Rigidbody>().isKinematic = true;
            tmpGameObject.GetComponent<MeshCollider>().isTrigger = false;


            //GameObject tmpGameObject = Instantiate(modelObjects[i], position + (i+1) * new Vector3(0.5f, 0, 0) + new Vector3(0.5f, 0, 2.5f), Quaternion.Euler(0, 90, 0));
            //tmpGameObject.transform.parent = station.transform;
            //tmpGameObject.AddComponent<Rigidbody>();
            //tmpGameObject.GetComponent<Rigidbody>().useGravity = true;
            //tmpGameObject.GetComponent<Rigidbody>().isKinematic = false;
            //tmpGameObject.GetComponent<MeshCollider>().isTrigger = false;

        }
    }

    void AddToModelPlan(GameObject gameObject)
    {
        modelMontagePlan.Add(gameObject);
    }

    void RemoveFromModelPlan(GameObject gameObject)
    {
        modelMontagePlan.Remove(gameObject);
    }
}
