using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public List<Part> getPreviousStationsParts()
    {
        List<Part> tmpParts = new List<Part>();
        foreach(Station prevStation in PreviousStations)
        {
            tmpParts.AddRange(prevStation.PartList);
            tmpParts.AddRange(prevStation.getPreviousStationsParts());
        }
        return tmpParts;
    }
}
