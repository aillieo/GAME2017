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


		public bool SendMessage(int index, int type,global::ProtoBuf.IExtensible msg)
		{

			System.IO.MemoryStream stream = new System.IO.MemoryStream ();
			ProtoBuf.Serializer.Serialize(stream , msg);
			byte[] bMsg = stream.ToArray();

			int len = 4 + 4 + bMsg.Length;
			byte[] bLen = BitConverter.GetBytes(len);
			System.Array.Reverse (bLen);
            byte[] bIndex = BitConverter.GetBytes(index);
            System.Array.Reverse(bIndex);
			byte[] bType = BitConverter.GetBytes(type);
			System.Array.Reverse (bType);

			byte[] bSend = new byte[4 + len];
			Array.Copy(bLen, 0, bSend, 0, 4);
            Array.Copy(bIndex, 0, bSend, 4, 4);
			Array.Copy(bType, 0, bSend, 8, 4);
			Array.Copy(bMsg, 0, bSend, 12, len - 8);
			_socket.Send(bSend);

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
			ParseMessage(index, type, stream);
		}

		private void ParseMessage(int index, int type, System.IO.MemoryStream stream)
		{
			switch (type) {
			case MessageTypes.C2S_Login:
				{
					ProtoBuf.C2S_Login msg = ProtoBuf.Serializer.Deserialize<ProtoBuf.C2S_Login> (stream);
					HandleMessage (index, msg);
					break;
				}
			case MessageTypes.C2S_UserInit:
				{
					ProtoBuf.C2S_UserInit msg = ProtoBuf.Serializer.Deserialize<ProtoBuf.C2S_UserInit> (stream);
                    HandleMessage(index, msg);
					break;
				}
			case MessageTypes.C2S_RoleInit:
				{
					ProtoBuf.C2S_RoleInit msg = ProtoBuf.Serializer.Deserialize<ProtoBuf.C2S_RoleInit> (stream);
                    HandleMessage(index, msg);
					break;
				}
			case MessageTypes.C2S_NewHero:
				{
					ProtoBuf.C2S_NewHero msg = ProtoBuf.Serializer.Deserialize<ProtoBuf.C2S_NewHero> (stream);
                    HandleMessage(index, msg);
					break;
				}
			}

		}


		private void HandleMessage(int index, ProtoBuf.C2S_Login req)
		{
			Console.WriteLine (req.ToString());
			ProtoBuf.S2C_Login resp = new ProtoBuf.S2C_Login();
			resp.ret = 0;
			resp.uid = "1000";
			resp.code = "1000";
			SendMessage(index, MessageTypes.S2C_Login, resp);
		}

        private void HandleMessage(int index, ProtoBuf.C2S_UserInit req)
		{
			Console.WriteLine (req.ToString());
			ProtoBuf.S2C_UserInit resp = new ProtoBuf.S2C_UserInit();
			resp.ret = 0;
			ProtoBuf.DAT_UserData userData = new ProtoBuf.DAT_UserData ();
			userData.nickname = "PAPAPA";

            resp.userData = userData;

            SendMessage(index, MessageTypes.S2C_UserInit, resp);
		}

        private void HandleMessage(int index, ProtoBuf.C2S_RoleInit req)
		{
			Console.WriteLine (req.ToString());
		}

        private void HandleMessage(int index, ProtoBuf.C2S_NewHero req)
		{
			Console.WriteLine (req.ToString());
		}
	}
}