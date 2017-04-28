using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GAME2017;

namespace GAME2017
{
	public class UIManager : SingletonMonoBehaviour <UIManager>{


		Stack<GameObject> _panelStack = new Stack<GameObject>();
		//GameObject _current ;

		public void OpenPanel(GameObject panel)
		{
			panel.SetActive (true);

			_panelStack.Push (panel);
		}

		public void ClosePanel()
		{
			if (_panelStack.Count > 0) 
			{
				_panelStack.Pop ().SetActive (false);
			}
		}

		public void ShowWaiting(float waitTime)
		{
			
		}

		public void ShowAlert(string alertText)
		{
			
		}

		public void ShowConfirm(string confirmText, System.Delegate confirmCallback)
		{
			
		}

		public void ShowTip(string tipText, float duration)
		{
			
		}

	}

}