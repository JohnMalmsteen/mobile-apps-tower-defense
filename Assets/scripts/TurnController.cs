using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TurnController : MonoBehaviour {
	public static List<attachableUnitDetails> playerUnits = new List<attachableUnitDetails>();
	public static List<attachableUnitDetails> compUnits =  new List<attachableUnitDetails>();
	public static attachableUnitDetails nebinsTower;
	public static int turnCount = 0;
	public static bool levelOver = false;
	public static bool playerWin = false;

	public Queue<attachableUnitDetails> actionOrder =  new Queue<attachableUnitDetails>();

	public void Start()
	{
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

	}

	public int GameLoop(Queue<attachableUnitDetails> actionQueue)
	{
		while(!levelOver)
		{
			attachableUnitDetails current = actionQueue.Dequeue();
			int result = Turn (current);
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

	public int Turn(attachableUnitDetails currentUnit)
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
