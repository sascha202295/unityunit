using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationManager : MonoBehaviour
{
    public Vector3 PartpickerModelPosition;
    public Vector3 PartpickerModelScale;
    public Material materialTransparent;
    public Material materialOpaque;
    public GameObject mStationPicker;
    public GameObject mPartPickerScreen;
    public GameObject mUIPointer;

    public GameObject PartPickerModel { get; private set; }
    public List<Station> Stations { get; private set; }

    void Awake()
    {
        // Instantiate PartPickerModel, position and scale it
        PartPickerModel = Instantiate((GameObject)Resources.Load("Cyclev2"), transform);
        PartPickerModel.transform.localPosition = PartpickerModelPosition;
        PartPickerModel.transform.localScale = Vector3.Scale(PartPickerModel.transform.localScale, PartpickerModelScale);

        List<Part> partData = new List<Part>(PartPickerModel.transform.childCount);

        foreach (Transform part in PartPickerModel.transform)
        {
            // set Material to Opaque
            Utils.SetObjectMaterial(part, materialOpaque);

            partData.Add(new Part(part.name));
        }
        // Part.Parts now contains list of all model components
        Part.Parts = partData;

        Stations = new List<Station>();

        if (!PlayerPrefs.GetString("fileName", "").Equals(""))
        {
            Load(PlayerPrefs.GetString("fileName"));
        }
    }

    public void AddStation(Station station)
    {
        if (!Stations.Contains(station))
        {
            Stations.Add(station);
        }
    }

    public void Save()
    {
        string json = Station2Json.stationSerializer(this.Stations);
        string fileName = "test";
        SaveLoadManager.SaveToFile(json, fileName);
    }

    public void Load(string fileName)
    {
        string json = SaveLoadManager.LoadFromFile(fileName);
        this.Stations = Station2Json.stationDeserializer(json);

        StationFactory factory = new StationFactory();

        foreach (Station station in Stations)
        {
            // place stations in scene
            factory.CreateStation(station, mUIPointer);

            // disable selected parts in the PartPicker model
            foreach (Part part in station.PartList)
            {
                Utils.SetObjectMaterial(PartPickerModel.transform.GetChild(part.PartID), materialTransparent);
                Utils.SetObjectColor(PartPickerModel.transform.GetChild(part.PartID), new Color(0f, 1f, 0f, 0.1f));
                Utils.RemoveAllMeshColliders(PartPickerModel.transform.GetChild(part.PartID));
            }
        }
        // update stationPickers stations list so they can be displayed 
        mStationPicker.GetComponent<PreviousStationController>().SetStationList(Stations);
    }


}
