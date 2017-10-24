using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GAME2017
{
	public class PanelDashBase : MonoBehaviour {

		public Text _textNickname;
		public Text _textLv;
		public Text _textGold;
		public Text _textGem;
		public Text _textHeroTotal;
		public Text _textKey;
		public RawImage _roleAvatar;

		// Use this for initialization
		void Start () {

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
            _roleAvatar.texture = SpriteTextureManager.Instance.GetRoleAvatarByID(userData.roleId);

		}
	}
}