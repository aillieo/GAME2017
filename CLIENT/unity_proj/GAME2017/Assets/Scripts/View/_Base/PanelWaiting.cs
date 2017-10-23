using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GAME2017
{
	public class PanelWaiting : MonoBehaviour {


		public GameObject loadingImage;

		// Use this for initialization
		void Start () {

		}

		// Update is called once per frame
		void Update () {

			loadingImage.transform.Rotate (new Vector3(0,0,5f));

		}
	}
}