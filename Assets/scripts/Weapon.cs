using UnityEngine;
using System.Collections;

public abstract class Weapon {
	public int range;
	public int damage;
	public int minCrit;
	public int critEffect;

	public abstract int WeaponAttack(Unit target, int attackBonus, int damageBuff);
}
