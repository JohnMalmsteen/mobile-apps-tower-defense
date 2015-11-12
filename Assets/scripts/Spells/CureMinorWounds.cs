using UnityEngine;
using System.Collections;

public class CureMinorWounds : Spell {
	public CureMinorWounds()
	{
		range = 0;
		damage = 1;
		spellType = (int)SpellTypes.Healing;
		damageMultiplier = 1;
		duration = 0;
		savingThrow = (int)SavingThrow.None;
		spellLevel = 0;
		type = (int)actionType.Proximity;
	}
	
	public override int cast(Unit target, int autoFailChance)
	{
		int healthBuff = 1;
		if((int)Random.Range(0, 101) > autoFailChance)
		{
			return 0;
		}
		else
		{
			target.health = ((target.health + healthBuff) > target.maxHealth) ? target.maxHealth : target.health + healthBuff;
			return healthBuff;
		}
	}
}
