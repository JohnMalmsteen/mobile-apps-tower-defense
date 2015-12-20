using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GlobalVars
{
    public static bool PLACE_MODE = false;

    public static int PlacedCount = 0;
    public static int StartGold = 1000;
    public static int GoldCount = 1000;
    public static int MAX_UNITS = 3;
    public static int GridSize = 10;
    public static List<GridVector> OccupiedGrid = new List<GridVector>();
    
    public static bool CheckGridSpace(int x, int z)
    {
        foreach (GridVector gv in OccupiedGrid)
        {
            if (gv.x == x && gv.z == z)
                return false;
        }

        return true;
    }
}
