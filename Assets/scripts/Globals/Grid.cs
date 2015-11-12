using System;
using UnityEngine;
using System.Collections.Generic;

public class Grid : MonoBehaviour
{ 
    private static Color RIGHT = Color.green;
    private static Color WRONG = Color.red;

    void Start() // Adding Occupied spaces
    {
        GlobalVars.OccupiedGrid.Add(new GridVector(2, 2));

        GlobalVars.OccupiedGrid.Add(new GridVector(5, 3));
    }

    public static Color CheckAvailabilityOnGridColor(GridVector gridVector)
    {
        foreach (GridVector gv in GlobalVars.OccupiedGrid)
        {            
            if (gv.x == gridVector.x && gv.z == gridVector.z)
            {
                return WRONG;
            }

        }//foreach

        return RIGHT;

    }// Color

    public static bool CheckAvailabilityOnGrid(GridVector gridVector)
    {
        foreach (GridVector gv in GlobalVars.OccupiedGrid)
        {
            if (gv.x == gridVector.x && gv.z == gridVector.z)
            {
                return false;
            }

        }//foreach

        return true;

    }// Color

    public static bool CheckImmediateAdjacencyOnGrid(GridVector currP,GridVector otherP)
    {
        /// N,S,E,W

        try
        {
            if (otherP.x == currP.x && otherP.z == currP.z + 1) // north
            {
                return true;
            }
        }catch(Exception e){e.ToString();}

        try
        {
            if (otherP.x == currP.x && otherP.z == currP.z - 1) // south
            {
                return true;
            }
        }
        catch (Exception e) { e.ToString(); }

        try
        {
            if (otherP.x == currP.x + 1 && otherP.z == currP.z) // east
            {
                return true;
            }
        }
        catch (Exception e) { e.ToString(); }

        try
        {
            if (otherP.x == currP.x - 1 && otherP.z == currP.z) // west
            {
                return true;
            }
        }
        catch (Exception e) { e.ToString(); }

        /// NE,NW,SE,NW

        try
        {
            if (otherP.x == currP.x - 1 && otherP.z == currP.z + 1) // north east
            {
                return true;
            }
        }
        catch (Exception e) { e.ToString(); }

        try
        {
            if (otherP.x == currP.x + 1 && otherP.z == currP.z + 1) // north west
            {
                return true;
            }
        }
        catch (Exception e) { e.ToString(); }

        try
        {
            if (otherP.x == currP.x - 1 && otherP.z == currP.z - 1) // south east
            {
                return true;
            }
        }
        catch (Exception e) { e.ToString(); }

        try
        {
            if (otherP.x == currP.x + 1 && otherP.z == currP.z - 1) // south west
            {
                return true;
            }
        }
        catch (Exception e) { e.ToString(); }

        ///

        return false;
    }

}// Grid
