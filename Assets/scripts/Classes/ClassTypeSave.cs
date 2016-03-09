using UnityEngine;

public abstract class ClassTypeSave
{
    public string babType = "average";
    public int level = 1;
    public int refSave = 0;
    public int conSave = 0;
    public int willSave = 0;
    public int unitCost = 100;
    public int initiative = Random.Range(1, 101);
    public Armor armor = new Armor();
    public abstract void levelUp();
    public Vector3 gridPosition;
    public GridVector gridVector;
}