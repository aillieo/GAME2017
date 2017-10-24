using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GNetwork
{
	public class MessageDispatcher :SingletonMonoBehaviour<MessageDispatcher>{

		public delegate void MsgHandler(object data);

		private Dictionary<int, MsgHandler> _handlerDic = new Dictionary<int, MsgHandler>();
        private Queue<KeyValuePair<int, ProtoBuf.IExtensible>> _msgQueue = new Queue<KeyValuePair<int, ProtoBuf.IExtensible>>();

		// Use this for initialization
		void Start () {

		}

		// Update is called once per frame
        void Update()
        {
            lock (_msgQueue)
            {
                if (_msgQueue.Count > 0)
                {
                    while (_msgQueue.Count > 0)
                    {
                        KeyValuePair<int, ProtoBuf.IExtensible> msgStruct = _msgQueue.Dequeue();
                        if (_handlerDic.ContainsKey(msgStruct.Key))
                        {
                            _handlerDic[msgStruct.Key](msgStruct.Value);
                        }
                    }
                }
            }
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

        public void AddMessage(int index, int messageType, ProtoBuf.IExtensible msg)
        {
            lock (_msgQueue)
            {
                _msgQueue.Enqueue(new KeyValuePair<int, ProtoBuf.IExtensible>(messageType, msg));
            }
        }
	}

}