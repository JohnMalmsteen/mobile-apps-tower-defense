using UnityEngine;
using System.Collections;

public class ClassType {
	public string babType;
	public int level;
	public int refSave;
	public int conSave;
	public int willSave;

	public ClassType(){
		refSave = 0;
		conSave = 0;
		willSave = 0;
		babType = "average";
		level = 1;
	}

}
