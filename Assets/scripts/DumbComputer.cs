using UnityEngine;
using System.Collections;

public class DumbComputer : MonoBehaviour
{
    public GameObject compPieces;
    public Material compMat;

    ScriptManager scriptManager;
    SpritesModels spriteModels;

    int x, z;

    public void Start()
    {
        scriptManager = GameObject.Find("ScriptManager").GetComponent<ScriptManager>();
        spriteModels = scriptManager.spriteModels;
    }

    public void PlaceUnits()
    {
        GameObject compUnit;
        attachableUnitDetails deets;

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

                foundPlace = GlobalVars.CheckGridSpace(x, z);
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
        

    }



}
