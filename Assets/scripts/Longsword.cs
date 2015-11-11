using UnityEngine;
using System.Collections;

public class Longsword : Weapon {
	public Longsword()
	{
		range = 5;
		damage = 6;
		critEffect = 2;
		minCrit = 20;
	}

	public override int WeaponAttack(Unit target, int attackBonus, int damageBuff)
	{
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
}
