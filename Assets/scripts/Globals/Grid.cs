using System;
using UnityEngine;
using System.Collections.Generic;

public class Grid : MonoBehaviour
{ 
    private static Color RIGHT = Color.green;
    private static Color WRONG = Color.red;
    
    TurnController turnController;
    ScriptManager scriptManager;

    void Start() // Adding Occupied spaces
    {
        scriptManager = GameObject.Find("ScriptManager").GetComponent<ScriptManager>();
        turnController = scriptManager.turnController;

        //GlobalVars.OccupiedGrid.Add(new GridVector(2, 2));
        //GlobalVars.OccupiedGrid.Add(new GridVector(5, 3));
    }

    public GridVector CheckforHuman(GridVector g)
    {
        //print("Count: " + GlobalVars.OccupiedGrid.Count);

        foreach (GridVector grid in GlobalVars.OccupiedGrid)
        {
            if (turnController.GetUnitAt(grid) != null)
            {
                //print("Owner: " + turnController.GetUnitAt(grid).GetComponent<attachableUnitDetails>().owner);

                if (turnController.GetUnitAt(grid).GetComponent<attachableUnitDetails>().owner == 0)
                {
                    if (Vector2.Distance(new Vector2(g.x, g.z), new Vector2(grid.x, grid.z)) == 1)
                    {
                        print("Trying: " + Vector2.Distance(new Vector2(g.x, g.z), new Vector2(grid.x, grid.z)));
                        print("Made it: " + turnController.GetUnitAt(grid).GetComponent<attachableUnitDetails>().owner);

                        return grid;

                        //print("This is it: " + grid);
                    }
                }
            }
        }

        return null;
    }

    public static Color CheckAvailabilityOnGridColor(GridVector curr, GridVector gridVector,bool mode)
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
        else
        {
            if ((curr.x + 1) == gridVector.x && curr.z == gridVector.z)
            {
                return RIGHT;
            }

            if ((curr.x - 1) == gridVector.x && curr.z == gridVector.z)
            {
                return RIGHT;
            }

            if (curr.x == gridVector.x && (curr.z + 1) == gridVector.z)
            {
                return RIGHT;
            }

            if (curr.x == gridVector.x && (curr.z - 1) == gridVector.z)
            {
                return RIGHT;
            }

            ////////////////////////////////////////////////////////////////

            if ((curr.x + 1) == gridVector.x && (curr.z + 1) == gridVector.z)
            {
                return RIGHT;
            }

            if ((curr.x - 1) == gridVector.x && (curr.z + 1) == gridVector.z)
            {
                return RIGHT;
            }

            if ((curr.x + 1) == gridVector.x && (curr.z - 1) == gridVector.z)
            {
                return RIGHT;
            }

            if ((curr.x - 1) == gridVector.x && (curr.z - 1) == gridVector.z)
            {
                return RIGHT;
            }
        }

        return WRONG;

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
