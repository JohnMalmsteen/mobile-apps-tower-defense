using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TurnController : MonoBehaviour {
	public static List<attachableUnitDetails> playerUnits = new List<attachableUnitDetails>();
	public static List<attachableUnitDetails> compUnits =  new List<attachableUnitDetails>();
	public static attachableUnitDetails nebinsTower;
	public static int turnCount = 0;
	public static bool levelOver = false;

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
		actionOrder = new Queue<attachableUnitDetails>(combined);

	}

	public int GameLoop(Queue<attachableUnitDetails> actionQueue)
	{
		return 0;
	}
}
