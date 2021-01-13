using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StationFactory
{

    public Material materialTransparent;
    public Material materialOpaque;

    private Vector3 stationModelOffset = new Vector3(-1.25f, 0, -0.75f);
    private Vector3 stationModelPedestalOffset = new Vector3(0, 0.06f, 0);
    private Vector3 stationModelModelOffset = new Vector3(0, 0.25f, -0.8f);
    private Vector3 stationScreenOffset = new Vector3(0.7f, 0.6f, -3.0f);
    private Vector3 scale = new Vector3(1.5f, 1.5f, 1.5f);

    private Color partPreviousStationColor = new Color(0.1f, 0.1f, 0.1f, 0.05f);

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

    public GameObject CreateStation(Station station, GameObject ui_Pointer)
    {
        if (maxNumberOfStations > numberOfStations)
        {
            //create station object
            tmpStation = new GameObject("Station" + numberOfStations);
            tmpStation.transform.position = station.Position;

            station.Name = "Station" + numberOfStations;

            GameObject model = new GameObject();
            model.transform.name = tmpStation.name + " - Model";
            model.transform.parent = tmpStation.transform;
            model.transform.localPosition = stationModelOffset;

            //create station model
            stationModelPodest = GameObject.Instantiate((GameObject)Resources.Load("ModelPrep"), model.transform);
            stationModelPodest.name = "Podest";
            stationModelPodest.transform.localPosition = stationModelPedestalOffset;
            stationModelPodest.transform.localScale = Vector3.Scale(stationModelPodest.transform.localScale, scale);

            stationModel = GameObject.Instantiate((GameObject)Resources.Load("Cyclev2"), model.transform);
            stationModel.transform.localScale = Vector3.Scale(stationModel.transform.localScale, scale);
            stationModel.transform.localPosition = stationModel.transform.localPosition + stationModelModelOffset;
            stationModel.GetComponent<ProductAssemblyController>().AddProductAssemblyColliders();
            foreach (Transform modelPart in stationModel.transform)
            {
                modelPart.gameObject.SetActive(false);
            }
            foreach (Part part in station.PartList)
            {
                Transform partObject = stationModel.transform.GetChild(part.PartID);
                partObject.gameObject.SetActive(true);
                modelObjects.Add(partObject.gameObject);
            }

            // enable parts of previous stations and color them distinctly
            foreach (Part part in station.GetPreviousStationsParts())
            {
                Transform partObject = stationModel.transform.GetChild(part.PartID);
                partObject.gameObject.SetActive(true);
                Utils.SetObjectColor(partObject, partPreviousStationColor);
            }

            //place StationScreen
            GameObject stationScreen = GameObject.Instantiate((GameObject)Resources.Load("StationScreen"), tmpStation.transform);
            stationScreen.transform.localPosition = stationScreenOffset;
            StationScreenController tmp = stationScreen.GetComponent<StationScreenController>();
            tmp.mUIPointer = ui_Pointer;
            tmp.mItemSpawnScale = scale;
            tmp.mItemSpawnMaterial = materialOpaque;
            tmp.mItemBuildMaterial = materialTransparent;
            tmp.station = station;
            tmp.stationModel = stationModel;


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

            Utils.SetObjectMaterial(tmpGameObject.transform, materialOpaque);
            GameObject.Destroy(tmpGameObject.GetComponent<ProductAssemblyCollider>());
            tmpGameObject.transform.parent = station.transform;
            tmpGameObject.AddComponent<Rigidbody>();
            tmpGameObject.GetComponent<Rigidbody>().useGravity = true;
            tmpGameObject.GetComponent<Rigidbody>().isKinematic = true;
            foreach (MeshCollider collider in tmpGameObject.GetComponents<MeshCollider>())
            {
                collider.isTrigger = false;
            }


            //GameObject tmpGameObject = Instantiate(modelObjects[i], position + (i+1) * new Vector3(0.5f, 0, 0) + new Vector3(0.5f, 0, 2.5f), Quaternion.Euler(0, 90, 0));
            //tmpGameObject.transform.parent = station.transform;
            //tmpGameObject.AddComponent<Rigidbody>();
            //tmpGameObject.GetComponent<Rigidbody>().useGravity = true;
            //tmpGameObject.GetComponent<Rigidbody>().isKinematic = false;
            //tmpGameObject.GetComponent<MeshCollider>().isTrigger = false;

        }
    }
}
