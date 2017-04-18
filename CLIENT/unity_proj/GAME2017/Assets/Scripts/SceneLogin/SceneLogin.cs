using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ProtoBuf;

namespace SceneLogin
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

        }

        // Update is called once per frame
        void Update()
        {

        }


        public void onSignInClick()
        {

            bool connected = CSSocket.CommunicationManager.instance.isConnected();
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
            CSSocket.CommunicationManager.instance.SendMessage(CSSocket.MessageTypes.C2S_Login, msg);

        }

        public void onSignUpClick()
        {

            bool connected = CSSocket.CommunicationManager.instance.isConnected();
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
            CSSocket.CommunicationManager.instance.SendMessage(CSSocket.MessageTypes.C2S_Login, msg);

        }

        public void handleMessage(S2C_Login msg)
        {



        }

    }
}