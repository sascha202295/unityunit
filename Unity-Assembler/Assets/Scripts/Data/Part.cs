using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Part
{
    string Name { get; }
    int PartID { get; }
    bool IsPlaced { get; set; }

    public Part(string name, int partID)
    {
        this.Name = name;
        this.PartID = partID;
        IsPlaced = false;
    }
}
