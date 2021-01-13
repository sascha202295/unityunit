using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.Newtonsoft.Json;

public class Station
{
    public string Name { get; set; }
    public List<Part> PartList { get; }
    public List<Station> PreviousStations { get; set; }
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

    public void PartPlaced(Part part)
    {
        part.IsPlaced = true;
        PartList.Remove(part);
        for(int i = 0; i < PartList.Count; i++)
        {
            if (!PartList[i].IsPlaced)
            {
                PartList.Insert(i, part);
                return;
            }
        }
        PartList.Add(part);
    }


    public struct StationData
    {
        public string Name { get; set; }
        public List<Part> PartList { get; set; }
        public List<string> PreviousStations { get; set; }
        public float[] Position;
        public float[] Rotation;

    }

    public static Station stationDeserializer(string jsonStation, string jsonAllStations)
    {

        StationData stationData = JsonConvert.DeserializeObject<StationData>(jsonStation);

    

        Station station = new Station(stationData.PartList);


        station.Position = new Vector3(stationData.Position[0], stationData.Position[1], stationData.Position[2]);
        station.Rotation = new Quaternion(stationData.Position[0], stationData.Position[1], stationData.Position[2], stationData.Position[3]);

        try
        {
            List<Station> PreviousStations = new List<Station>();
            foreach (var prefStatinon in stationData.PreviousStations)
            {
                PreviousStations.Add(Station.stationDeserializer(prefStatinon, jsonAllStations));
            }
            station.PreviousStations = PreviousStations;
        } catch { }
        return station;
    }


    public string[] stationSerializer()
    {
        List<string> previousStationsNames = new List<string>();
        foreach (var station in this.PreviousStations) {
            if(station.Name!=null)
            previousStationsNames.Add(station.Name);

        }
                
        StationData stationData = new StationData
        {


            Name = this.Name,
            PartList = this.PartList,
            PreviousStations = previousStationsNames,
            Position = new float[3] { this.Position.x, this.Position.y, this.Position.z },
            Rotation = new float[4] { this.Rotation.x, this.Rotation.y, this.Rotation.z, this.Rotation.w }
        };

        return new string[] { stationData.Name,JsonConvert.SerializeObject(stationData, Formatting.Indented) };
    }

}
