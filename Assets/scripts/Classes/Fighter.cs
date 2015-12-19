using UnityEngine;
using System.Collections;

public class Fighter : ClassType {

    public static int FighterCost = 120;

	public Fighter()
	{
		refSave = 2;
		conSave = 0;
		willSave = 0;
		babType = "good";
		armor.armorClassBonus = 5;
		armor.armorCheckPenality = 1;
		armor.spellFailPenalty = 50;
		armor.name = "Chain Mail";

        unitCost = FighterCost;
    }

	public override void levelUp(){
		if(level % 2 == 0)
		{
			refSave += 1;
		}
		else if(level % 3 == 0)
		{
			conSave += 1;
			willSave += 1;
		}
	}

}
