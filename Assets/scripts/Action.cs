using UnityEngine;
using System.Collections;

public abstract class Action {
	public enum actionType{Ranged, Proximity, Self};
	public enum actionClass{Move, Attack};
	public int type;
	public int actionPhase;
}
