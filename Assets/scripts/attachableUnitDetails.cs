using UnityEngine;
using System.Collections;

public class attachableUnitDetails : MonoBehaviour {

	public Weapon currentEquippedWeapon;
	public Unit unit;
	public ClassType _class;

	public void LevelUp()
	{
		_class.level += 1;
		if(_class.babType == "good")
		{
			unit.BAB += 1;
		}
		else if(_class.babType == "poor")
		{
			if(_class.level % 2 == 0)
			{
				unit.BAB += 1;
			}
		}
		else
		{
			if(_class.level % 3 == 0)
			{
				unit.BAB += 1;
			}
		}
		
		if(_class.level % 4 == 0)
		{
			unit.str += 1;
		}

		int healthIncrease = Random.Range(1, unit.hitDie+1) + unit.con;
		unit.maxHealth += healthIncrease;
		unit.health += healthIncrease;
		_class.levelUp();
	}

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
		
		if(_class is Fighter)
		{
			Debug.Log(this.ToString());
			for(int i = 0; i < 5; i++)
				currentEquippedWeapon.WeaponAttack(unit, unit.BAB+unit.str, unit.str);
		}
	}
}
