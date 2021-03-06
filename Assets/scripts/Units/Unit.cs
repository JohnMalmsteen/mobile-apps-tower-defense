﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

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
	public bool inCombat;
	public int inititiative;
	public int movementRange;

    public int pAtk;
    public int cAtk;

	public Unit(){
		hitDie = 8;
		str = GetAbilityScore ();
		dex = GetAbilityScore ();
		con = GetAbilityScore ();
        //health = hitDie + con;

        health = (int)UnityEngine.Random.Range(25, 35);

        maxHealth = health;
		position [0] = 0;
		position [1] = 0;
		armorClass = 10 + dex;
		XP = 0;
		BAB = 0;
		numOfAttacks = 1;
		movementRange = 60;
		inititiative = (int)UnityEngine.Random.Range(1, 21) + dex;
	}

    public int playerAtk()
    {
        return Convert.ToInt32(UnityEngine.Random.Range(3, 10));
    }

    public int compAtk()
    {
        return Convert.ToInt32(UnityEngine.Random.Range(1, 5));
    }

    public void takeDmg(int x)
    {
        health -= x;
    }

    public bool isDead()
    {
        if(health < 0 )
        {
            return true;
        }

        return false;
    }

    public virtual int UnarmedAttack(Unit target){
		int damage = (int)UnityEngine.Random.Range (1, 3);
		damage += str;

		int atkRoll = (int)UnityEngine.Random.Range (1, 21);

		atkRoll += str;

		if (atkRoll >= target.armorClass) 
		{
			target.health -= damage;
			if(target.health > 0)
			{
				return 1;
			}
			else
			{
				return 2;
			}
		} 
		else 
		{
			return 0;
		}

	}

	public List<Unit> isInCombat(List<Unit> enemies)
	{
		List<Unit> threatRangeUnits = new List<Unit>();

		foreach(Unit enemy in enemies)
		{
			if(Math.Abs(enemy.position[0]-position[0]) == 1 && Math.Abs(enemy.position[1]-position[1]) == 1)
			{
				threatRangeUnits.Add(enemy);
				inCombat = true;
			}
		}

		if(threatRangeUnits.Count > 0)
		{
			return threatRangeUnits;
		}

		return null;
	}

	public void Move(int [] destination){

	}

	private int GetAbilityScore(){
		int score = 0;

		int lowest = 7;
		List<int> rolls = new List<int> ();

		for (int i = 0; i < 4; i++) {
			int roll = 0;
			if((roll = (int)UnityEngine.Random.Range(1, 7)) < lowest){
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
