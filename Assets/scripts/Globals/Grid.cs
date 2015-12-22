using System;
using UnityEngine;
using System.Collections.Generic;

public class Grid : MonoBehaviour
{ 
    private static Color RIGHT = Color.green;
    private static Color WRONG = Color.red;

    void Start() // Adding Occupied spaces
    {
        //GlobalVars.OccupiedGrid.Add(new GridVector(2, 2));
        //GlobalVars.OccupiedGrid.Add(new GridVector(5, 3));
    }

    public static GridVector CheckforHuman(GridVector g)
    {
        foreach (GridVector grid in GlobalVars.OccupiedGrid)
        {
            if ((g.x + 1) == grid.x && g.z == grid.z)
            {
                return grid;
            }

            if ((g.x - 1) == grid.x && g.z == grid.z)
            {
                return grid;
            }

            if (g.x == grid.x && (g.z + 1) == grid.z)
            {
                return grid;
            }

            if (g.x == grid.x && (g.z - 1) == grid.z)
            {
                return grid;
            }

            ////////////////////////////////////////////////////////////////

            if ((g.x + 1) == grid.x && (g.z + 1) == grid.z)
            {
                return grid;
            }

            if ((g.x - 1) == grid.x && (g.z + 1) == grid.z)
            {
                return grid;
            }

            if ((g.x + 1) == grid.x && (g.z - 1) == grid.z)
            {
                return grid;
            }

            if ((g.x - 1) == grid.x && (g.z - 1) == grid.z)
            {
                return grid;
            }

        }

        return null;
    }

    public static Color CheckAvailabilityOnGridColor(GridVector gridVector,bool mode)
    {
        foreach (GridVector gv in GlobalVars.OccupiedGrid)
        {            
            if (gv.x == gridVector.x && gv.z == gridVector.z)
            {
                return WRONG;
            }

        }//foreach
                
        if (mode)
        {
            if (GlobalVars.CheckGridHalf(gridVector.z))
                return RIGHT;
            else
                return WRONG;
        }

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
        
        //print(currP.x + "," + currP.z + " : " + otherP.x + "," + otherP.z);

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
