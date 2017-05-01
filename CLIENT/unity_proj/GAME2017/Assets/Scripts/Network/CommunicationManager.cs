using System.Collections;
using System.Collections.Generic;
using System;
using GNetwork;

namespace GNetwork{

	public class CommunicationManager: Singleton<CommunicationManager>
	{

		string _serverIP = GConfig.Server.server;
		int _serverPort = GConfig.Server.port;

        bool _isConnected = false;

        
        public bool IsConnected()
        {
            return _isConnected;
        }

		SocketClient _socketClient;

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
			_socketClient = new SocketClient();
			_isConnected = _socketClient.ConnectServer(_serverIP, _serverPort);
			return _isConnected;
		}


        public bool SendMessage(int type,global::ProtoBuf.IExtensible msg)
        {
            if (!_isConnected)
            {
                return false;
            }

            System.IO.MemoryStream stream = new System.IO.MemoryStream ();
            ProtoBuf.Serializer.Serialize(stream , msg);
            byte[] bMsg = stream.ToArray();

            int len = 4 + bMsg.Length;
            byte[] bLen = BitConverter.GetBytes(len);
			System.Array.Reverse (bLen);
            byte[] bType = BitConverter.GetBytes(type);
			System.Array.Reverse (bType);

            byte[] bSend = new byte[4 + len];
            Array.Copy(bLen, 0, bSend, 0, 4);
            Array.Copy(bType, 0, bSend, 4, 4);
            Array.Copy(bMsg, 0, bSend, 8, len - 4);
            _socketClient.Send(bSend);

            return true;
        }

		public void Receive(byte[] data, int len)
		{
            int type = 0;
			byte[] bType = new byte[4];
			Array.Copy(data,0,bType,0,4);
			System.Array.Reverse (bType);
			type = BitConverter.ToInt32(bType, 0);
            byte[] bRecv = new byte[len - 4];
            Array.Copy(data,4,bRecv,0,len - 4);
            System.IO.MemoryStream stream = new System.IO.MemoryStream(bRecv);
            ParseMessage(type,stream);

		}

		private void ParseMessage(int type, System.IO.MemoryStream stream)
		{
			switch (type) {
			case MessageTypes.S2C_Login:
				{
					ProtoBuf.S2C_Login msg = ProtoBuf.Serializer.Deserialize<ProtoBuf.S2C_Login> (stream);
					MessageDispatcher.Instance.AddMessage (type, msg);
					break;
				}
			case MessageTypes.S2C_UserData:
				{
					ProtoBuf.S2C_UserData msg = ProtoBuf.Serializer.Deserialize<ProtoBuf.S2C_UserData> (stream);
					MessageDispatcher.Instance.AddMessage (type, msg);
					break;
				}
			case MessageTypes.S2C_RoleInit:
				{
					ProtoBuf.S2C_RoleInit msg = ProtoBuf.Serializer.Deserialize<ProtoBuf.S2C_RoleInit> (stream);
					MessageDispatcher.Instance.AddMessage (type, msg);
					break;
				}
			case MessageTypes.S2C_NewHero:
				{
					ProtoBuf.S2C_NewHero msg = ProtoBuf.Serializer.Deserialize<ProtoBuf.S2C_NewHero> (stream);
					MessageDispatcher.Instance.AddMessage (type, msg);
					break;
				}
			}


		}

		public void Disconnect()
		{
			_socketClient = null;
			_isConnected = false;
			GAME2017.UIManager.Instance.ShowAlert ("网络连接已断开");
		}



	}
}
