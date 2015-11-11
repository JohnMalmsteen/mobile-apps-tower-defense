using UnityEngine;
using System.Collections;

public interface IRangedAttack {
	int rangedAttack(Unit target, int attackBonus, int damageBonus, int autoFailChance);
}
