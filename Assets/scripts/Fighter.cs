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

	public void levelUp(int lev){
		if(lev % 2 == 0)
		{
			refSave += 1;
		}
		else if(lev % 3 == 0)
		{
			conSave += 1;
			willSave += 1;
		}
	}

}
