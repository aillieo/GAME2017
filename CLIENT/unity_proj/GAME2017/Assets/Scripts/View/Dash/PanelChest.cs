using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GAME2017
{
	public class PanelChest : MonoBehaviour {

		public Text _textKeyNum1;
		public Text _textKeyNum2;
		public Text _textKeyNum3;
		public Text _textKeyNum4;

		public Toggle _toggle1, _toggle2, _toggle3, _toggle4 ;

		// Use this for initialization
		void Start () {

		}

		void OnEnable()
		{
			
			LoadData ();
		}


		void LoadData()
		{
			UserData ud = UserDataManager.Instance.GetUserData ();
			_textKeyNum1.text = ud.keys.air.ToString();
			_textKeyNum2.text = ud.keys.water.ToString();
			_textKeyNum3.text = ud.keys.fire.ToString();
			_textKeyNum4.text = ud.keys.earth.ToString();

		}

		public void OnUseKeyClick()
		{
			int index = 0;
			bool hasEnoughKey = false;
			UserData ud = UserDataManager.Instance.GetUserData ();

			if (_toggle1.isOn) {
				index = 1;
				if(ud.keys.air >0)
				{
					hasEnoughKey = true;
				}
			} else if (_toggle2.isOn) {
				index = 2;
				if(ud.keys.water >0)
				{
					hasEnoughKey = true;
				}
			} else if (_toggle3.isOn) {
				index = 3;
				if(ud.keys.fire >0)
				{
					hasEnoughKey = true;
				}
			} else if (_toggle4.isOn) {
				index = 4;
				if(ud.keys.earth >0)
				{
					hasEnoughKey = true;
				}
			}


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
