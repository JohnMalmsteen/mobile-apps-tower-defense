using UnityEngine;
using System.Collections;
using System;
using UnityEngine.EventSystems;

public class MouseTile : MonoBehaviour
{
    ScriptManager scriptManager;
    TurnController turnController;
    DumbComputer dumbComputer;
    guiController guiController;

    public PlaceUnits placeunits;

    public Material playMat;
    public GameObject playerPieces;
    public GameObject SelectedTile;
    public Boolean ShowTile = false;

    private float Height = 0.1f;
    private Vector3 currentPosition;

    void Start()
    {
        scriptManager = GameObject.Find("ScriptManager").GetComponent<ScriptManager>();
        turnController = scriptManager.turnController;
        dumbComputer = scriptManager.dumbComputer;
        guiController = scriptManager.guiController;

        //currentTurnUnit
    }

    void Update()
    {
        if (GlobalVars.PLACE_MODE && GlobalVars.MOUSE)
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

                if (Input.GetMouseButtonDown(0) && GlobalVars.CheckGridHalf(zi))
                {
                    if (GlobalVars.PlacedCount < TurnController.playerUnits.Count && GlobalVars.CheckGridSpace(xi, zi))
                    {
                        if (currentPosition.x >= 0.5f && currentPosition.x <= GlobalVars.GridSize + 0.5f && currentPosition.z >= 0.5f && currentPosition.z <= GlobalVars.GridSize + 0.5f)
                        {
                            CreatePlayerPiece(finalPosition,xi, zi);
                        }
                    }

                    if (GlobalVars.PlacedCount >= TurnController.playerUnits.Count)
                    {
                        GlobalVars.PLACE_MODE = false;
                        SelectedTile.gameObject.GetComponent<MeshRenderer>().enabled = false;
                        placeunits.donePlacing();
                    }

                }

                SelectedTile.transform.position = finalPosition;

                SelectedTile.GetComponent<MeshRenderer>().material.color = Grid.CheckAvailabilityOnGridColor(new GridVector((int)SelectedTile.transform.position.x, (int)SelectedTile.transform.position.z),true);
            }
            else
                SelectedTile.gameObject.GetComponent<MeshRenderer>().enabled = false;

        }

        ////////////////////////////////////////////////////////////////////////////////////////////

        if (GlobalVars.PLAYER_TURN && GlobalVars.MOUSE)
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

                GridVector temp = new GridVector(xi, zi);

                GridVector gv = turnController.currentTurnUnit.GetComponent<attachableUnitDetails>()._class.gridVector;

                ////////////////////////////////////////

                if (Input.GetMouseButtonDown(0))
                {
                    //print("Trying to do user action");

                    if (GlobalVars.CheckGridSpace(xi, zi))
                    {
                        //print("Free Grid space");
                        
                        if(Grid.CheckImmediateAdjacencyOnGrid(gv, temp))
                        {
                            //print("Moving");

                            turnController.currentTurnUnit.GetComponent<attachableUnitDetails>()._class.unitBoardModel.transform.position = finalPosition;

                            GlobalVars.UpdateOccupied(gv,temp);

                            turnController.currentTurnUnit.GetComponent<attachableUnitDetails>()._class.gridVector = new GridVector(temp.x, temp.z);

                            turnController.unitTurn++;
                        }

                        SelectedTile.gameObject.GetComponent<MeshRenderer>().enabled = false;

                        GlobalVars.PLAYER_TURN = false;
                    }
                    else
                    {
                        // Try attack

                        GameObject go = turnController.GetUnitAt(new GridVector(xi, zi));

                        int difference = go.GetComponent<attachableUnitDetails>().unit.health;

                        go.GetComponent<attachableUnitDetails>().unit.takeDmg(turnController.currentTurnUnit.GetComponent<attachableUnitDetails>().unit.playerAtk());

                        difference -= go.GetComponent<attachableUnitDetails>().unit.health;

                        guiController.statusText.text = "Attacked the computer for: " + difference;

                        dumbComputer.CheckComputerDead(go);

                        turnController.unitTurn++;
                    }

                    turnController.DrawHealth();
                }

                ////////////////////////////////////////

                SelectedTile.transform.position = finalPosition;

                SelectedTile.GetComponent<MeshRenderer>().material.color = Grid.CheckAvailabilityOnGridColor(new GridVector((int)SelectedTile.transform.position.x, (int)SelectedTile.transform.position.z), false);
            }
            else
                SelectedTile.gameObject.GetComponent<MeshRenderer>().enabled = false;

        }

    }

    public void CreatePlayerPiece(Vector3 finalPosition,int xi,int zi)
    {
        GameObject unitModel = TurnController.playerUnits[GlobalVars.PlacedCount].gameObject.GetComponent<attachableUnitDetails>()._class.model;

        TurnController.playerUnits[GlobalVars.PlacedCount].gameObject.GetComponent<attachableUnitDetails>()._class.unitButton.SetActive(false);
        TurnController.playerUnits[GlobalVars.PlacedCount].gameObject.GetComponent<attachableUnitDetails>()._class.unitBoardModel = Instantiate(unitModel, finalPosition, transform.rotation) as GameObject;
        TurnController.playerUnits[GlobalVars.PlacedCount].gameObject.GetComponent<attachableUnitDetails>()._class.unitBoardModel.transform.Find("Tops").GetComponent<SkinnedMeshRenderer>().material = playMat;
        TurnController.playerUnits[GlobalVars.PlacedCount].gameObject.GetComponent<attachableUnitDetails>()._class.unitBoardModel.name = "Board Model Player: " + TurnController.playerUnits[GlobalVars.PlacedCount].gameObject.GetComponent<attachableUnitDetails>()._class.model.name;
        TurnController.playerUnits[GlobalVars.PlacedCount].gameObject.GetComponent<attachableUnitDetails>()._class.unitBoardModel.gameObject.transform.SetParent(playerPieces.transform);
        TurnController.playerUnits[GlobalVars.PlacedCount].gameObject.GetComponent<attachableUnitDetails>().owner = 0;

        GlobalVars.OccupiedGrid.Add(new GridVector(xi, zi));

        TurnController.playerUnits[GlobalVars.PlacedCount].gameObject.GetComponent<attachableUnitDetails>()._class.gridVector = new GridVector(xi, zi);

        //print("X: " + xi + " Z: " + zi);

        GlobalVars.PlacedCount++;
    }

}// MouseTile
