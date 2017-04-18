using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace CSSocket{

public class SocketClient{

		public SocketClient(){}

		private Socket _socket;

		public bool ConnectServer()
		{
			IPAddress ip = IPAddress.Parse(CSSocket.Config.server);
			int port = CSSocket.Config.port;
			_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			try
			{
				_socket.Connect(new IPEndPoint(ip, port));
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
			byte[] recvBytes = new byte[CSSocket.Config.buffer_max_length];
			int bytes;
			while (true)
			{
				int len = 0;
				bytes = _socket.Receive(lenBytes, 4, 0);
				if(bytes > 0)
				{
					len = System.BitConverter.ToInt32(lenBytes, 0);
				}
				if (bytes < 0)
				{
					break;
				}


				if (len > 0)
				{
					bytes = _socket.Receive(recvBytes, len , 0);
					CommunicationManager.instance.Receive(recvBytes, len);

				}
				if (bytes < 0)
				{
					break;
				}



			}

			_socket.Close();

		}


		public void Send(byte[] data)
		{
			_socket.Send(data);
		}

	}


}
