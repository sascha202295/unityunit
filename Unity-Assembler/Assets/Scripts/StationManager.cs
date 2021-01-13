using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationManager : MonoBehaviour
{
    public Vector3 PartpickerModelPosition;
    public Vector3 PartpickerModelScale;
    public Material materialTransparent;
    public Material materialOpaque;

    public GameObject PartPickerModel;

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

        if (PlayerPrefs.GetString("fileName") !=null) {
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

      string json =  Station2Json.stationSerializer(this.Stations);
        string fileName = "test";
        SaveLoadManager.SaveToFile(json, fileName);
    }

    public void Load(string fileName)
    {
       // string fileName = "test";
        this.Stations = Station2Json.stationDeserializer(fileName);
    }


}
