﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChangeWeapon : Action {
	public Weapon changeWeapon(List<Weapon> currentInventory)
	{
		actionPhase = (int)actionClass.Move;
		/*
		 * Take user input here to select the new weapon from the weapons inventory list
		 */
		return new LongBow();
	}
}
