﻿using UnityEngine;
using System.Collections;


public class Sorcerer : ClassType {

	public int [] spellsPerDay =  new int[10];

	public Sorcerer(){
		spellsPerDay[0] = 5;
		spellsPerDay[1] = 3;
		for(int i = 2; i < 10; i++)
		{
			spellsPerDay[i] = 0;
		}
		babType = "poor";
	}

	public void levelUp(int lev)
	{
		if(lev % 2 == 0)
		{
			willSave += 1;
		}
		else if(lev % 3 == 0)
		{
			refSave += 1;
			conSave += 1;
		}

		for(int i = 0; i < (int)((float)lev/2f); i++)
		{
			if(spellsPerDay[i] == 0)
			{
				spellsPerDay[i] = 3;
			}
			else
			{
				spellsPerDay[i] = (spellsPerDay[i] < 6) ? spellsPerDay[i] +1 : 6;
			}
		}
	}
}
