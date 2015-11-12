using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PredictiveTile : MonoBehaviour
{
    public GameObject PredictTile;

    private List<GameObject> PredictiveTiles = new List<GameObject>();

    void Start()
    {
        StartCoroutine(Waiting());
    }

    IEnumerator Waiting()
    {
        yield return new WaitForSeconds(5.0f);

        ShowMovementSquares(new GridVector(5, 5), 4);

        yield return new WaitForSeconds(5.0f);

        CleanUpTiles();
    }

    void ShowMovementSquares(GridVector CurrentPosition,int MovementCount)
    {
        for (int i = 0; i < MovementCount; i++) // North
        {
            if (Grid.CheckAvailabilityOnGrid(new GridVector(CurrentPosition.x, CurrentPosition.z + i)))
            {
                GameObject temp = Instantiate(PredictTile, new Vector3(CurrentPosition.x, 0, CurrentPosition.z + i), Quaternion.identity) as GameObject;
                PredictiveTiles.Add(temp);
            }

            for (int j = 0; j < MovementCount; j++) // Left
            {
                if (Grid.CheckAvailabilityOnGrid(new GridVector(CurrentPosition.x - j,CurrentPosition.z + i)))
                {
                    GameObject temp = Instantiate(PredictTile, new Vector3(CurrentPosition.x - j, 0, CurrentPosition.z + i), Quaternion.identity) as GameObject;
                    PredictiveTiles.Add(temp);
                }
            }

            for (int k = 0; k < MovementCount; k++) // Right
            {

                if (Grid.CheckAvailabilityOnGrid(new GridVector(CurrentPosition.x + k, CurrentPosition.z + i)))
                {
                    GameObject temp = Instantiate(PredictTile, new Vector3(CurrentPosition.x + k, 0, CurrentPosition.z + i), Quaternion.identity) as GameObject;
                    PredictiveTiles.Add(temp);
                }
            }
        }

        for (int i = 0; i < MovementCount; i++) // South
        {
            if (Grid.CheckAvailabilityOnGrid(new GridVector(CurrentPosition.x, CurrentPosition.z - i)))
            {
                GameObject temp = Instantiate(PredictTile, new Vector3(CurrentPosition.x, 0, CurrentPosition.z - i), Quaternion.identity) as GameObject;
                PredictiveTiles.Add(temp);
            }

            
            for (int j = 0; j < MovementCount; j++) // Left
            {
                if (Grid.CheckAvailabilityOnGrid(new GridVector(CurrentPosition.x - j, CurrentPosition.z - i)))
                {
                    GameObject temp = Instantiate(PredictTile, new Vector3(CurrentPosition.x - j, 0, CurrentPosition.z - i), Quaternion.identity) as GameObject;
                    PredictiveTiles.Add(temp);
                }
            }

            for (int k = 0; k < MovementCount; k++) // Right
            {
                if (Grid.CheckAvailabilityOnGrid(new GridVector(CurrentPosition.x + k, CurrentPosition.z - i)))
                {
                    GameObject temp = Instantiate(PredictTile, new Vector3(CurrentPosition.x + k, 0, CurrentPosition.z - i), Quaternion.identity) as GameObject;
                    PredictiveTiles.Add(temp);
                }
            }
        }

        for (int i = 0; i < MovementCount; i++) // East
        {
            if (Grid.CheckAvailabilityOnGrid(new GridVector(CurrentPosition.x + i, CurrentPosition.z)))
            {
                GameObject temp = Instantiate(PredictTile, new Vector3(CurrentPosition.x + i, 0, CurrentPosition.z), Quaternion.identity) as GameObject;
                PredictiveTiles.Add(temp);
            }
        }

        for (int i = 0; i < MovementCount; i++) // West
        {
            if (Grid.CheckAvailabilityOnGrid(new GridVector(CurrentPosition.x - i, CurrentPosition.z)))
            {
                GameObject temp = Instantiate(PredictTile, new Vector3(CurrentPosition.x - i, 0, CurrentPosition.z), Quaternion.identity) as GameObject;
                PredictiveTiles.Add(temp);
            }
        }

    }

    private void CleanUpTiles()
    {
        foreach (GameObject go in PredictiveTiles)
        {
            Destroy(go);
        }

        PredictiveTiles = new List<GameObject>();
    }
}
