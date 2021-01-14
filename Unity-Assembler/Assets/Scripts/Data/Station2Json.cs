using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.Newtonsoft.Json;

public class Station2Json
{
    public string Name { get; set; }
    public List<Part> PartList { get; set; }
    public List<string> PreviousStations { get; set; }
    public float[] Position;
    public float[] Rotation;
    private Station station;

    public Station2Json(Station station)
    {
        this.station = station;
        this.Name = station.Name;
        this.Position = new float[3] { this.station.Position.x, this.station.Position.y, this.station.Position.z };
        this.Rotation = new float[4] { this.station.Rotation.x, this.station.Rotation.y, this.station.Rotation.z, this.station.Rotation.w };

        this.PartList = station.PartList;


        Debug.Log("PartID:" + this.PartList[0].PartID);
        List<string> previousStationsNames = new List<string>();
        foreach (var st in station.PreviousStations)
        {
            if (st.Name != null)
                previousStationsNames.Add(st.Name);
        }
        this.PreviousStations = previousStationsNames;


    }

    public Station2Json()
    {

    }


    // Convert Station2Json object to Station Object
    private static Station ToStation(Station2Json station2Json)
    {

        Station station = new Station(station2Json.PartList);
        station.Name = station2Json.Name;
        station.Position = new Vector3(station2Json.Position[0], station2Json.Position[1], station2Json.Position[2]);
        station.Rotation = new Quaternion(station2Json.Rotation[0], station2Json.Rotation[1], station2Json.Rotation[2], station2Json.Rotation[3]);

        return station;
    }

    // get list of stations from json file
    public static List<Station> stationDeserializer(string json)
    {
        List<Station> stations = new List<Station>();

        // get dictionary of <string, Station2Json> from file
        // Dictionary<string, Station2Json> DicOfStations = SaveLoadManager.Load<Dictionary<string, Station2Json>>(fileName);
        Dictionary<string, Station2Json> DicOfStations = JsonConvert.DeserializeObject<Dictionary<string, Station2Json>>(json);
        //
        foreach (KeyValuePair<string, Station2Json> keyValue in DicOfStations)
        {
            Station station = ToStation(keyValue.Value);

            List<Station> PreviousStations = new List<Station>();
            foreach (string prefStatinon in keyValue.Value.PreviousStations)
            {
                Station prefStation = ToStation(DicOfStations[prefStatinon]);
                PreviousStations.Add(prefStation);
            }
            station.PreviousStations = PreviousStations;

            stations.Add(station);
        }


        return stations;
    }

    // convert list of station to json
    public static string stationSerializer(List<Station> stations)
    {
        List<string[]> stationJson = new List<string[]>();
        Dictionary<string, Station2Json> dic = new Dictionary<string, Station2Json>();
        foreach (Station station_ in stations)
        {
            Station2Json conv = new Station2Json(station_);
            dic.Add(conv.Name, conv.station2Dic()[conv.Name]);
        }
        return JsonConvert.SerializeObject(dic, Valve.Newtonsoft.Json.Formatting.Indented);
    }

    // convert this class to Dictionary<string, Station2Json>
    public Dictionary<string, Station2Json> station2Dic()
    {

        Station2Json Station2Json = new Station2Json
        {
            Name = this.Name,
            PartList = this.PartList,
            PreviousStations = this.PreviousStations,
            Position = this.Position,
            Rotation = this.Rotation
        };
        Dictionary<string, Station2Json> dic = new Dictionary<string, Station2Json>();
        dic.Add(Station2Json.Name, Station2Json);
        return dic;
    }


}
