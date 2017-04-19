using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageDispatcher :SingletonMonoBehaviour<MessageDispatcher>{

	public delegate void MsgHandler(object data);

    private Dictionary<int, MsgHandler> _handlerDic = new Dictionary<int, MsgHandler>();
    private Queue<KeyValuePair<int, object>> _msgQueue = new Queue<KeyValuePair<int, object>>();

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (_msgQueue.Count > 0)
		{
			while (_msgQueue.Count > 0)
			{
				KeyValuePair<int, object> msgStruct = _msgQueue.Dequeue();
				if (_handlerDic.ContainsKey(msgStruct.Key))
				{
					_handlerDic[msgStruct.Key](msgStruct.Value);
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

	public void AddMessage(int messageType, object msg)
	{
		_msgQueue.Enqueue (new KeyValuePair<int,object>(messageType,msg));
	}
}
