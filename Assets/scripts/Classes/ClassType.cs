using UnityEngine;
using System.Collections;

public abstract class ClassType {
	public string babType = "average";
	public int level = 1;
	public int refSave = 0;
	public int conSave = 0;
	public int willSave = 0;
	public Armor armor = new Armor();
	public abstract void levelUp();
}
