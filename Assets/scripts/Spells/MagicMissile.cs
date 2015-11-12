using UnityEngine;
using System.Collections;

public class MagicMissile : Spell, IRangedAttack {
	public MagicMissile()
	{
		range = 60;
		damage = 4;
		spellType = (int)SpellTypes.Ranged;
		damageMultiplier = 1;
		duration = 0;
		savingThrow = (int)SavingThrow.None;
		spellLevel = 1;
		type = (int)actionType.Ranged;
	}

	public int rangedAttack(Unit target, int attackBonus, int damageBonus, int autoFailChance)
	{
		int hitDamage = 0;


		for(int i = 0; i < damageMultiplier; i++)
		{
			hitDamage = (int)Random.Range(1, damage+1);
			hitDamage += 1;
		}

		if((int)Random.Range(1, 101)< autoFailChance)
		{
			return 0;
		}
		else
		{
			target.health -= hitDamage;
		}

		if(target.health < 0)
		{
			return 1;
		}
		else
		{
			return 2;
		}

	}

	public override int cast(Unit target, int autoFailChance)
	{
		int ret = rangedAttack(target, 0, 0, autoFailChance);
		return ret;
	}
}
