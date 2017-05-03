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




		void OnUserInitBack(object msg)
		{
			UIManager.Instance.HideWaiting ();

			ProtoBuf.S2C_UserInit _msg = (ProtoBuf.S2C_UserInit)msg;

			if (_msg.ret != 0) 
			{
				UIManager.Instance.ShowAlert (LanguageManager.Instance.GetErrorMessage(_msg.ret));
				return;
			}

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

				// request heroes data
				ProtoBuf.C2S_GetHeroData chd = new ProtoBuf.C2S_GetHeroData();
				List<string> _heroes = _msg.userData.heroes;
				chd.heroes.AddRange(_heroes);
				GNetwork.CommunicationManager.Instance.SendMessage (GNetwork.MessageTypes.C2S_GetHeroData,chd);

			}

		}


		void OnRoleInitBack(object msg)
		{
			
			UIManager.Instance.HideWaiting ();
			
			ProtoBuf.S2C_RoleInit _msg = msg as ProtoBuf.S2C_RoleInit;
			if (_msg.ret != 0) 
			{
				UIManager.Instance.ShowAlert (LanguageManager.Instance.GetErrorMessage(_msg.ret));				
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

		void OnGetHeroDataBack(object msg)
		{
			UIManager.Instance.HideWaiting ();
			ProtoBuf.S2C_GetHeroData _msg = msg as ProtoBuf.S2C_GetHeroData;
			if (_msg.ret != 0) 
			{
				UIManager.Instance.ShowAlert (LanguageManager.Instance.GetErrorMessage(_msg.ret));		
				return;
			}

			foreach (ProtoBuf.DAT_HeroData phd in _msg.heroes) {
				
				HeroData hd = new HeroData();
				hd.SetData (phd);
				UserDataManager.Instance.SetHeroData (hd);
			}



		}

		void OnNewHeroBack(object msg)
		{
			UIManager.Instance.HideWaiting ();
			ProtoBuf.S2C_NewHero _msg = msg as ProtoBuf.S2C_NewHero;
			if (_msg.ret != 0) 
			{
				UIManager.Instance.ShowAlert (LanguageManager.Instance.GetErrorMessage(_msg.ret));		
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
			MessageDispatcher.Instance.AddHandler (MessageTypes.S2C_UserInit,OnUserInitBack);

			// get hero(es)
			MessageDispatcher.Instance.AddHandler (MessageTypes.S2C_GetHeroData,OnGetHeroDataBack);

			// get new hero
			MessageDispatcher.Instance.AddHandler (MessageTypes.S2C_NewHero, OnNewHeroBack);

		}


	}

}