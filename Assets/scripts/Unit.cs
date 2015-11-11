using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Unit : MonoBehaviour {
	public int maxHealth;
	public int health;
	public int str;
	public int dex;
	public int con;
	public int hitDie;
	public int armorClass;
	public int [] position = new int[2];

	void Start(){
		hitDie = 8;
		str = GetAbilityScore ();
		dex = GetAbilityScore ();
		con = GetAbilityScore ();
		health = hitDie + con;
		maxHealth = health;
		position [0] = 0;
		position [1] = 0;
		armorClass = 10 + dex;
		Debug.Log (this.ToString ());
	}

	public int Attack(Unit target){
		int damage = (int)Random.Range (1, 3);
		damage += str;

		return damage;
	}

	public void Move(int [] destination){

	}

	private int GetAbilityScore(){
		int score = 0;

		int lowest = 7;
		List<int> rolls = new List<int> ();

		for (int i = 0; i < 4; i++) {
			int roll = 0;
			if((roll = (int)Random.Range(1, 6)) < lowest){
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

	public string ToString(){
		string ret = "Unit: \nMax Health: " + maxHealth + "\nCurrent Health: " + health + "\nStrength: " 
			+ str + "\nDex: " + dex + "\nConstituion: " + con + "\nAC: " + armorClass;
		return ret;
	}

}
