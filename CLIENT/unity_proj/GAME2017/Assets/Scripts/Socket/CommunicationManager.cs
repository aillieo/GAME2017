using System.Collections;
using System.Collections.Generic;
using System;

namespace CSSocket{

	public class CommunicationManager{

		private CommunicationManager() {

			Init ();
		}


		public static readonly CommunicationManager instance = new CommunicationManager();

		SocketClient _socketClient;

		public bool Init()
		{

			_socketClient = new SocketClient();
			bool ret = _socketClient.ConnectServer();
			return ret;
		}

		public bool SendMessage(int type, string str )
		{
			Message msg = new Message();
			msg.Type = 0;
			msg.Content = str;


			byte[] bMsg;
			int len = msg.serializeToBytes(out bMsg);
			byte[] bLen = BitConverter.GetBytes(len);
			byte[] bSend = new byte[bMsg.Length + 4];
			Array.Copy(bLen, 0, bSend, 0, 4);
			Array.Copy(bMsg, 0, bSend, 4, len);
			_socketClient.Send(bSend);

			return true;
		}


		public void Receive(byte[] data, int len)
		{
			Message msg = new Message();
			msg.ParseFromBytes(data, len);
			HandleMessage(msg );
		}

		private void HandleMessage(CSSocket.Message msg)
		{
			if(msg.Type == 0)
			{
				// do something
			}

		}



	}
}
