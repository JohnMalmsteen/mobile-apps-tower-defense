using UnityEngine;
using System.Collections;

public class Move : Action {
	public Move()
	{
		actionPhase = (int)actionClass.Move;
		type = (int)actionType.Ranged;
	}

	public int moveUnit(attachableUnitDetails unit, int [] destination)
	{
		return 0;
	}
}
