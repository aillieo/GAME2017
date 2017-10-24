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
            byte[] bLen = BitConverter.GetBytes(len);
			System.Array.Reverse (bLen);
            int index = MessageDispatcher.Instance.GetMsgIndex();
            byte[] bIndex = BitConverter.GetBytes(index);
            System.Array.Reverse(bIndex);
            byte[] bType = BitConverter.GetBytes(type);
			System.Array.Reverse (bType);

            byte[] bSend = new byte[4 + len];
            Array.Copy(bLen, 0, bSend, 0, 4);
            Array.Copy(bIndex, 0, bSend, 4, 4);
            Array.Copy(bType, 0, bSend, 8, 4);
            Array.Copy(bMsg, 0, bSend, 12, len - 8);
            _socketClient.Send(bSend);

            return true;
        }

		public void Receive(byte[] data, int len)
		{
            int index = 0;
            byte[] bIndex = new byte[4];
            Array.Copy(data, 0, bIndex, 0, 4);
            System.Array.Reverse(bIndex);
            index = BitConverter.ToInt32(bIndex, 0);
            int type = 0;
			byte[] bType = new byte[4];
			Array.Copy(data,4,bType,0,4);
			System.Array.Reverse (bType);
			type = BitConverter.ToInt32(bType, 0);
            byte[] bRecv = new byte[len - 8];
            Array.Copy(data,8,bRecv,0,len - 8);
            System.IO.MemoryStream stream = new System.IO.MemoryStream(bRecv);
            ParseMessage(index,type,stream);

		}

		private void ParseMessage(int index, int type, System.IO.MemoryStream stream)
		{
			switch (type) {
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

		public void Disconnect()
		{
			_socketClient = null;
			_isConnected = false;
			GAME2017.UIManager.Instance.ShowAlert ("网络连接已断开");
		}



	}
}
