using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TurnController : MonoBehaviour {
	public static List<attachableUnitDetails> playerUnits = new List<attachableUnitDetails>();
	public static List<attachableUnitDetails> compUnits =  new List<attachableUnitDetails>();
	public static int turnCount = 0;
}
