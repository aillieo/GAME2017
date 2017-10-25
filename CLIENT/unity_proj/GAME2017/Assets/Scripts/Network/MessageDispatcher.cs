using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GNetwork
{
	public class MessageDispatcher :SingletonMonoBehaviour<MessageDispatcher>{

		public delegate void MsgHandler(object data);

		private Dictionary<int, MsgHandler> _handlerDic = new Dictionary<int, MsgHandler>();

		// Use this for initialization
		void Start () {

		}

		// Update is called once per frame
        void Update()
        {
			CommunicationManager.Instance.Check (Time.deltaTime);
        }

		public void AddHandler(int messageType, MsgHandler handler)
		{
			if (_handlerDic.ContainsKey(messageType))
			{
				_handlerDic[messageType] += handler;
			}
			else
			{
				_handlerDic.Add(messageType, handler);
			}
		}
        
	}

}