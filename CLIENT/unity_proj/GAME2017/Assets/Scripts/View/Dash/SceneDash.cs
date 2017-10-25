using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GNetwork;
using ProtoBuf;

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

            Debug.Log("SceneDash.Start");
			UserDataManager.Instance.RequestUserData ();

			UIManager.Instance.ShowWaiting ();

		}




        void OnUserInitBack(S2C_UserInit msg)
		{
			UIManager.Instance.HideWaiting ();

            if (msg.ret != 0) 
			{
                UIManager.Instance.ShowAlert(LanguageManager.Instance.GetErrorMessage(msg.ret));
				return;
			}

            bool isNewUser = msg.newUser;
			if (isNewUser) 
			{
				UIManager.Instance.OpenPanel (_panelRoleInit);
			} 
			else 
			{
				UserDataManager.Instance.UpdateUserData(msg.userData);
				DestroyObject (_panelRoleInit.gameObject);
				_panelDashBase.SetActive (true);
				_panelDashBase.GetComponent<PanelDashBase> ().LoadUserData ();

				// request heroes data
				ProtoBuf.C2S_GetHeroData chd = new ProtoBuf.C2S_GetHeroData();
                List<string> _heroes = msg.userData.heroes;
				chd.heroes.AddRange(_heroes);
				GNetwork.CommunicationManager.Instance.SendMessage (GNetwork.MessageTypes.C2S_GetHeroData,chd);

			}

		}


        void OnRoleInitBack(S2C_RoleInit msg)
		{
			
			UIManager.Instance.HideWaiting ();

            if (msg.ret != 0) 
			{
                UIManager.Instance.ShowAlert(LanguageManager.Instance.GetErrorMessage(msg.ret));				
			}
			else 
			{
				UIManager.Instance.HideWaiting ();
				DestroyObject (_panelRoleInit.gameObject);
				_panelDashBase.SetActive (true);
                UserDataManager.Instance.UpdateUserData(msg.userData);
				_panelDashBase.GetComponent<PanelDashBase> ().LoadUserData ();
			}
		}

        void OnGetHeroDataBack(S2C_GetHeroData msg)
		{
			UIManager.Instance.HideWaiting ();

            if (msg.ret != 0) 
			{
                UIManager.Instance.ShowAlert(LanguageManager.Instance.GetErrorMessage(msg.ret));
				return;
			}

            foreach (ProtoBuf.DAT_HeroData phd in msg.heroes)
            {
                UserDataManager.Instance.SetHeroData(phd);
			}
		}


        void OnNewHeroBack(S2C_NewHero msg)
		{
			UIManager.Instance.HideWaiting ();

            if (msg.ret != 0) 
			{
                UIManager.Instance.ShowAlert(LanguageManager.Instance.GetErrorMessage(msg.ret));		
				return;
			}

            ProtoBuf.DAT_HeroData phd = msg.hero;
            UserDataManager.Instance.AddNewHero(phd);

		}

		// Update is called once per frame
		void Update () {



		}


        void Awake()
        {
            Messenger.Cleanup();
        
        }

        void OnEnable()
        {
            Debug.Log("SceneDash.OnEnable");
            Messenger.AddListener<S2C_RoleInit>("S2C_RoleInit", OnRoleInitBack);
            Messenger.AddListener<S2C_UserInit>("S2C_UserInit", OnUserInitBack);
            Messenger.AddListener<S2C_GetHeroData>("S2C_GetHeroData", OnGetHeroDataBack);
            Messenger.AddListener<S2C_NewHero>("S2C_NewHero", OnNewHeroBack);
        }

        void OnDisable()
        {
            Debug.Log("SceneDash.OnDisable");
            Messenger.RemoveListener<S2C_RoleInit>("S2C_RoleInit", OnRoleInitBack);
            Messenger.RemoveListener<S2C_UserInit>("S2C_UserInit", OnUserInitBack);
            Messenger.RemoveListener<S2C_GetHeroData>("S2C_GetHeroData", OnGetHeroDataBack);
            Messenger.RemoveListener<S2C_NewHero>("S2C_NewHero", OnNewHeroBack);
        }


	}

}