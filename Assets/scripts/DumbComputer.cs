using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DumbComputer : MonoBehaviour
{
    public GameObject compPieces;
    public Material compMat;

    ScriptManager scriptManager;
    SpritesModels spriteModels;
    TurnController turnController;
    guiController guiController;
    MessageController messageController;
    Grid grid;

    int x, z;

    public void Start()
    {
        scriptManager = GameObject.Find("ScriptManager").GetComponent<ScriptManager>();
        spriteModels = scriptManager.spriteModels;
        turnController = scriptManager.turnController;
        guiController = scriptManager.guiController;
        messageController = scriptManager.messageController;
        
        grid = scriptManager.grid;
    }

    public void FindAndAttack(GameObject enemyCurr)
    {
        GameObject currentEnemy = enemyCurr;
        GameObject closestPlayerUnit = null;
        int closestCost = 100;

        Quaternion q = new Quaternion(0, 0, 0, 0);
        Quaternion q2 = new Quaternion(0, 180.0f, 0, 0);

        foreach (GameObject go in TurnController.playerUnits)
        {
            int x = CostDistance(currentEnemy, go);

            if (x < closestCost)
            {
                closestPlayerUnit = go;
                closestCost = x;
            }
        }

        GridVector e = enemyCurr.GetComponent<attachableUnitDetails>()._class.gridVector;
        GridVector p = closestPlayerUnit.GetComponent<attachableUnitDetails>()._class.gridVector;

        Vector3 finalPosition = Vector3.zero;
        GridVector gv;

        ////////////////////////////////////////////////////////////////////////
        /// MAKE COMPUTER MOVE
        //////////////////////////////////////////////////////////////////////

        bool attackRange = false;

        GridVector close = grid.CheckforHuman(e);

        if (close != null)
        {
            //print("In attack range");
            
            GameObject go = turnController.GetUnitAt(close);
            
            int difference = go.GetComponent<attachableUnitDetails>().unit.health;

            //print("Health Before: " + go.GetComponent<attachableUnitDetails>().unit.health);

            go.GetComponent<attachableUnitDetails>().unit.takeDmg(enemyCurr.GetComponent<attachableUnitDetails>().unit.compAtk());

            //print("Health After: " + go.GetComponent<attachableUnitDetails>().unit.health);

            difference -= go.GetComponent<attachableUnitDetails>().unit.health;

            messageController.UpdateStatusMessage("Attacked by player for: " + difference + "!");

            CheckPlayerDead(go);

            attackRange = true;
        }
        
        if(!attackRange)
        {
            //print("Computer Compare: " + e.z + " " + p.z);

            GridVector curr = new GridVector(-1, -1);

            if (e.z < p.z)
            {
                gv = new GridVector(e.x, e.z + 1);
                
                if (GlobalVars.CheckGridSpace(curr,gv.x, gv.z))
                {
                    //print("1");

                    finalPosition = new Vector3(e.x, 0, e.z + 1);
                    enemyCurr.GetComponent<attachableUnitDetails>()._class.unitBoardModel.transform.position = finalPosition;
                    GlobalVars.UpdateOccupied(enemyCurr.GetComponent<attachableUnitDetails>()._class.gridVector, gv);

                    enemyCurr.GetComponent<attachableUnitDetails>()._class.gridVector = gv;
                }
            }
            else if(e.z > p.z)
            {
                gv = new GridVector(e.x, e.z - 1);

                if (GlobalVars.CheckGridSpace(curr, gv.x, gv.z))
                {
                    //print("2");

                    finalPosition = new Vector3(e.x, 0, e.z - 1);
                    enemyCurr.GetComponent<attachableUnitDetails>()._class.unitBoardModel.transform.position = finalPosition;
                    GlobalVars.UpdateOccupied(enemyCurr.GetComponent<attachableUnitDetails>()._class.gridVector, gv);
                    
                    enemyCurr.GetComponent<attachableUnitDetails>()._class.gridVector = gv;
                }
            }
            else
            {
                if (e.x > p.x)
                {
                    gv = new GridVector(e.x - 1, e.z);

                    if (GlobalVars.CheckGridSpace(curr, gv.x, gv.z))
                    {
                        //print("3");

                        finalPosition = new Vector3((e.x - 1), 0, e.z);
                        enemyCurr.GetComponent<attachableUnitDetails>()._class.unitBoardModel.transform.position = finalPosition;
                        GlobalVars.UpdateOccupied(enemyCurr.GetComponent<attachableUnitDetails>()._class.gridVector, gv);

                        enemyCurr.GetComponent<attachableUnitDetails>()._class.gridVector = gv;
                    }
                }
                else
                {

                    gv = new GridVector(e.x + 1, e.z);

                    if (GlobalVars.CheckGridSpace(curr, gv.x, gv.z))
                    {
                        //print("4");

                        finalPosition = new Vector3((e.x + 1), 0, e.z);
                        enemyCurr.GetComponent<attachableUnitDetails>()._class.unitBoardModel.transform.position = finalPosition;                        
                        GlobalVars.UpdateOccupied(enemyCurr.GetComponent<attachableUnitDetails>()._class.gridVector, gv);

                        enemyCurr.GetComponent<attachableUnitDetails>()._class.gridVector = gv;
                    }

                }
            }

            //print("Final position: " + finalPosition);
        }

        turnController.DrawHealth();
    }

    public void CheckPlayerDead(GameObject player)
    {
        int k = 0;
        bool found = false;

        if (player.gameObject.GetComponent<attachableUnitDetails>().unit.health < 0)
        {
            foreach (KeyValuePair<int, GameObject> g in TurnController.initiative)
            {
                if (g.Value == player)
                {
                    k = g.Key;
                    found = true;
                }
            }

            if (found)
            {
                //print("Removed Player");

                TurnController.initiative.Remove(k);
            }


            Destroy(player.gameObject.GetComponent<attachableUnitDetails>()._class.unitBoardModel);
            Destroy(player.gameObject.GetComponent<attachableUnitDetails>()._class.unitButton);

            GlobalVars.PlayerAliveCount--;
        }

        if(GlobalVars.PlayerAliveCount < 0)
        {
            Application.LoadLevel("testScene");
        }

        if (GlobalVars.CompAliveCount < 0)
        {
            Application.LoadLevel("testScene");
        }

        /*
        if (player.gameObject.GetComponent<attachableUnitDetails>().unit.isDead())
        {
            print("Destroying Dead Player:");

            Destroy(player);
        }
        */
    }

    public void CheckComputerDead(GameObject player)
    {
        int k = 0;
        bool found = false;

        if (player.gameObject.GetComponent<attachableUnitDetails>().unit.health < 0)
        {
            foreach (KeyValuePair<int, GameObject> g in TurnController.initiative)
            {
                if (g.Value == player)
                {
                    k = g.Key;
                    found = true;
                }
            }

            if (found)
            {
                //print("Removed Player");

                TurnController.initiative.Remove(k);
            }


            Destroy(player.gameObject.GetComponent<attachableUnitDetails>()._class.unitBoardModel);
            Destroy(player.gameObject.GetComponent<attachableUnitDetails>()._class.unitButton);

            GlobalVars.CompAliveCount--;
        }

        if (GlobalVars.PlayerAliveCount < 0)
        {
            Application.LoadLevel("testScene");
        }

        if (GlobalVars.CompAliveCount < 0)
        {
            Application.LoadLevel("testScene");
        }

        /*
        if (player.gameObject.GetComponent<attachableUnitDetails>().unit.isDead())
        {
            print("Destroying Dead Player:");

            Destroy(player);
        }
        */
    }

    public int CostDistance(GameObject enemy,GameObject player)
    {
        int cost = 0;

        GridVector enV = enemy.GetComponent<attachableUnitDetails>()._class.gridVector;
        GridVector pV = enemy.GetComponent<attachableUnitDetails>()._class.gridVector;

        cost = Mathf.Abs(Mathf.Abs(enV.x) - Mathf.Abs(pV.x));

        cost += Mathf.Abs(Mathf.Abs(enV.z) - Mathf.Abs(pV.z));

        return cost;
    }

    public void PlaceUnits()
    {
        GameObject compUnit;
        attachableUnitDetails deets;

        GridVector curr = new GridVector(-1, -1);

        while (GlobalVars.ComputerPlacedCount < GlobalVars.MAX_UNITS)
        {
            compUnit = new GameObject();            
            compUnit.transform.parent = compPieces.transform;

            deets = compUnit.gameObject.AddComponent<attachableUnitDetails>();

            deets.unit = new Unit();
            deets.unit.BAB = 15;
            deets.unit.maxHealth = 30;
            deets.unit.health = 30;
            deets.unit.armorClass = 15;

            switch(Random.Range(0,3))
            {
                case 0:
                    deets._class = new Fighter();
                    deets._class.spriteImage = spriteModels.dwarf_fighter;
                    deets._class.model = spriteModels.model_dwarf_fighter;
                    deets._class.level = 3;
                    break;

                case 1:
                    deets._class = new Ranger();
                    deets._class.spriteImage = spriteModels.dwarf_ranger;
                    deets._class.model = spriteModels.model_dwarf_ranger;
                    deets._class.level = 3;
                    break;

                case 2:
                    deets._class = new Sorcerer();
                    deets._class.spriteImage = spriteModels.dwarf_sorcerer;
                    deets._class.model = spriteModels.model_dwarf_sorcerer;
                    deets._class.level = 3;
                    break;
            }
            
            compUnit.gameObject.name = deets._class.ToString();

            TurnController.compUnits.Add(compUnit.gameObject);

            ////////////////////////////////////////////////////////////////////////////////////

            GameObject unitModel = TurnController.compUnits[GlobalVars.ComputerPlacedCount].gameObject.GetComponent<attachableUnitDetails>()._class.model;
            attachableUnitDetails compModComp = TurnController.compUnits[GlobalVars.ComputerPlacedCount].gameObject.GetComponent<attachableUnitDetails>();

            compModComp.owner = 1;

            unitModel.transform.Find("Tops").GetComponent<SkinnedMeshRenderer>().material = compMat;

            //

            bool foundPlace = false;

            while(!foundPlace)
            {
                x = Random.Range(1, GlobalVars.GridSize);
                z = Random.Range(GlobalVars.GridSize - GlobalVars.StartRowCount, GlobalVars.GridSize);

                foundPlace = GlobalVars.CheckGridSpace(curr, x, z);
            }

            compModComp._class.gridVector = new GridVector(x, z);

            GlobalVars.OccupiedGrid.Add(new GridVector(x, z));

            //print("X: " + x + " Z: " + z);

            Vector3 finalPosition = new Vector3(x , 0, z);

            //

            Quaternion q = new Quaternion(0, 180.0f, 0, 0);

            compModComp._class.unitBoardModel = Instantiate(unitModel, finalPosition, q) as GameObject;

            compModComp._class.unitBoardModel.name = "Computer Model Player: " + TurnController.playerUnits[GlobalVars.ComputerPlacedCount].gameObject.GetComponent<attachableUnitDetails>()._class.model.name;

            compModComp._class.unitBoardModel.gameObject.transform.SetParent(compPieces.transform);
            
            GlobalVars.ComputerPlacedCount++;
        }

        //print("GlobalVars.OccupiedGrid: " + GlobalVars.OccupiedGrid.Count);
    }
}
