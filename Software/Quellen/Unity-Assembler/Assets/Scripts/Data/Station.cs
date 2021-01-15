using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// represents a station
/// </summary>
public class Station
{
    /// <summary>
    /// name of the station
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// List of Parts in this Station; reflects build order of Parts
    /// </summary>
    public List<Part> PartList { get; }
    /// <summary>
    /// List of Stations this Station is dependant on
    /// </summary>
    public List<Station> PreviousStations { get; set; }
    /// <summary>
    /// position of the Station in the Scene
    /// </summary>
    public Vector3 Position { get; set; }
    /// <summary>
    /// rotation of the Station in the Scene
    /// </summary>
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
        PreviousStations = new List<Station>();
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

    /// <summary>
    /// returns Parts of all previous Stations recursivly
    /// </summary>
    public List<Part> GetPreviousStationsParts()
    {
        List<Part> tmpParts = new List<Part>();
        if (PreviousStations != null)
        {
            foreach (Station prevStation in PreviousStations)
            {
                tmpParts.AddRange(prevStation.PartList);
                tmpParts.AddRange(prevStation.GetPreviousStationsParts());
            }
        }
        return tmpParts;
    }

    /// <summary>
    /// mark a Part as placed and sort PartList
    /// </summary>
    public void PartPlaced(Part part)
    {
        part.IsPlaced = true;
        PartList.Remove(part);
        for (int i = 0; i < PartList.Count; i++)
        {
            if (!PartList[i].IsPlaced)
            {
                PartList.Insert(i, part);
                return;
            }
        }
        PartList.Add(part);
    }

}
