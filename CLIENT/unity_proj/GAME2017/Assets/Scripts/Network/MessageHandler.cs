using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProtoBuf;
using GAME2017;

namespace GNetwork
{
    public class MessageHandler : SingletonMonoBehaviour<MessageHandler>
    {

		public delegate void MsgHandler(ProtoBuf.IExtensible msg);

		private Dictionary<int, MsgHandler> _handlerDic = new Dictionary<int, MsgHandler>();

		// Use this for initialization
		void Start () {

            //DontDestroyOnLoad(gameObject);
		}

		// Update is called once per frame
        void Update()
        {
			CommunicationManager.Instance.Check (Time.deltaTime);
        }

		public void AddHandlerForNextRequest(MsgHandler handler)
		{
            int index = CommunicationManager.Instance.GetMsgIndex();

            if (_handlerDic.ContainsKey(index))
			{
                _handlerDic[index] += handler;
			}
			else
			{
                _handlerDic.Add(index, handler);
			}
		}

        void CheckHandlerByIndex(int index, ProtoBuf.IExtensible msg)
        {
            if (_handlerDic.ContainsKey(index))
            {
                _handlerDic[index](msg);
                _handlerDic.Remove(index);
            }
        }

        //////////////////////////////////////////////////////////////////////////

        public static void HandleMsg(int index, int type, S2C_Login resp)
        {
            Messenger.Broadcast<S2C_Login>("S2C_Login", resp);
        }

        public static void HandleMsg(int index, int type, S2C_UserInit resp)
        {
            Messenger.Broadcast<S2C_UserInit>("S2C_UserInit", resp);
        }

        public static void HandleMsg(int index, int type, S2C_RoleInit resp)
        {
            Messenger.Broadcast<S2C_RoleInit>("S2C_RoleInit", resp);
        }

        public static void HandleMsg(int index, int type, S2C_NewHero resp)
        {
            Messenger.Broadcast<S2C_NewHero>("S2C_NewHero", resp);
        }
        
	}

}