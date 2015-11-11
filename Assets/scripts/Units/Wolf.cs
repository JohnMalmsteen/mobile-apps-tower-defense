using UnityEngine;
using System.Collections;

public class Wolf : Unit
{
	public Wolf()
	{
		BAB = 1;
		str = 1;
		dex = 1;
		con = 0;
		armorClass += 2; // adding natural armor bonus;
		numOfAttacks = 1;
	}

	public int UnarmedAttack(Unit target){
		int damage = (int)Random.Range (1, 7);
		damage += str;
		
		int atkRoll = (int)Random.Range (1, 21);
		
		atkRoll += str;
		
		if (atkRoll >= target.armorClass) {
			target.health -= damage;
			if(target.health > 0){
				return 1;
			}else{
				return 2;
			}
		} else {
			return 0;
		}
		
	}
}
