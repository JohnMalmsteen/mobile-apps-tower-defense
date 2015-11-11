using UnityEngine;
using System.Collections;

public class LongBow : Weapon, IRangedAttack {

	public LongBow()
	{
		range = 60;
		ranged = true;
		damage = 8;
		minCrit = 20;
		critEffect = 3;
	}

	public override int WeaponAttack(Unit target, int attackBonus, int damageBuff)
	{
		if(damageBuff == -1)
		{
			damageBuff = (damage - 3) * -1;
		}

		int roll = (int)Random.Range(1, 21);
		bool crit = (roll >= minCrit) ? true : false;
		int hitdamage = 0;
		if(crit)
		{
			int critRoll = (int)Random.Range(1, 21);
			if((critRoll + attackBonus) >= target.armorClass)
			{
				hitdamage += (int)Random.Range(1, damage + 1);
				hitdamage += damageBuff;
			}
			else
			{
				crit = false;
			}
		}
		else if(roll + attackBonus >= target.armorClass)
		{
			if(damageBuff < 0)
			{
				hitdamage += (int)Random.Range(1, damage + 1);
				hitdamage += damageBuff;
			}
			hitdamage += (int)Random.Range(1, damage + 1);
			hitdamage += damageBuff;
		}
		else
		{
			Debug.Log("did " + hitdamage + " damage");
			return 0;
		}
		
		target.health -= hitdamage;
		Debug.Log("did " + hitdamage + " damage");
		if(target.health < 0)
		{
			
			return 1;
		}
		else
		{
			return 2;
		}
	}

	public int rangedAttack(Unit target, int attackBonus, int damageBonus, int autoFailChance)
	{
		int ret = WeaponAttack(target, attackBonus, -1);
		return ret;
	}
}
