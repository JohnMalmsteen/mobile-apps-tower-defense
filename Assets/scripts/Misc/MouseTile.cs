using UnityEngine;
using System.Collections;
using System;
using UnityEngine.EventSystems;

public class MouseTile : MonoBehaviour
{
    public PlaceUnits placeunits;

    public GameObject SelectedTile;
    public Boolean ShowTile = false;

    private float Height = 0.1f;
    private Vector3 currentPosition;

    void Update()
    {
        if (GlobalVars.PLACE_MODE)
        {
            Vector3 position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.y));

            currentPosition = position;

            if (position.x >= 0.5f && position.x <= GlobalVars.GridSize + 0.5f && position.z >= 0.5f && position.z <= GlobalVars.GridSize + 0.5f) // Mouse Cursor withing Grid
            {
                SelectedTile.gameObject.GetComponent<MeshRenderer>().enabled = true;

                float x, y, z;

                if (position.x < 1)
                {
                    x = 1;
                }
                else
                {
                    x = (float)Math.Floor(position.x);
                }

                if (position.z < 1)
                {
                    z = 1;
                }
                else
                {
                    z = (float)Math.Floor(position.z);
                }

                y = Height;

                Vector3 finalPosition = new Vector3(x, y, z);
                int xi, zi;

                xi = Convert.ToInt32(x);
                zi = Convert.ToInt32(z); 

                ////////////////////////////////////////

                if (Input.GetMouseButtonDown(0))
                {
                    if (GlobalVars.PlacedCount < TurnController.playerUnits.Count && GlobalVars.CheckGridSpace(xi, zi))
                    {
                        if (currentPosition.x >= 0.5f && currentPosition.x <= GlobalVars.GridSize + 0.5f && currentPosition.z >= 0.5f && currentPosition.z <= GlobalVars.GridSize + 0.5f)
                        {
                            GameObject unitModel = TurnController.playerUnits[GlobalVars.PlacedCount].gameObject.GetComponent<attachableUnitDetails>()._class.model;

                            TurnController.playerUnits[GlobalVars.PlacedCount].gameObject.GetComponent<attachableUnitDetails>()._class.unitButton.SetActive(false);

                            TurnController.playerUnits[GlobalVars.PlacedCount].gameObject.GetComponent<attachableUnitDetails>()._class.unitBoardModel = Instantiate(unitModel, finalPosition, transform.rotation) as GameObject;

                            GlobalVars.OccupiedGrid.Add(new GridVector(xi, zi));

                            GlobalVars.PlacedCount++;
                        }
                    }

                    if (GlobalVars.PlacedCount >= TurnController.playerUnits.Count)
                    {
                        GlobalVars.PLACE_MODE = false;
                        SelectedTile.gameObject.GetComponent<MeshRenderer>().enabled = false;
                        placeunits.donePlacing();
                    }

                }

                ////////////////////////////////////////

                SelectedTile.transform.position = finalPosition;

                SelectedTile.GetComponent<MeshRenderer>().material.color = Grid.CheckAvailabilityOnGridColor(new GridVector((int)SelectedTile.transform.position.x, (int)SelectedTile.transform.position.z));
            }
            else
                SelectedTile.gameObject.GetComponent<MeshRenderer>().enabled = false;




        }

    }

}// MouseTile
