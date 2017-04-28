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

			#if USE_LOCAL_DATA


			// load local data

			SceneManager.LoadScene ("SceneDash");

			#else

			bool connected = CommunicationManager.Instance.IsConnected();
			if (!connected)
			{
				connected = CommunicationManager.Instance.Init ();
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
            msg.type = 0;
			CommunicationManager.Instance.SendMessage(MessageTypes.C2S_Login, msg);

			#endif
        }

        public void OnSignUpClick()
        {
			#if USE_LOCAL_DATA

			// init local data

			#else

			bool connected = CommunicationManager.Instance.IsConnected();
			if (!connected)
			{
				connected = CommunicationManager.Instance.Init ();
			}
			if (!connected)
			{
				UIManager.Instance.ShowAlert ("网络连接失败");
				//_tip.text = "网络连接失败";
				return;
			}

            _tip.text = "正在连接";
            C2S_Login msg = new C2S_Login();
            msg.username = _username.text;
            msg.password = _password.text;
            msg.type = 1;
            CommunicationManager.Instance.SendMessage(MessageTypes.C2S_Login, msg);

			#endif

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
				//SceneManager.LoadScene ("SceneDash");
			}
				
        }



    }
}