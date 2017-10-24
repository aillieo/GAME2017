using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ProtoBuf;

namespace GAME2017
{
	public class PanelChest : MonoBehaviour {


		// Use this for initialization
		void Start () {

		}

		void OnEnable()
		{
			
			LoadData ();
		}


		void LoadData()
		{
			DAT_UserData ud = UserDataManager.Instance.GetUserData ();
            // gold  or  gems
		}

		public void OnUseKeyClick()
		{
			int index = 0;
			bool hasEnoughKey = false;
            DAT_UserData ud = UserDataManager.Instance.GetUserData();


			if (index > 0) {

				if (hasEnoughKey) {

					Debug.Log ("KeyType " + index);

					ProtoBuf.C2S_NewHero msg = new ProtoBuf.C2S_NewHero ();
					//msg.keyType = index;
					GNetwork.CommunicationManager.Instance.SendMessage (GNetwork.MessageTypes.C2S_NewHero, msg);
					UIManager.Instance.ShowWaiting ();
				} 
				else 
				{
					UIManager.Instance.ShowAlert ("没有足够的钥匙");
				}
			}

		}
	}
}
