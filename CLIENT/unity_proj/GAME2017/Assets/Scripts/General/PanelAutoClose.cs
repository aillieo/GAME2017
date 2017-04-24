using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelAutoClose : MonoBehaviour {


	protected GameObject _panelRoot;

	// Use this for initialization
	void Start () {

		_panelRoot = this.gameObject;

	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButtonDown (0)) 
		{
			//Vector2 pos =  Camera.main.ScreenToWorldPoint (Input.mousePosition);
			if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject () ){
				_panelRoot.SetActive (false);
				//Debug.Log ("out");
				// if pos locate outside the panel 
				// set panel inactive
			} else {
				//Debug.Log ("in");
			}

		}

	}
		
}
