using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GAME2017
{
	public class PanelAlert : MonoBehaviour {

		public Text _text;

		// Use this for initialization
		void Start () {


		}

		// Update is called once per frame
		void Update () {

		}

		public void SetContent(string content)
		{
			_text.text = content;
		}

		public void OnButtonClick()
		{
			GameObject.DestroyObject (this.gameObject);
		}
	}

}