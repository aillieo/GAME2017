using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneLogin : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	public void onSignInClick()
	{
		Debug.Log ("Sign in");
		Text tip =  GameObject.Find ("TipLabel") .GetComponent<Text>();
		tip.text = "Sign in";
	}

	public void onSignUpClick()
	{
		//Debug.Log ("Sign up");
		//Text tip =  GameObject.Find ("TipLabel") .GetComponent<Text>();
		//tip.text = "Sign up";

		Text username = GameObject.Find ("InputUsername/Text").GetComponent<Text>();
		CSSocket.CommunicationManager.instance.SendMessage (0,username.text);

	}

}
