using UnityEngine;
using System.Collections;

public abstract class Spell : Action {
	public enum SpellTypes{Healing, Ranged, Proximity, Self}
	public enum SavingThrow{None, Ref, RefHalf, Con, ConHalf, Will}
	public int spellType;
	public int range;
	public int damage;
	public int damageMultiplier;
	public int duration;
	public int savingThrow;
	public int spellLevel;

	public abstract int cast(Unit target, int autoFailChance);
}

