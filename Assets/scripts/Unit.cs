using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Unit {
	public int XP;
	public int BAB;
	public int maxHealth;
	public int health;
	public int str;
	public int dex;
	public int con;
	public int hitDie;
	public int armorClass;
	public int [] position = new int[2];
	public int numOfAttacks;

	public Unit(){
		hitDie = 8;
		str = GetAbilityScore ();
		dex = GetAbilityScore ();
		con = GetAbilityScore ();
		health = hitDie + con;
		maxHealth = health;
		position [0] = 0;
		position [1] = 0;
		armorClass = 10 + dex;
		XP = 0;
		BAB = 0;
		numOfAttacks = 1;
	}

	public int UnarmedAttack(Unit target){
		int damage = (int)Random.Range (1, 4);
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

	public void Move(int [] destination){

	}

	private int GetAbilityScore(){
		int score = 0;

		int lowest = 7;
		List<int> rolls = new List<int> ();

		for (int i = 0; i < 4; i++) {
			int roll = 0;
			if((roll = (int)Random.Range(1, 7)) < lowest){
				lowest = roll;
			}

			rolls.Add(roll);
		}

		foreach (int roll in rolls) {
			score += roll;
		};
		score -= lowest;

		score -= 10;

		score = (int)((float)score / 2f);
		return score;
	}

	public override string ToString(){
		string ret = "Unit: \nMax Health: " + maxHealth + "\nCurrent Health: " + health + "\nStrength: " 
			+ str + "\nDex: " + dex + "\nConstituion: " + con + "\nAC: " + armorClass;
		return ret;
	}

}
