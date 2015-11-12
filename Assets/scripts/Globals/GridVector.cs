using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class GridVector
{
    public int x { get; set; }
    public int z { get; set; }

    public GridVector(int x, int z)
    {
        this.x = x;
        this.z = z;
    }

    public override string ToString()
    {
        return "X: " + x + " Z: " + z;
    }
}
