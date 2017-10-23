using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GAME2017;

namespace GAME2017
{
	public class UIManager : SingletonMonoBehaviour <UIManager>{


		Stack<GameObject> _panelStack = new Stack<GameObject>();

		public GameObject _UIRoot;

		public PanelWaiting panelWaiting ;
		public PanelAlert panelAlert ;
		public PanelConfirm panelConfirm ;
		public PanelTip panelTip ;

		PanelWaiting _pw = null;

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

		public void ShowWaiting()
		{
			if (_pw == null) {
				_pw = Instantiate (panelWaiting);
				_pw.gameObject.transform.SetParent (_UIRoot.transform, false);
			}
		}


		public void HideWaiting()
		{
			if (_pw == null) {
				return;
			}
			DestroyObject (_pw.gameObject);
		}


		public void ShowAlert(string alertText)
		{
			PanelAlert pa = Instantiate (panelAlert);
			pa.gameObject.transform.SetParent (_UIRoot.transform,false);
			pa.SetContent (alertText);
		}

		public void ShowConfirm(string confirmText, System.Delegate confirmCallback)
		{
			
		}

		public void ShowTip(string tipText, float duration)
		{
			PanelTip pt = Instantiate (panelTip);
			pt.gameObject.transform.SetParent (_UIRoot.transform,false);
			pt.Init (tipText,duration);
		}


		public void ShowTip(string tipText)
		{
			PanelTip pt = Instantiate (panelTip);
			pt.gameObject.transform.SetParent (_UIRoot.transform,false);
			pt.Init (tipText,1.0f);
		}


	}

}