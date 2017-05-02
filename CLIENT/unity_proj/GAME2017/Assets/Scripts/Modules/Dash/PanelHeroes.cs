using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GAME2017
{
	public class PanelHeroes : MonoBehaviour {


		public GameObject _heroDataPanel;

		// Use this for initialization
		void Start () {



		}

		// Update is called once per frame
		void Update () {

		}

		public void OnClickHeroPad()
		{
			UIManager.Instance.OpenPanel (_heroDataPanel);

		}
	}
}