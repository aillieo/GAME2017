using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ProtoBuf;
using GNetwork;
using UnityEngine.SceneManagement;

namespace GAME2017
{

    public class SceneLogin : MonoBehaviour
    {


        Text _tip;
        Text _username;
        Text _password;


        // Use this for initialization
        void Start()
        {

            _tip = GameObject.Find("TipLabel").GetComponent<Text>();
            _username = GameObject.Find("InputUsername/Text").GetComponent<Text>();
            _password = GameObject.Find("InputPassword/Text").GetComponent<Text>();

			MessageDispatcher.Instance.AddHandler (MessageTypes.S2C_Login,handleMessage);

        }

        // Update is called once per frame
        void Update()
        {

        }


        public void onSignInClick()
        {

			bool connected = CommunicationManager.Instance.IsConnected();
			if (!connected)
			{
				connected = CommunicationManager.Instance.Init ();
			}
            if (!connected)
            {
                _tip.text = "网络连接失败";
                return;
            }

            _tip.text = "正在连接";

            ProtoBuf.C2S_Login msg = new ProtoBuf.C2S_Login();
            msg.username = _username.text;
            msg.password = _password.text;
            msg.type = 0;
			CommunicationManager.Instance.SendMessage(MessageTypes.C2S_Login, msg);

        }

        public void onSignUpClick()
        {

			bool connected = CommunicationManager.Instance.IsConnected();
			if (!connected)
			{
				connected = CommunicationManager.Instance.Init ();
			}
			if (!connected)
			{
				_tip.text = "网络连接失败";
				return;
			}

            _tip.text = "正在连接";
            C2S_Login msg = new C2S_Login();
            msg.username = _username.text;
            msg.password = _password.text;
            msg.type = 1;
            CommunicationManager.Instance.SendMessage(MessageTypes.C2S_Login, msg);

        }


        public void handleMessage(object msg)
        {
			S2C_Login _msg = msg as S2C_Login;
			if (_msg.ret != 0) 
			{
				_tip.text = "error code " + _msg.ret.ToString ();				
			} 
			else 
			{
				_tip.text = "uid: " + _msg.uid;
				GAME2017.UserData.Instance.Init (_msg.uid,_msg.code);
				//SceneManager.LoadScene ("SceneDash");
			}
				
        }



    }
}