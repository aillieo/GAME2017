using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GAME2017
{
	public class PadHero : MonoBehaviour {


		HeroData _heroData;

		public Text _heroName;
		public Text _heroLv;
		public Text _heroEleType;
		public RawImage _heroPic;
		public RawImage _heroPicCover;

		// Use this for initialization
		void Start () {

		}

		// Update is called once per frame
		void Update () {

		}

		void OnEnable()
		{
			
		}

		public void SetHeroData (HeroData hd)
		{
			_heroData = hd;
			_heroName.text = hd.id;

		}

		public void OnClick ()
		{

		}


	}

}