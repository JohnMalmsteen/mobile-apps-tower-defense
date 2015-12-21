using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class TurnController : MonoBehaviour {
    
    public static List<GameObject> playerUnits = new List<GameObject>();
	public static List<GameObject> compUnits =  new List<GameObject>();

	public static GameObject nebinsTower;
	public static int turnCount = 0;
	public static bool levelOver = false;
	public static bool playerWin = false;

	public static GameObject currentTurnUnit;

	public Queue<GameObject> actionOrder =  new Queue<GameObject>();

    public UnitStore unitstore;
    public Text statusText;

	public void Start()
	{
        /*
		nebinsTower = GameObject.Find("nebinsTower").AddComponent<attachableUnitDetails>();
		nebinsTower.unit = new Unit();
		nebinsTower.unit.BAB = 15;
		nebinsTower.unit.maxHealth = 30;
		nebinsTower.unit.health = 30;
		nebinsTower.unit.armorClass = 15;
		nebinsTower._class = new Sorcerer();
		nebinsTower._class.level =  3;

		List<attachableUnitDetails> combined = new List<attachableUnitDetails>(playerUnits);
		combined.AddRange(compUnits);
		combined.Sort();
		combined.Add(nebinsTower);
		actionOrder = new Queue<attachableUnitDetails>(combined);
        */

        // Calling the SetUpPhase

        SetUpPhase();

	}

    public void SetUpPhase()
    {
        unitstore.display_UnitStoreMenu();
    }

	public int GameLoop(Queue<GameObject> actionQueue)
	{
		while(!levelOver)
		{
			currentTurnUnit = actionQueue.Dequeue();
			int result = Turn (currentTurnUnit);
			if(result == 1)
			{
				levelOver = true;
				playerWin = true;
			}
			else if(result == 2)
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
}
