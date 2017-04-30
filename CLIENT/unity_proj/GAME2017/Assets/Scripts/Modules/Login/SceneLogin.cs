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


		#if DEBUG_SERVER

		Text _serverIP;
		Text _serverPort;

		#else
		GameObject _debugNode;
		#endif

        // Use this for initialization
        void Start()
        {

            _tip = GameObject.Find("TipLabel").GetComponent<Text>();
            _username = GameObject.Find("InputUsername/Text").GetComponent<Text>();
            _password = GameObject.Find("InputPassword/Text").GetComponent<Text>();

			#if DEBUG_SERVER
			_serverIP = GameObject.Find("DebugNode/InputServerIP/Text").GetComponent<Text>();
			_serverPort = GameObject.Find("DebugNode/InputServerPort/Text").GetComponent<Text>();
			_serverIP.text = GConfig.Server.server;
			_serverPort.text = GConfig.Server.port.ToString();
			Text _serverIPPlaceholder = GameObject.Find("DebugNode/InputServerIP/Placeholder").GetComponent<Text>();
			Text _serverPortPlaceholder = GameObject.Find("DebugNode/InputServerPort/Placeholder").GetComponent<Text>();
			_serverIPPlaceholder.text = GConfig.Server.server;
			_serverPortPlaceholder.text = GConfig.Server.port.ToString();

			#else
			_debugNode = GameObject.Find ("DebugNode");
			_debugNode.SetActive (false);
			#endif


			MessageDispatcher.Instance.AddHandler (MessageTypes.S2C_Login,HandleMessage);

        }

        // Update is called once per frame
        void Update()
        {

        }


        public void OnSignInClick()
        {

			Connect (0);

        }

        public void OnSignUpClick()
        {

			Connect (1);

        }


        public void HandleMessage(object msg)
        {
			S2C_Login _msg = msg as S2C_Login;
			if (_msg.ret != 0) 
			{
				_tip.text = "error code " + _msg.ret.ToString ();				
			} 
			else 
			{
				_tip.text = "uid: " + _msg.uid;
				GAME2017.UserDataManager.Instance.Init (_msg.uid,_msg.code);
				UIManager.Instance.HideWaiting ();
				SceneManager.LoadScene ("SceneDash");
			}
				
        }

		void Connect(int type)
		{
			bool connected = CommunicationManager.Instance.IsConnected();
			if (!connected)
			{
				#if DEBUG_SERVER

				string ip = GConfig.Server.server;
				int port = GConfig.Server.port;

				if(_serverIP.text.Length != 0)
				{
					ip = _serverIP.text;
				}

				if(_serverPort.text.Length != 0)
				{
					port = int.Parse(_serverPort.text);
				}

				connected = CommunicationManager.Instance.Init (ip,port);

				#else
				connected = CommunicationManager.Instance.Init ();

				#endif

			}

			if (!connected)
			{
				UIManager.Instance.ShowAlert ("网络连接失败");
				return;
			}

			_tip.text = "正在连接";

			ProtoBuf.C2S_Login msg = new ProtoBuf.C2S_Login();
			msg.username = _username.text;
			msg.password = _password.text;
			msg.type = type;
			CommunicationManager.Instance.SendMessage(MessageTypes.C2S_Login, msg);

			UIManager.Instance.ShowWaiting ();

		}


    }
}