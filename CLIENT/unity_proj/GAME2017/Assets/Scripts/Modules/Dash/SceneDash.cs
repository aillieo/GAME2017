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

			InitHandlers ();

			UserDataManager.Instance.RequestUserData ();

			UIManager.Instance.ShowWaiting ();

		}




		void OnUserDataBack(object msg)
		{
			UIManager.Instance.HideWaiting ();

			ProtoBuf.S2C_UserData _msg = (ProtoBuf.S2C_UserData)msg;
			bool isNewUser = _msg.newUser;
			if (isNewUser) 
			{
				UIManager.Instance.OpenPanel (_panelRoleInit);
				GNetwork.MessageDispatcher.Instance.AddHandler (MessageTypes.S2C_RoleInit, OnRoleInitBack);
			} 
			else 
			{
				UserDataManager.Instance.UpdateUserData(_msg.userData);
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
				UserDataManager.Instance.UpdateUserData (_msg.userData);
				_panelDashBase.GetComponent<PanelDashBase> ().LoadUserData ();
			}
		}

		void OnNewHeroBack(object msg)
		{
			ProtoBuf.S2C_NewHero _msg = msg as ProtoBuf.S2C_NewHero;
			if (_msg.ret != 0) 
			{
				UIManager.Instance.ShowAlert ("error code : "+_msg.ret.ToString());	
				return;
			}

			ProtoBuf.DAT_HeroData phd = _msg.hero;
			HeroData hd = new HeroData();
			hd.SetData (phd);
			UserDataManager.Instance.AddNewHero (hd);

		}

		// Update is called once per frame
		void Update () {



		}



		void InitHandlers()
		{
			// get user data
			GNetwork.MessageDispatcher.Instance.AddHandler (GNetwork.MessageTypes.S2C_UserData,OnUserDataBack);

			// get new hero
			GNetwork.MessageDispatcher.Instance.AddHandler (MessageTypes.S2C_NewHero, OnNewHeroBack);

		}


	}

}