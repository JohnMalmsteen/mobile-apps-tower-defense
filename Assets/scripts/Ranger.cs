using UnityEngine;
using System.Collections;

public class Ranger : ClassType {

	public Unit favoredEnemy;

	public Ranger()
	{
		refSave = 2;
		conSave = 2;
		willSave = 0;
		babType = "average";
		armor.armorClassBonus = 3;
		armor.armorCheckPenality = 1;
		armor.spellFailPenalty = 50;
		armor.name = "Hide Armor";
		favoredEnemy = new Human();
	}
	
	public override void levelUp(){
		if(level % 2 == 0)
		{
			refSave += 1;
			conSave += 1;
		}
		else if(level % 3 == 0)
		{
			willSave += 1;
		}
	}

}
