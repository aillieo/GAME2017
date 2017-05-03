using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GAME2017
{
	public class PanelHeroes : MonoBehaviour {


		public GameObject _heroDataPanel;
		public PadHero _padHero;
		public GameObject _scrollViewContentRoot;

		// Use this for initialization
		void Start () {



		}

		// Update is called once per frame
		void Update () {

		}

		void OnEnable () {

			LoadData ();
		}

		public void OnClickHeroPad()
		{
			UIManager.Instance.OpenPanel (_heroDataPanel);

		}


		public void LoadData()
		{
			UserData ud = UserDataManager.Instance.GetUserData();
			if (ud.heroes.Count > 0) {

				foreach (string heroUid in ud.heroes) {

					PadHero ph = Instantiate (_padHero);
					ph.gameObject.transform.SetParent (_scrollViewContentRoot.transform);
					ph.SetHeroData (UserDataManager.Instance.GetHeroData (heroUid));

				}

			}
		}
	}
}