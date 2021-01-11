using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Station
{

    List<Part> partList;
    List<Station> previousStations;
    Vector3 Position { get; set; }
    Quaternion Rotation { get; set; }

    public Station(List<Part> partList, List<Station> previousStations)
    {
        this.partList = partList;
        this.previousStations = previousStations;
    }

    public Station(List<Part> partList, Station previousStation)
    {
        this.partList = partList;
        previousStations = new List<Station>
        {
            previousStation
        };
    }

    public Station(List<Part> partList)
    {
        this.partList = partList;
        previousStations = null;
    }

    public bool AddPart(Part part)
    {
        if (!partList.Contains(part))
        {
            partList.Add(part);
            return true;
        }
        return false;
    }

    public bool RemovePart(Part part)
    {
        return partList.Remove(part);
    }
}
