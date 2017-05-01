using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GNetwork;

namespace GAME2017
{
	public class SceneDash : MonoBehaviour {


		public GameObject _panelDashBase;
		public GameObject _panelRoleInit;

		// Use this for initialization
		void Start () {

			//_panelDashBase = GameObject.Find ("PanelDashBase");
			//_panelRoleInit = GameObject.Find ("PanelRoleInit");

			_panelDashBase.SetActive (false);
			_panelRoleInit.SetActive (false);

			UserDataManager.Instance.InitHandler ();

			UserDataManager.Instance.RequestUserData ();

			UIManager.Instance.ShowWaiting ();

			StartCoroutine(	TryLoadUserData ());

		}

		IEnumerator TryLoadUserData()
		{

			yield return new WaitUntil (()=>{return GAME2017.UserDataManager.Instance.HasUserData();});


			UIManager.Instance.HideWaiting ();


			if (GAME2017.UserDataManager.Instance.IsNewUser) 
			{
				UIManager.Instance.OpenPanel (_panelRoleInit);
				GNetwork.MessageDispatcher.Instance.AddHandler (MessageTypes.S2C_RoleInit, OnRoleInitBack);

			}
			else
			{
				DestroyObject (_panelRoleInit.gameObject);
				_panelDashBase.SetActive (true);
				_panelDashBase.GetComponent<PanelDashBase> ().LoadUserData ();
			}


		}

		void OnRoleInitBack(object msg)
		{
			ProtoBuf.S2C_RoleInit _msg = msg as ProtoBuf.S2C_RoleInit;
			if (_msg.ret != 0) 
			{
				UIManager.Instance.ShowAlert ("error code : "+_msg.ret.ToString());				
			}
			else 
			{
				UIManager.Instance.HideWaiting ();
				DestroyObject (_panelRoleInit.gameObject);
				_panelDashBase.SetActive (true);
				UserDataManager.Instance.InitWithData (_msg.userData);
				_panelDashBase.GetComponent<PanelDashBase> ().LoadUserData ();
			}
		}

		// Update is called once per frame
		void Update () {



		}



	}

}