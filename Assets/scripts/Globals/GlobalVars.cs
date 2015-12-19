using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GlobalVars
{
    public static int MAX_UNITS = 3;
    public static int GridSize = 10;
    public static List<GridVector> OccupiedGrid = new List<GridVector>();
    public int GoldCount = 0;

    public void AddGold(int amt)
    {
        GoldCount += amt;
    }

    public void LoseGold(int amt)
    {
        GoldCount =- amt;
    }

    public int GetGold()
    {
        return this.GoldCount;
    }
}
