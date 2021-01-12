using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StationFactory
{

    public Material materialTransparent;
    public Material materialOpaque;
    private Vector3 scale = new Vector3(1.5f, 1.5f, 1.5f);

    private static int numberOfStations = 0;
    private const int maxNumberOfStations = 50;

    private GameObject tmpStation;
    private GameObject stationModelPodest;
    private GameObject stationModel;
    private List<GameObject> modelObjects;

    public StationFactory()
    {
        materialTransparent = (Material)Resources.Load("CycleTransparent");
        materialOpaque = (Material)Resources.Load("CycleWhite");
        modelObjects = new List<GameObject>();
    }

    public GameObject CreateStation(Station station)
    {
        if (maxNumberOfStations > numberOfStations)
        {
            //create station object
            tmpStation = new GameObject("Station" + numberOfStations);
            tmpStation.transform.position = station.Position;

            //create station model
            stationModelPodest = GameObject.Instantiate((GameObject)Resources.Load("ModelPrep"));
            stationModelPodest.name = tmpStation.name + " - Model";
            stationModelPodest.transform.parent = tmpStation.transform;
            stationModelPodest.transform.localPosition = new Vector3(-1.25f, 0.07f, -0.75f);
            Vector3.Scale(stationModelPodest.transform.localScale, scale);

            stationModel = GameObject.Instantiate((GameObject)Resources.Load("Cyclev2"));
            Vector3.Scale(stationModel.transform.localScale, scale);
            stationModel.transform.localPosition = stationModel.transform.localPosition + new Vector3(0, 0.4f, -0.2f);
            foreach(Transform modelPart in stationModel.transform)
            {
                modelPart.gameObject.SetActive(false);
            }
            foreach(Part part in station.PartList)
            {
                Transform partObject = stationModel.transform.GetChild(part.PartID);
                partObject.gameObject.SetActive(true);
                modelObjects.Add(partObject.gameObject);
            }
            //TODO enable and color parts of previous stations and color them differently

            //place StationScreen

            //place parts on Tables
            PartsOnTable(tmpStation);
            numberOfStations++;
        }
        else
        {
            // TODO: maybe show dialog max number of stations
        }
        return tmpStation;
    }

    private void PartsOnTable(GameObject station)
    {
        GameObject table_small = (GameObject)Resources.Load("Pref_Station_Small");
        for (int i = 0; i < modelObjects.Count; i++)
        {
            GameObject tmpTable = GameObject.Instantiate(table_small);
            tmpTable.AddComponent<Rigidbody>();
            tmpTable.GetComponent<Rigidbody>().isKinematic = true;
            tmpTable.AddComponent<BoxCollider>();
            tmpTable.transform.position = station.transform.position + (i + 1) * new Vector3(1, 0, 0) + new Vector3(-0.75f, 0.8f, 1.25f);
            tmpTable.transform.parent = station.transform;

            GameObject tmpGameObject = GameObject.Instantiate(modelObjects[i], station.transform.position + (i + 1) * new Vector3(1, 0, 0) + new Vector3(-0.75f, 1f, 0.75f), Quaternion.Euler(0, 0, 90));

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
}
