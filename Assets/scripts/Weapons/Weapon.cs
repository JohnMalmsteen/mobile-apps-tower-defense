using UnityEngine;
using System.Collections;
using Newtonsoft.Json;

public abstract class Weapon : Action{
	public int range;
	public int damage;
	public int minCrit;
	public int critEffect;
	public bool ranged;
    public abstract int WeaponAttack(Unit target, int attackBonus, int damageBuff);
}
