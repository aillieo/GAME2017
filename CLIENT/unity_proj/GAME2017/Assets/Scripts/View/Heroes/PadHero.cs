using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ProtoBuf;

namespace GAME2017
{
	public class PadHero : MonoBehaviour {


		DAT_HeroData _heroData;

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

        public void SetHeroData(DAT_HeroData hd)
		{
			_heroData = hd;
			_heroName.text = hd.heroId;

		}

		public void OnClick ()
		{

		}


	}

}