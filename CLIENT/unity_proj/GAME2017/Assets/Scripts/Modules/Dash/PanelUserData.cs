using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GAME2017
{
	public class PanelUserData : MonoBehaviour {

		public Text _textNickname;
		public Text _textLv;
		public Text _textHeroTotal;
		public Text _textRoleStrength;
		public Text _textRoleAgility;
		public Text _textRoleMagic;
		public Text _textRoleEPAir;
		public Text _textRoleEPWater;
		public Text _textRoleEPFire;
		public Text _textRoleEPEarth;
		public RawImage _roleImage;

		// Use this for initialization
		void Start () {
/*
			_textNickname = GameObject.Find ("PanelUserData/TextNickname").GetComponent<Text>();
			_textLv = GameObject.Find("PanelUserData/TextLv").GetComponent<Text>();
			_textHeroTotal = GameObject.Find("PanelUserData/TextHeroTotal").GetComponent<Text>();
			_textRoleStrength = GameObject.Find("PanelUserData/TextRoleStrength").GetComponent<Text>();
			_textRoleAgility = GameObject.Find("PanelUserData/TextRoleAgility").GetComponent<Text>();
			_textRoleMagic = GameObject.Find("PanelUserData/TextRoleMagic").GetComponent<Text>();
			_textRoleEPAir = GameObject.Find("PanelUserData/TextRoleEPAir").GetComponent<Text>();
			_textRoleEPWater = GameObject.Find("PanelUserData/TextRoleEPWater").GetComponent<Text>();
			_textRoleEPFire = GameObject.Find("PanelUserData/TextRoleEPFire").GetComponent<Text>();
			_textRoleEPEarth = GameObject.Find("PanelUserData/TextRoleEPEarth").GetComponent<Text>();
*/


		}

		// Update is called once per frame
		void Update () {

		}

		void OnEnable()
		{
			LoadData ();
		}

		void LoadData()
		{

			UserData ud = UserDataManager.Instance.GetUserData ();

			_textNickname.text = ud.nickname;
			_textLv.text = "lv " + ud.lv.ToString ();
			_textHeroTotal.text = "Heroes " + ud.heroes.Count.ToString ();
			_textRoleStrength.text = "力量 " + ud.strength.ToString ();
			_textRoleAgility.text = "敏捷 " + ud.agility.ToString ();
			_textRoleMagic.text = "魔法 "+ ud.magic.ToString ();
			_textRoleEPAir.text = "气 " +ud.eleProperty.air.ToString ();
			_textRoleEPWater.text = "水 " + ud.eleProperty.water.ToString ();
			_textRoleEPFire.text = "火 " +ud.eleProperty.fire.ToString ();
			_textRoleEPEarth.text = "土 " + ud.eleProperty.earth.ToString ();
			_roleImage.texture = SpriteResourceManager.Instance.GetRoleImageByID (ud.roleId);

		}
	}

}