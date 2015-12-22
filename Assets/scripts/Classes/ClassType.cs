using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public abstract class ClassType {
	public string babType = "average";
	public int level = 1;
	public int refSave = 0;
	public int conSave = 0;
	public int willSave = 0;
    public int unitCost = 100;
    public int initiative = Random.Range(1,101);
	public Armor armor = new Armor();
	public abstract void levelUp();
    public GameObject model;
    public GameObject unitBoardModel;
    public GameObject unitButton;
    public Sprite spriteImage;
    public Vector3 gridPosition;
    public GridVector gridVector;
}
