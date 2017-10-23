using System;
using System.Net;
using System.Net.Sockets;
using GNetwork;

namespace fake_server
{

	class CommunicationManager
	{

		public CommunicationManager(Socket s)
		{
			_socket = s;
		}

		private Socket _socket;


		public bool SendMessage(int type,global::ProtoBuf.IExtensible msg)
		{

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
			_socket.Send(bSend);

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
			case MessageTypes.C2S_Login:
				{
					ProtoBuf.C2S_Login msg = ProtoBuf.Serializer.Deserialize<ProtoBuf.C2S_Login> (stream);
					HandleMessage (msg);
					break;
				}
			case MessageTypes.C2S_UserInit:
				{
					ProtoBuf.C2S_UserInit msg = ProtoBuf.Serializer.Deserialize<ProtoBuf.C2S_UserInit> (stream);
					HandleMessage (msg);
					break;
				}
			case MessageTypes.C2S_RoleInit:
				{
					ProtoBuf.C2S_RoleInit msg = ProtoBuf.Serializer.Deserialize<ProtoBuf.C2S_RoleInit> (stream);
					HandleMessage (msg);
					break;
				}
			case MessageTypes.C2S_NewHero:
				{
					ProtoBuf.C2S_NewHero msg = ProtoBuf.Serializer.Deserialize<ProtoBuf.C2S_NewHero> (stream);
					HandleMessage (msg);
					break;
				}
			}

		}


		private void HandleMessage(ProtoBuf.C2S_Login msg)
		{
			
		}

		private void HandleMessage(ProtoBuf.C2S_UserInit msg)
		{

		}

		private void HandleMessage(ProtoBuf.C2S_RoleInit msg)
		{

		}

		private void HandleMessage(ProtoBuf.C2S_NewHero msg)
		{

		}
	}
}