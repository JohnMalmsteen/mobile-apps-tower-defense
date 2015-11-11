using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Human : Unit {

	public ClassType _class;

	public void LevelUp()
	{
		_class.level += 1;
		if(_class.babType == "good")
		{
			BAB += 1;
		}
		else if(_class.babType == "poor")
		{
			if(_class.level % 2 == 0)
			{
				BAB += 1;
			}
		}
		else
		{
			if(_class.level % 3 == 0)
			{
				BAB += 1;
			}
		}
	}

	void Start()
	{
		_class = new Fighter ();
		if(_class.babType == "good")
		{
			BAB += 1;
		}
	}


}
