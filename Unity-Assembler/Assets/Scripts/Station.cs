using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

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

    public SteamVR_Action_Boolean mPlaceStationAction;
    public SteamVR_Input_Sources mTargetSource;

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
            stationModelPodest.transform.localPosition = new Vector3(0, 0.07f, 1);
            stationModelPodest.AddComponent<ModelPlan>();
            ModelPlan mp = stationModelPodest.GetComponent<ModelPlan>();
            mp.setMaterial((Material)Resources.Load("CycleTransparent"));
            mp.createModel(gameObjects);
            modelMontagePlan = new List<GameObject>();
            PartsOnTable(position, station);
            stations.Add(station);
            numberOfStations++;

            //rotate station on creation
      /* 1st while loop- wait until the pressed button is released ,2nd while loop rotate station using left and right controllers and press button to terminate

            while (true)
            {
                if (mPlaceStationAction.GetStateUp(mTargetSource))
                 break;
                
            }

            while (true)
            {    

                        if left() {
                            station.transform.Rotate(0, 5f, 0);
                        }
                        else right() {
                            station.transform.Rotate(0, -5f, 0);
                        }
                        if (mPlaceStationAction.GetStateDown(mTargetSource)) { 
                            break;
                        }
            }
      */
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


            //GameObject tmpGameObject = Instantiate(modelObjects[i], position + (i+1) * new Vector3(0.5f, 0, 0) + new Vector3(0.5f, 0, 2.5f), Quaternion.Euler(0, 90, 0));
            //tmpGameObject.transform.parent = station.transform;
            //tmpGameObject.AddComponent<Rigidbody>();
            //tmpGameObject.GetComponent<Rigidbody>().useGravity = true;
            //tmpGameObject.GetComponent<Rigidbody>().isKinematic = false;
            //tmpGameObject.GetComponent<MeshCollider>().isTrigger = false;

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
