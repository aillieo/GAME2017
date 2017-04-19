using System.Collections;
using System.Collections.Generic;
using System;

namespace CSSocket{

	public class CommunicationManager: Singleton<CommunicationManager>
	{

        
        bool _isConnected = false;

        
        public bool IsConnected()
        {
            return _isConnected;
        }

		SocketClient _socketClient;

		public bool Init()
		{
			_socketClient = new SocketClient();
            _isConnected = _socketClient.ConnectServer();
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
            byte[] bType = BitConverter.GetBytes(type);

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
            type = BitConverter.ToInt32(data, 0);
            byte[] bRecv = new byte[len - 4];
            Array.Copy(data,4,bRecv,0,len - 4);
            System.IO.MemoryStream stream = new System.IO.MemoryStream(bRecv);
            ParseMessage(type,stream);

		}

		private void ParseMessage(int type, System.IO.MemoryStream stream)
		{
			switch (type) {
			case CSSocket.MessageTypes.S2C_Login:
				{
					ProtoBuf.S2C_Login msg = ProtoBuf.Serializer.Deserialize<ProtoBuf.S2C_Login> (stream);
					MessageDispatcher.Instance.AddMessage (type, msg);
					break;
				}

			}


		}



	}
}
