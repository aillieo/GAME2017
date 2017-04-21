using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelBase : MonoBehaviour {


	protected GameObject _panelRoot;

	// Use this for initialization
	void Start () {



	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButtonDown (0)) 
		{
			Vector2 pos =  Camera.main.ScreenToWorldPoint (Input.mousePosition);

			// if pos locate outside the panel 
			// set panel inactive

		}

	}
		
}
