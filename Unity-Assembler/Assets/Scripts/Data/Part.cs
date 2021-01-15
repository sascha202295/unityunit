using System.Collections.Generic;
using Valve.Newtonsoft.Json;

/// <summary>
/// represents a part of the model
/// </summary>
public class Part
{
    /// <summary>
    /// name of the Part
    /// </summary>
    public string Name { get; }
    /// <summary>
    /// PartID Corresponds with the index of the Part in the model
    /// </summary>
    public int PartID { get; }
    /// <summary>
    /// reflects wether or not a Part has been placed into the model in one of the Stations
    /// </summary>
    public bool IsPlaced { get; set; }

    /// <summary>
    /// static list of all parts of the model; get filled at the start of the program by StationManager
    /// </summary>
    public static List<Part> Parts { get; set; }
    /// <summary>
    /// used to generate PartIDs
    /// </summary>
    private static int partCount = 0;



    public Part(string name)
    {
        this.Name = name;
        this.PartID = partCount;
        IsPlaced = false;
        partCount++;
    }


    [JsonConstructor]
    public Part(string name, int partId)
    {
        this.Name = name;
        this.PartID = partId;
    }



    public override bool Equals(object obj)
    {
        if (obj == null) return false;
        Part objAsPart = obj as Part;
        if (objAsPart == null) return false;

        if (objAsPart.Name.Equals(this.Name) && objAsPart.PartID == this.PartID && objAsPart.IsPlaced == this.IsPlaced)
        {
            return true;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}
