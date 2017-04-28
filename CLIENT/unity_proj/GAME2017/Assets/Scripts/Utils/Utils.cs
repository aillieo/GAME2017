using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace GAME2017
{
	public class Utils{


		public static IEnumerator DelayToInvokeDo(Action action, float delaySeconds)  
		{
			yield return new WaitForSeconds(delaySeconds);
			action();
		}


	}
}
