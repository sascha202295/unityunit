using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.Newtonsoft.Json;

public class Station
{
    public string Name { get; }
    public List<Part> PartList { get; }
    public List<Station> PreviousStations { get; }
    public Vector3 Position { get; set; }
    public Quaternion Rotation { get; set; }

    public Station(List<Part> partList, List<Station> previousStations)
    {
        this.PartList = partList;
        this.PreviousStations = previousStations;
        Position = Vector3.zero;
        Rotation = new Quaternion();
    }

    public Station(List<Part> partList, Station previousStation)
    {
        this.PartList = partList;
        PreviousStations = new List<Station>
        {
            previousStation
        };
        Position = Vector3.zero;
        Rotation = new Quaternion();
    }

    public Station(List<Part> partList)
    {
        this.PartList = partList;
        PreviousStations = null;
        Position = Vector3.zero;
        Rotation = new Quaternion();
    }

    public bool AddPart(Part part)
    {
        if (!PartList.Contains(part))
        {
            PartList.Add(part);
            return true;
        }
        return false;
    }

    public bool RemovePart(Part part)
    {
        return PartList.Remove(part);
    }

    public List<Part> GetPreviousStationsParts()
    {
        List<Part> tmpParts = new List<Part>();
        foreach(Station prevStation in PreviousStations)
        {
            tmpParts.AddRange(prevStation.PartList);
            tmpParts.AddRange(prevStation.GetPreviousStationsParts());
        }
        return tmpParts;
    }



    public struct StationData
    {
        public string Name { get; set; }
        public List<Part> PartList { get; set; }
        public List<Station> PreviousStations { get; set; }
        public float[] Position;
        public float[] Rotation;

    }

    public static Station stationDeserializer(string json)
    {

        StationData stationData = JsonConvert.DeserializeObject<StationData>(json);
        Station station = new Station(stationData.PartList, stationData.PreviousStations);
        station.Position = new Vector3(stationData.Position[0], stationData.Position[1], stationData.Position[2]);
        station.Rotation = new Quaternion(stationData.Position[0], stationData.Position[1], stationData.Position[2], stationData.Position[3]);
        return station;
    }
    public string stationSerializer()
    {
        StationData stationData = new StationData
        {
            Name = this.Name,
            PartList = this.PartList,
            PreviousStations = this.PreviousStations,
            Position = new float[3] { this.Position.x, this.Position.y, this.Position.z },
            Rotation = new float[4] { this.Rotation.x, this.Rotation.y, this.Rotation.z, this.Rotation.w }
        };

        return JsonConvert.SerializeObject(stationData, Formatting.Indented);
    }

}
