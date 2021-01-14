using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.Newtonsoft.Json;

public class Part
{
    public string Name { get; }
    public int PartID { get; }
    public bool IsPlaced { get; set; }

    public static List<Part> Parts { get; set; }
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

        this.PartID = partId;
    }



    public override bool Equals(object obj)
    {
        Part part = (Part)obj;
        if (part.Name.Equals(this.Name) && part.PartID == this.PartID && part.IsPlaced == this.IsPlaced)
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
