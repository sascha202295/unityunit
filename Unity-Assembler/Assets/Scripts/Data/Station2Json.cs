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
    public Station2Json(Station station) {
        this.station = station;
        this.Name = station.Name;
        
        }
       public Station2Json() {
       // this.Name = station.Name;
    }

        public static List<Station> stationDeserializer(string fileName)
    {

        List<Station> stations = new List<Station>();

      // Dictionary<string, Station2Json> Station2JsonDic = JsonConvert.DeserializeObject<Dictionary<string, Station2Json>>(fileName);
        Dictionary<string, Station2Json> Station2JsonDic = SaveLoadManager.Load<Dictionary<string, Station2Json>>(fileName);
        foreach (KeyValuePair<string, Station2Json> keyValue in Station2JsonDic) {

     
            Station station = ToStation(keyValue.Value);
        
            List<Station> PreviousStations = new List<Station>();
            foreach (string prefStatinon in keyValue.Value.PreviousStations)
            {
               Station prefStation= ToStation(Station2JsonDic[prefStatinon]);
                PreviousStations.Add(prefStation);
                }
                station.PreviousStations = PreviousStations;
     
            stations.Add(station);

        }

        Station ToStation( Station2Json station2Json) {


            Station station = new Station(station2Json.PartList);
            station.Name = station2Json.Name;
            station.Position = new Vector3(station2Json.Position[0], station2Json.Position[1], station2Json.Position[2]);
            station.Rotation = new Quaternion(station2Json.Rotation[0], station2Json.Rotation[1], station2Json.Rotation[2], station2Json.Rotation[3]);

            return station;

        } 

       


       

     
        return stations;
    }

 
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

   
    public Dictionary<string, Station2Json> station2Dic()
    {


        List<string> previousStationsNames = new List<string>();
        foreach (var station in this.station.PreviousStations)
        {
            if (station.Name != null)
                previousStationsNames.Add(station.Name);

        }

        Station2Json Station2Json = new Station2Json
        {


            Name = this.Name,
            PartList = this.station.PartList,
            PreviousStations = previousStationsNames,
            Position = new float[3] { this.station.Position.x, this.station.Position.y, this.station.Position.z },
            Rotation = new float[4] { this.station.Rotation.x, this.station.Rotation.y, this.station.Rotation.z, this.station.Rotation.w }
        };
        Dictionary<string, Station2Json> dic = new Dictionary<string, Station2Json>();
        dic.Add(Station2Json.Name, Station2Json);
        return dic;
    }


}
