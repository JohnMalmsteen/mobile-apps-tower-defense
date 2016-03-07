using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class GlobalVars
{
    public static bool PLACE_MODE = false;
    public static bool PLAYER_TURN = false;
    public static bool MOUSE = false;

    public static int GridSize = 15;

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
        int count = 0;
        int pos = 0;
        bool found = false;

        //Debug.Log(oldGv.x + " : " + oldGv.z);

        foreach (GridVector gv in OccupiedGrid)
        {
            if (oldGv.x == gv.x && oldGv.z == gv.z)
            {
                pos = count;
                found = true;
            }
            
            count++;
        }

        if (found)
        {
            //Debug.Log("Found");
            OccupiedGrid.RemoveAt(pos);
            OccupiedGrid.Add(new GridVector(newGv.x,newGv.z));
        }
        else
        {
            Debug.Log("Not found");
        }

        //Debug.Log("Done");

        //Debug.Log("Count: " + OccupiedGrid.Count);
    }

    public static bool CheckGridSpace(GridVector curr,int x, int z)
    {
        if (curr.x == x && curr.z == z)
            return false;

        foreach (GridVector gv in OccupiedGrid)
        {
            if (gv.x == x && gv.z == z)
                return false;
        }

        return true;
    }

    public static void Remove(GridVector gv)
    {
        int count = 0;
        int pos = 0;
        bool found = false;

        foreach (GridVector g in OccupiedGrid)
        {
            if(g.x == gv.x && g.z == gv.z)
            {
                pos = count;
                found = true;
            }

            count++;
        }

        if(found)
        {
            OccupiedGrid.RemoveAt(pos);
        }
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
