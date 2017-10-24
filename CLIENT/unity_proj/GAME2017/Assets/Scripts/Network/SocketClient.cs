using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace GNetwork{

	public class SocketClient{

		public SocketClient(){}

		private Socket _socket;

		public bool ConnectServer(string ip, int port)
		{
			IPAddress _ip = IPAddress.Parse(ip);
			_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			try
			{
				_socket.Connect(new IPEndPoint(_ip, port));
				//Console.WriteLine("connected");


				Thread tRecv = new Thread(new ThreadStart(ReceiveMsg));
				tRecv.IsBackground = true;
				tRecv.Start();


				return true;
			}
			catch
			{
				return false;
			}

		}

		void ReceiveMsg()
		{
			byte[] lenBytes = new byte[4];
			byte[] recvBytes = new byte[GConfig.Constant.buffer_max_length];
			int bytes;
			while (true)
			{
				int len = 0;
				bytes = _socket.Receive(lenBytes, 4, 0);
				if(bytes > 0)
				{
					System.Array.Reverse (lenBytes);
					len = System.BitConverter.ToInt32(lenBytes, 0);

				}
				if (bytes < 0)
				{
					break;
				}


                if (len > 0 && len <= GConfig.Constant.buffer_max_length)
                {
                    bytes = _socket.Receive(recvBytes, len, 0);
                    CommunicationManager.Instance.Receive(recvBytes, len);
                }
                else
                {
                    break;
                }
			}

			_socket.Close();
			CommunicationManager.Instance.Disconnect ();

		}


		public void Send(byte[] data)
		{
			_socket.Send(data);
		}

	}


}
