using UnityEngine;
using System.Collections;

public class CureLightWounds : Spell {
	public CureLightWounds()
	{
		range = 0;
		damage = 8;
		spellType = (int)SpellTypes.Healing;
		damageMultiplier = 1;
		duration = 0;
		savingThrow = (int)SavingThrow.None;
		spellLevel = 1;
		type = (int)actionType.Proximity;
		actionPhase = (int)actionClass.Attack;
	}

	public override int cast(Unit target, int autoFailChance)
	{
		int healthBuff = (int)Random.Range(1, damage+1);
		healthBuff += damageMultiplier;
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
