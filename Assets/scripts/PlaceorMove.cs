using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class PlaceorMove : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{
    GridVector gv;
    private float Height = 0.1f;
    private Vector3 currentPosition;

    ScriptManager scriptManager;
    TurnController turnController;
    DumbComputer dumbComputer;
    MessageController messageController;
    Grid gridscr;

    public Material playMat;
    public GameObject playerPieces;
    public GameObject SelectedTile;
    public Boolean ShowTile = false;

    public PlaceUnits placeunits;

    void Start()
    {
        scriptManager = GameObject.Find("ScriptManager").GetComponent<ScriptManager>();
        turnController = scriptManager.turnController;
        dumbComputer = scriptManager.dumbComputer;
        messageController = scriptManager.messageController;
        gridscr = scriptManager.grid;
        placeunits = scriptManager.placeUnits;

        playerPieces = GameObject.Find("PlayerPieces");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //print(gameObject.name + " " + GlobalVars.PLACE_MODE + " " + GlobalVars.MOUSE);
        
        currentPosition = new Vector3(gv.x, 0, gv.z);

        if (GlobalVars.PLACE_MODE && GlobalVars.MOUSE)
        {
            if (currentPosition.x >= 0.5f && currentPosition.x <= GlobalVars.GridSize + 0.5f && currentPosition.z >= 0.5f && currentPosition.z <= GlobalVars.GridSize + 0.5f) // Mouse Cursor withing Grid
            {
                SelectedTile.gameObject.GetComponent<MeshRenderer>().enabled = true;

                float x, y, z;

                if (currentPosition.x < 1)
                {
                    x = 1;
                }
                else
                {
                    x = (float)Math.Floor(currentPosition.x);
                }

                if (currentPosition.z < 1)
                {
                    z = 1;
                }
                else
                {
                    z = (float)Math.Floor(currentPosition.z);
                }

                y = Height;

                Vector3 finalPosition = new Vector3(x, y, z);
                int xi, zi;

                xi = Convert.ToInt32(x);
                zi = Convert.ToInt32(z);

                GridVector curr = new GridVector(-1, -1);

                if (GlobalVars.CheckGridHalf(zi))
                {
                    if (GlobalVars.PlacedCount < TurnController.playerUnits.Count && GlobalVars.CheckGridSpace(curr, xi, zi))
                    {
                        if (currentPosition.x >= 0.5f && currentPosition.x <= GlobalVars.GridSize + 0.5f && currentPosition.z >= 0.5f && currentPosition.z <= GlobalVars.GridSize + 0.5f)
                        {
                            CreatePlayerPiece(finalPosition, xi, zi);
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

                SelectedTile.GetComponent<MeshRenderer>().sharedMaterial.color = Grid.CheckAvailabilityOnGridColor(null, new GridVector((int)SelectedTile.transform.position.x, (int)SelectedTile.transform.position.z), true);
            }
            else
            {
                SelectedTile.gameObject.GetComponent<MeshRenderer>().enabled = false;
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////

        if (GlobalVars.PLAYER_TURN && GlobalVars.MOUSE)
        {
            //print("player move");

            if (currentPosition.x >= 0 && currentPosition.x <= GlobalVars.GridSize && currentPosition.z >= 0 && currentPosition.z <= GlobalVars.GridSize) // Mouse Cursor withing Grid
            {
                SelectedTile.gameObject.GetComponent<MeshRenderer>().enabled = true;
                
                Vector3 finalPosition = new Vector3(currentPosition.x, currentPosition.y, currentPosition.z);
                int xi, zi;

                xi = Convert.ToInt32(currentPosition.x);
                zi = Convert.ToInt32(currentPosition.z);

                GridVector temp = new GridVector(xi, zi);

                GridVector tempgv = turnController.currentTurnUnit.GetComponent<attachableUnitDetails>()._class.gridVector;

                ////////////////////////////////////////
                
                if (GlobalVars.CheckGridSpace(tempgv, xi, zi))
                {
                    //print("Free Grid space");

                    if (Grid.CheckImmediateAdjacencyOnGrid(tempgv, temp))
                    {
                        walkUnitToSpace(turnController.currentTurnUnit.GetComponent<attachableUnitDetails>()._class.unitBoardModel,finalPosition);

                        GlobalVars.UpdateOccupied(tempgv, temp);

                        turnController.currentTurnUnit.GetComponent<attachableUnitDetails>()._class.gridVector = new GridVector(temp.x, temp.z);

                        turnController.DrawHealth();

                        turnController.unitTurn++;
                    }

                    SelectedTile.gameObject.GetComponent<MeshRenderer>().enabled = false;

                    GlobalVars.PLAYER_TURN = false;
                }
                else
                {

                    if(Grid.CheckImmediateAdjacencyOnGrid(tempgv, temp) && gridscr.CheckforHumanThere(temp))
                    {
                        GlobalVars.PLAYER_TURN = false;

                        print("Attacking");

                        GameObject go = turnController.GetUnitAt(new GridVector(xi, zi));

                        int difference = go.GetComponent<attachableUnitDetails>().unit.health;

                        go.GetComponent<attachableUnitDetails>().unit.takeDmg(turnController.currentTurnUnit.GetComponent<attachableUnitDetails>().unit.playerAtk());

                        difference -= go.GetComponent<attachableUnitDetails>().unit.health;

                        messageController.UpdateStatusMessage("Attacked the computer for: " + difference + "!");

                        dumbComputer.CheckComputerDead(go);

                        AttackAnimation(turnController.currentTurnUnit.GetComponent<attachableUnitDetails>()._class.unitBoardModel, go);

                        turnController.unitTurn++;
                    }
                    else
                        print("Not Attacking: " + Grid.CheckImmediateAdjacencyOnGrid(tempgv, temp) + " : " + gridscr.CheckforHuman(gv) == null);

                    turnController.DrawHealth();
                }

                SelectedTile.transform.position = finalPosition;

                SelectedTile.GetComponent<MeshRenderer>().sharedMaterial.color = Grid.CheckAvailabilityOnGridColor(gv, new GridVector((int)SelectedTile.transform.position.x, (int)SelectedTile.transform.position.z), false);
            }
            else
                SelectedTile.gameObject.GetComponent<MeshRenderer>().enabled = false;

        }
    }

    public void walkUnitToSpace(GameObject unit,Vector3 walkToHere)
    {
        StartCoroutine(WalkAnimation(unit, walkToHere, 8f));
    }

    private IEnumerator WalkAnimation(GameObject unit,Vector3 finishPosition,float time)
    {
        float elapsedTime = 0;
        bool walking = false;
        
        while (elapsedTime < time)
        {
            unit.transform.position = Vector3.Lerp(unit.transform.position, finishPosition, (elapsedTime / time));
            elapsedTime += Time.deltaTime;

            yield return new WaitForEndOfFrame();

            if (!walking)
            {
                walking = true;
                unit.GetComponent<Animator>().Play("Walk Forward");
            }
        }
    }

    private void AttackAnimation(GameObject unit, GameObject victim)
    {
        unit.transform.LookAt(victim.transform);
        unit.GetComponent<Animator>().Play("Attack");
    }

    public void CreatePlayerPiece(Vector3 finalPosition, int xi, int zi)
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

    public void OnPointerDown(PointerEventData eventData)
    {

    }

    public void OnPointerUp(PointerEventData eventData)
    {

    }
    
    public void setGrid(int x,int z)
    {
        gv = new GridVector(x, z);
    }
}
