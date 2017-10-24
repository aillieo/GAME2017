using System.Collections;
using System.Collections.Generic;
using System;
using System.Net;
using UnityEngine;
using GNetwork;


namespace GNetwork{

	public class CommunicationManager: Singleton<CommunicationManager>
	{

		string _serverIP = GConfig.Server.server;
		int _serverPort = GConfig.Server.port;

        bool _isConnected = false;

        SocketClient _socketClient;

        private int _index = 0;

		private Byte[] _buffer = null;
		private int _offset = 0;

        public bool IsConnected()
        {
            return _isConnected;
        }

		public bool Init()
		{
			_socketClient = new SocketClient();
            _isConnected = _socketClient.ConnectServer(_serverIP, _serverPort);
            return _isConnected;
		}

		public bool Init(string ip, int port)
		{
			_serverIP = ip;
			_serverPort = port;

            return Init();
		}

        public int GetMsgIndex()
        {
            return _index;
        }

        public bool SendMessage(int type, ProtoBuf.IExtensible msg)
        {
            if (!_isConnected)
            {
                return false;
            }

            System.IO.MemoryStream stream = new System.IO.MemoryStream ();
            ProtoBuf.Serializer.Serialize(stream , msg);
            byte[] bMsg = stream.ToArray();

            int len = 4 + 4 + bMsg.Length;
			int rLen = IPAddress.HostToNetworkOrder (len);
			byte[] bLen = BitConverter.GetBytes(rLen);
 
			int rIndex = IPAddress.HostToNetworkOrder (_index);
			byte[] bIndex = BitConverter.GetBytes(rIndex);

			int rType = IPAddress.HostToNetworkOrder (type);
			byte[] bType = BitConverter.GetBytes(rType);

            byte[] bSend = new byte[4 + len];
            Array.Copy(bLen, 0, bSend, 0, 4);
            Array.Copy(bIndex, 0, bSend, 4, 4);
            Array.Copy(bType, 0, bSend, 8, 4);
            Array.Copy(bMsg, 0, bSend, 12, len - 8);
            _socketClient.Send(bSend);

            ++_index;

            return true;
        }
			

        public void Disconnect()
        {
            _socketClient = null;
            _isConnected = false;
            GAME2017.UIManager.Instance.ShowAlert("网络连接已断开");
        }


		public void Check(float deltaTime)
		{

			if(!_isConnected)
			{
				return;
			}
				
			lock (_socketClient)
			{
				_socketClient.GetBuffer (ref _buffer, ref _offset);
				int readLen = 0;
				while(_offset - readLen > 4)
				{
					int len = BitConverter.ToInt32(_buffer, 0);
					len = IPAddress.NetworkToHostOrder(len);

					if(_offset >= 4 + len)
					{
						int index = BitConverter.ToInt32(_buffer, 4);
						index = IPAddress.NetworkToHostOrder(index);

						int type = BitConverter.ToInt32(_buffer, 8);
						type = IPAddress.NetworkToHostOrder (type);

						System.IO.MemoryStream stream = new System.IO.MemoryStream(_buffer, 12, len - 8);
						ParseMessage(index, type, stream);

						readLen += (4 + len);
					}

					if(readLen > 0)
					{
						Array.Copy (_buffer, readLen, _buffer, 0, _offset - readLen);
						_offset -= readLen;
					}

				}
			}
		}

		private void ParseMessage(int index, int type, System.IO.MemoryStream stream)
		{

			Debug.Log ("ParseMessage index = " + index.ToString() + "type = " + type.ToString());

			switch (type) 
            {
			case MessageTypes.S2C_Login:
				{
					ProtoBuf.S2C_Login msg = ProtoBuf.Serializer.Deserialize<ProtoBuf.S2C_Login> (stream);
                    MessageDispatcher.Instance.AddMessage (index, type, msg);
					break;
				}
			case MessageTypes.S2C_UserInit:
				{
					ProtoBuf.S2C_UserInit msg = ProtoBuf.Serializer.Deserialize<ProtoBuf.S2C_UserInit> (stream);
                    MessageDispatcher.Instance.AddMessage(index, type, msg);
					break;
				}
			case MessageTypes.S2C_RoleInit:
				{
					ProtoBuf.S2C_RoleInit msg = ProtoBuf.Serializer.Deserialize<ProtoBuf.S2C_RoleInit> (stream);
                    MessageDispatcher.Instance.AddMessage(index, type, msg);
					break;
				}
			case MessageTypes.S2C_NewHero:
				{
					ProtoBuf.S2C_NewHero msg = ProtoBuf.Serializer.Deserialize<ProtoBuf.S2C_NewHero> (stream);
                    MessageDispatcher.Instance.AddMessage(index, type, msg);
					break;
				}
			}


		}


	}
}
