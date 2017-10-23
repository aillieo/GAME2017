using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GAME2017
{
	public class PanelTip : MonoBehaviour {

		public Text _text;

		// Use this for initialization
		void Start () {



		}

		// Update is called once per frame
		void Update () {

		}

		public void Init(string content , float time)
		{
			_text.text = content;

			StartCoroutine(GAME2017.Utils.DelayToInvokeDo(() =>
				{
					DestroyObject(this.gameObject);
				}, time));
		}


	}

}