using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneDash : MonoBehaviour {


	GameObject _UIRoot;
	public GameObject _panelUserData;


	// Use this for initialization
	void Start () {

		_UIRoot = GameObject.Find ("DashUIRoot");
		_panelUserData = Instantiate(_panelUserData);
		_panelUserData.transform.SetParent (_UIRoot.transform,false);
		_panelUserData.SetActive (false);




		_UIRoot.SetActive (false);


		if (!GAME2017.UserDataManager.Instance.IsInited()) {

			// wait till user data inited
		}

		if (GAME2017.UserDataManager.Instance.IsNewUser) 
		{
			// show role select view
		}
		else
		{
			// display user data
			_UIRoot.SetActive (true);
		}


	
	}
	
	// Update is called once per frame
	void Update () {



	}



	public void onButtonUserData()
	{
		_panelUserData.SetActive(true);
	}

}
