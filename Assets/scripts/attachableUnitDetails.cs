using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

public class attachableUnitDetails : MonoBehaviour, IComparer {

	public int owner;
	public int gold;

    public List<Weapon> inventoryWeapons =  new List<Weapon>();
	public Weapon currentEquippedWeapon;
    public Unit unit;
	public ClassType _class;
    public List<Action> availableActions = new List<Action>();

	public void LevelUp()
	{
		_class.level += 1;
		if(_class.babType == "good")
		{
			unit.BAB += 1;
			if(_class.level % 4 == 0)
			{
				unit.str += 1;
			}
		}
		else if(_class.babType == "poor")
		{
			if(_class.level % 2 == 0)
			{
				unit.BAB += 1;
			}

			if(_class.level % 4 == 0)
			{
				unit.con += 1;
			}
		}
		else
		{
			if(_class.level % 3 == 0)
			{
				unit.BAB += 1;
			}

			if(_class.level % 4 == 0)
			{
				unit.dex += 1;
			}
		}

		int healthIncrease = Random.Range(1, unit.hitDie+1) + unit.con;
		unit.maxHealth += healthIncrease;
		unit.health += healthIncrease;
		_class.levelUp();
	}

	public int Compare (object x, object y)
	{
		if(x is attachableUnitDetails && y is attachableUnitDetails)
		{
			attachableUnitDetails ax = (attachableUnitDetails)x;
			attachableUnitDetails ay = (attachableUnitDetails)x;

			if(ax.unit.inititiative == ay.unit.inititiative)
			{
				return 0;
			}
			else if(ax.unit.inititiative > ay.unit.inititiative)
			{
				return 1;
			}
			else
			{
				return -1;
			}
		}
		else return 1;
	}

    /*
	void Start()
	{
		unit = new Unit();
		currentEquippedWeapon = new Longsword();
		_class = new Fighter ();
		if(_class.babType == "good")
		{
			unit.BAB += 1;
			unit.hitDie=10;
			unit.maxHealth += 2;
			unit.health += 2;
		}
		else if(_class.babType == "poor")
		{
			unit.hitDie = 6;
			unit.maxHealth -= 2;
			unit.health -= 2;
		}

		if(_class is Ranger)
		{
			unit.movementRange += 10;
			inventoryWeapons.Add(new Dagger());
			inventoryWeapons.Add(new LongBow());
			currentEquippedWeapon = inventoryWeapons[inventoryWeapons.Count-1];
			availableActions.Add(new ChangeWeapon());
			availableActions.Add(currentEquippedWeapon);
			availableActions.Add(new Move());
		}

		owner = 0;
		gold = 100;
	}
    */
}
