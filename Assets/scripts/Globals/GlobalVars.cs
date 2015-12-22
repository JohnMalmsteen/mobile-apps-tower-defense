using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class GlobalVars
{
    public static bool PLACE_MODE = false;
    public static bool PLAYER_TURN = false;

    public static int GridSize = 10;
    public static int StartRowCount = 3;

    public static int PlayerAliveCount = 0;
    public static int CompAliveCount = 0;

    public static int PlacedCount = 0;
    public static int ComputerPlacedCount = 0;
    public static int StartGold = 1000;
    public static int GoldCount = 1000;
    public static int MAX_UNITS = 3;
    public static List<GridVector> OccupiedGrid = new List<GridVector>();
    
    public static void UpdateOccupied(GridVector oldGv, GridVector newGv)
    {
        foreach (GridVector gv in OccupiedGrid)
        {
            if (oldGv.x == gv.x && oldGv.z == gv.z)
                OccupiedGrid.Remove(gv);
        }

        OccupiedGrid.Add(newGv);
    }

    public static bool CheckGridSpace(int x, int z)
    {
        foreach (GridVector gv in OccupiedGrid)
        {
            if (gv.x == x && gv.z == z)
                return false;
        }

        return true;
    }
    /*
    public static bool GetOccuppiedGridSpace(int x, int z)
    {
        foreach (GridVector gv in OccupiedGrid)
        {
            if (gv.x == x && gv.z == z)
                return false;
        }

    }
    */

    public static bool CheckGridHalf(int zi)
    {
        if (zi <= StartRowCount)
            return true;
        else
            return false;
    }

}
