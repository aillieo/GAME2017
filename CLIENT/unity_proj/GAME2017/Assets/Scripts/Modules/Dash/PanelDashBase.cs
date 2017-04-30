using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GAME2017
{
	public class PanelDashBase : MonoBehaviour {

		Text _textNickname;
		Text _textLv;
		Text _textGold;
		Text _textGem;
		Text _textHeroTotal;
		Text _textKey;

		// Use this for initialization
		void Start () {

			_textNickname = GameObject.Find ("PanelDashBase/TextNickname").GetComponent<Text>();
			_textLv = GameObject.Find ("PanelDashBase/TextLv").GetComponent<Text>();
			_textGold = GameObject.Find ("PanelDashBase/TextGold").GetComponent<Text>();
			_textGem = GameObject.Find ("PanelDashBase/TextGem").GetComponent<Text>();
			_textHeroTotal = GameObject.Find ("PanelDashBase/TextHeroTotal").GetComponent<Text>();
			_textKey = GameObject.Find ("PanelDashBase/TextKey").GetComponent<Text>();

		}

		// Update is called once per frame
		void Update () {

		}

		public void LoadUserData (){

			UserData userData = UserDataManager.Instance.GetUserData ();
			_textNickname.text = userData.nickname;
			_textLv.text = "lv "+userData.lv.ToString();
			_textGold.text = "金币 "+userData.gold.ToString();
			_textGem.text = "钻石 "+userData.gem.ToString();
			_textHeroTotal.text = "英雄 "+userData.heroes.Count.ToString();
			_textKey.text = "钥匙 "+(userData.keys.air +userData.keys.water + userData.keys.fire + userData.keys.earth).ToString ();

		}
	}
}