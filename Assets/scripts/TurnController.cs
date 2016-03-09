using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class TurnController : MonoBehaviour
{
    UnitStore unitstore;
    ScriptManager scriptManager;
    DumbComputer dumbComputer;
    guiController guiController;

    public static List<GameObject> playerUnits = new List<GameObject>();
    public static List<GameObject> compUnits = new List<GameObject>();
    public static SortedDictionary<int, GameObject> initiative = new SortedDictionary<int, GameObject>();
    public static GameObject nebinsTower;
    public static int turnCount = 0;
    public static bool levelOver = false;
    public static bool playerWin = false;

    public GameObject currentTurnUnit;
    public GameObject activeTile;
    public GameObject healthCanvas;
    public GameObject healthUIObject;
    public GameObject selectedTile;
    public GameObject obstacle;

    public Queue<GameObject> actionOrder = new Queue<GameObject>();

    attachableUnitDetails nebinsTowerAttachable;
    
    public int unitTurn = 0;

    public void Start()
    {
        scriptManager = GameObject.Find("ScriptManager").GetComponent<ScriptManager>();
        unitstore = scriptManager.unitStore;
        dumbComputer = scriptManager.dumbComputer;
        guiController = scriptManager.guiController;

        addObstacles();

        SetUpPhase();        
    }

    public void SetUpPhase()
    {
        nebinsTower = GameObject.Find("nebinsTower").AddComponent<attachableUnitDetails>().gameObject;
        nebinsTowerAttachable = nebinsTower.GetComponent<attachableUnitDetails>();

        nebinsTowerAttachable.unit = new Unit();
        nebinsTowerAttachable.unit.BAB = 15;
        nebinsTowerAttachable.unit.maxHealth = 30;
        nebinsTowerAttachable.unit.health = 30;
        nebinsTowerAttachable.unit.armorClass = 15;
        nebinsTowerAttachable._class = new Sorcerer();
        nebinsTowerAttachable._class.level = 3;

        /*
		List<attachableUnitDetails> combined = new List<attachableUnitDetails>(playerUnits);
		combined.AddRange(compUnits);
		combined.Sort();
		combined.Add(nebinsTower);
		actionOrder = new Queue<attachableUnitDetails>(combined);
        */

        unitstore.display_UnitStoreMenu();
    }

    public void FinishedPlacing()
    {
        dumbComputer.PlaceUnits();

        GlobalVars.PlayerAliveCount = playerUnits.Count;
        GlobalVars.CompAliveCount = compUnits.Count;

        ShowInitiativeList();

        StartCoroutine(GameLoop());

        DrawHealth();
    }

    IEnumerator GameLoop()
    {
        while (GlobalVars.PlayerAliveCount > 0 && GlobalVars.CompAliveCount > 0)
        {
            if (unitTurn > initiative.Count - 1)
            {
                unitTurn = 0;
            }

            //print("Problem: " + " unitTurn: " + unitTurn + " : " + GetInitUnit(unitTurn).gameObject.GetComponent<attachableUnitDetails>()._class.unitBoardModel);

            activeTile.transform.position = GetInitUnit(unitTurn).gameObject.GetComponent<attachableUnitDetails>()._class.unitBoardModel.transform.position;

            ButtonUpdate(GetInitUnit(unitTurn), unitTurn);

            if (GetInitUnit(unitTurn).GetComponent<attachableUnitDetails>().owner == 0) // IF HUMAN
            {
                GlobalVars.PLAYER_TURN = true;

                currentTurnUnit = GetInitUnit(unitTurn);

                selectedTile.transform.position = currentTurnUnit.transform.position;

                //predictiveTile.ShowMovementSquares(currentTurnUnit.GetComponent<attachableUnitDetails>()._class.gridVector,1);

                //print("Trying Human");

                yield return StartCoroutine(PlayerTurn());

            }
            else // IF COMPUTER
            {
                //print("Trying Computer");

                currentTurnUnit = GetInitUnit(unitTurn);

                dumbComputer.FindAndAttack(currentTurnUnit);

                unitTurn++;
            }

            yield return new WaitForSeconds(1);

        }

    }

    IEnumerator PlayerTurn()
    {
        if (!GlobalVars.PLAYER_TURN)
        {
            print("Checking");
        }

        yield return new WaitForSeconds(1);
    }

    public void addObstacles()
    {
        int turnCount;

        for (int i = 0; i < 8; ++i)
        {
            bool foundPlace = false;
            int x = 0;
            int z = 0;

            turnCount = 0;

            while (!foundPlace || turnCount < 40) // just in case
            {
                x = Random.Range(1, GlobalVars.GridSize);
                z = Random.Range(GlobalVars.StartRowCount + 2, GlobalVars.GridSize);

                foundPlace = GlobalVars.CheckGridSpace(null, x, z);

                turnCount++;
            }

            Instantiate(obstacle, new Vector3(x, 0, z), Quaternion.identity);

            GlobalVars.OccupiedGrid.Add(new GridVector(x, z));
        }
    }

    public void ButtonUpdate(GameObject go, int updateThis)
    {
        foreach (KeyValuePair<int, GameObject> g in initiative)
        {
            g.Value.gameObject.GetComponent<attachableUnitDetails>()._class.unitButton.GetComponent<Outline>().effectDistance = new Vector2(0, 0);
            g.Value.gameObject.GetComponent<attachableUnitDetails>()._class.unitButton.GetComponent<Outline>().effectColor = Color.black;
        }

        int count = 0;

        foreach (KeyValuePair<int, GameObject> g in initiative)
        {
            if (count == updateThis)
            {
                g.Value.gameObject.GetComponent<attachableUnitDetails>()._class.unitButton.GetComponent<Outline>().effectDistance = new Vector2(-4, 4);
                g.Value.gameObject.GetComponent<attachableUnitDetails>()._class.unitButton.GetComponent<Outline>().effectColor = Color.green;
            }

            ++count;
        }
    }

    public GameObject GetUnitAt(GridVector gridVector)
    {
        foreach (GameObject go in compUnits)
        {
            if (go.GetComponent<attachableUnitDetails>()._class.gridVector.x == gridVector.x && go.GetComponent<attachableUnitDetails>()._class.gridVector.z == gridVector.z)
            {
                return go;
            }
        }

        foreach (GameObject go in playerUnits)
        {
            if (go.GetComponent<attachableUnitDetails>()._class.gridVector.x == gridVector.x && go.GetComponent<attachableUnitDetails>()._class.gridVector.z == gridVector.z)
            {
                return go;
            }
        }

        return null;
    }

    public GameObject GetInitUnit(int turn)
    {
        int count = 0;

        //print("initiative: " + initiative.Count + " : " + turn);

        foreach (KeyValuePair<int, GameObject> go in initiative)
        {
            if (count == turn)
            {
                return go.Value;
            }

            ++count;
        }

        //print("Nope");

        return null;
    }
    
    public int GameLoop(Queue<GameObject> actionQueue)
    {
        while (!levelOver)
        {
            currentTurnUnit = actionQueue.Dequeue();
            int result = Turn(currentTurnUnit);
            if (result == 1)
            {
                levelOver = true;
                playerWin = true;
            }
            else if (result == 2)
            {
                levelOver = true;
            }
        }
        return 0;
    }

    public int Turn(GameObject currentUnit)
    {
        /*
		 * Move code goes here
		 * if unit owner == 0
		 * 		Present user with option to move the unit
		 * else
		 * 		move the computer unit
		 * 
		 * The player can also use this action to switch weapon 
		 */

        /*
		 * Attack code goes here
		 * If the player is in combat or has a ranged attack that goes here
		 *
		 */

        /*
		 * If the player has destroyed the tower 
		 * 		return 1;
		 * else if the player has no units left
		 * 		return 2;
		 * else
		 * {
		 */

        return 0;
    }

    public void ShowInitiativeList()
    {
        foreach (GameObject go in playerUnits)
        {
            if (!initiative.ContainsKey(go.gameObject.GetComponent<attachableUnitDetails>()._class.initiative))
                initiative.Add(go.gameObject.GetComponent<attachableUnitDetails>()._class.initiative, go);

            else
            {
                while (initiative.ContainsKey(go.gameObject.GetComponent<attachableUnitDetails>()._class.initiative))
                    go.gameObject.GetComponent<attachableUnitDetails>()._class.initiative++;

                initiative.Add(go.gameObject.GetComponent<attachableUnitDetails>()._class.initiative, go);
            }
        }

        foreach (GameObject og in compUnits)
        {
            if (!initiative.ContainsKey(og.gameObject.GetComponent<attachableUnitDetails>()._class.initiative))
                initiative.Add(og.gameObject.GetComponent<attachableUnitDetails>()._class.initiative, og);
            else
            {
                while (initiative.ContainsKey(og.gameObject.GetComponent<attachableUnitDetails>()._class.initiative))
                    og.gameObject.GetComponent<attachableUnitDetails>()._class.initiative++;

                initiative.Add(og.gameObject.GetComponent<attachableUnitDetails>()._class.initiative, og);
            }
        }

        int count = 0;

        foreach (KeyValuePair<int, GameObject> kv in initiative)
        {
            //print("Key: " + kv.Key);

            guiController.initiativePanel.SetActive(true);

            GameObject ImageButton = new GameObject();
            ImageButton.AddComponent<CanvasRenderer>();
            ImageButton.AddComponent<Image>().sprite = kv.Value.gameObject.GetComponent<attachableUnitDetails>()._class.spriteImage;
            ImageButton.AddComponent<Button>();
            ImageButton.transform.SetParent(guiController.initiativePanel.transform);

            kv.Value.gameObject.GetComponent<attachableUnitDetails>()._class.unitButton = ImageButton;

            if (count == 0)
            {
                ImageButton.AddComponent<Outline>().effectDistance = new Vector2(-4, 4);
                ImageButton.GetComponent<Outline>().effectColor = Color.green;
            }
            else
            {
                ImageButton.AddComponent<Outline>().effectDistance = new Vector2(0, 0);
                ImageButton.GetComponent<Outline>().effectColor = Color.black;
            }

            if (kv.Value.gameObject.GetComponent<attachableUnitDetails>().owner == 1)
                ImageButton.GetComponent<Image>().color = Color.red;

            ++count;
        }
    }

    ////////////////////////////////////////////////////////////

    public void DrawHealth()
    {
        int health = 0;
        float offset = 0.0f;

        try
        {

            foreach (Transform child in healthCanvas.transform)
            {
                GameObject.Destroy(child.gameObject);
            }

            foreach (GameObject go in playerUnits)
            {
                health = go.GetComponent<attachableUnitDetails>().unit.health;

                Vector3 screenPosition = Camera.main.WorldToScreenPoint(go.GetComponent<attachableUnitDetails>()._class.unitBoardModel.transform.position);

                screenPosition = new Vector3(screenPosition.x + offset, screenPosition.y, screenPosition.z);

                GameObject ui = Instantiate(healthUIObject, screenPosition, Quaternion.identity) as GameObject;

                ui.GetComponent<Image>().color = Color.green;

                ui.GetComponentInChildren<Text>().text = "HP: " + health;

                ui.transform.SetParent(healthCanvas.gameObject.transform);
            }

            foreach (GameObject go in compUnits)
            {
                health = go.GetComponent<attachableUnitDetails>().unit.health;

                try
                {
                    Vector3 screenPosition = Camera.main.WorldToScreenPoint(go.GetComponent<attachableUnitDetails>()._class.unitBoardModel.transform.position);

                    screenPosition = new Vector3(screenPosition.x + offset, screenPosition.y, screenPosition.z);

                    GameObject ui = Instantiate(healthUIObject, screenPosition, Quaternion.identity) as GameObject;

                    ui.GetComponent<Image>().color = Color.red;

                    ui.GetComponentInChildren<Text>().text = "HP: " + health;

                    ui.transform.SetParent(healthCanvas.gameObject.transform);
                }
                catch { }
            }
        }
        catch { }

    }
}
