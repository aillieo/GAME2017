using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GAME2017
{
	public class SceneDash : MonoBehaviour {


		GameObject _panelDashBase;
		GameObject _panelRoleInit;

		// Use this for initialization
		void Start () {

			_panelDashBase = GameObject.Find ("PanelDashBase");

			_panelDashBase.SetActive (false);


			if (!GAME2017.UserDataManager.Instance.IsInited()) {

				// wait till user data inited
			}

			if (GAME2017.UserDataManager.Instance.IsNewUser) 
			{

				_panelRoleInit = GameObject.Find ("PanelRoleInit");
				_panelRoleInit.SetActive(true);

			}
			else
			{
				// display user data
				_panelDashBase.SetActive (true);
			}



		}

		// Update is called once per frame
		void Update () {



		}



	}

}