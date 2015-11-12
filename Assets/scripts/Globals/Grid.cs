using UnityEngine;
using System.Collections.Generic;

public class Grid : MonoBehaviour
{ 
    public static List<GridVector> OccupiedGrid = new List<GridVector>();

    public static Color RIGHT = Color.green;
    public static Color WRONG = Color.red;

    void Start() // Adding Occupied spaces
    {
        OccupiedGrid.Add(new GridVector(2, 2));
        OccupiedGrid.Add(new GridVector(5, 5));
    }

    public static Color CheckAvailabilityOnGridColor(GridVector gridVector)
    {
        foreach (GridVector gv in OccupiedGrid)
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
        foreach (GridVector gv in OccupiedGrid)
        {
            if (gv.x == gridVector.x && gv.z == gridVector.z)
            {
                return false;
            }

        }//foreach

        return true;

    }// Color



}// Grid
