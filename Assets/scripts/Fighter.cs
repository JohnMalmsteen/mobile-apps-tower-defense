using UnityEngine;
using System.Collections;

public class Fighter : ClassType {

	public Fighter()
	{
		refSave = 2;
		conSave = 0;
		willSave = 0;
		babType = "good";
	}

	public override void levelUp(){
		if(level % 2 == 0)
		{
			refSave += 1;
		}
		else if(level % 3 == 0)
		{
			conSave += 1;
			willSave += 1;
		}
	}

}
