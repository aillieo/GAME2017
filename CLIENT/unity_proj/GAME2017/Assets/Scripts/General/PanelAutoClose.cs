using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelAutoClose : MonoBehaviour {


	protected GameObject _panel;

	// Use this for initialization
	void Start () {

		_panel = this.gameObject;

	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButtonDown (0)) 
		{
			//Vector2 pos =  Camera.main.ScreenToWorldPoint (Input.mousePosition);
			if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject () ){
				GAME2017.UIManager.Instance.ClosePanel ();
				//Debug.Log ("out");
				// if pos locate outside the panel 
				// set panel inactive
			} else {
				//Debug.Log ("in");
			}

		}

	}
		
}
