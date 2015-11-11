using UnityEngine;
using System.Collections;

public class Armor {
	public int armorClassBonus;
	public int armorCheckPenality;
	public int spellFailPenalty;
	public string name;

	public Armor()
	{
		armorClassBonus = 0;
		armorCheckPenality = 0;
		spellFailPenalty = 0;
	}
}
