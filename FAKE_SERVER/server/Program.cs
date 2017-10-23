using System;
using System.Collections.Generic;
using System.Threading;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace fake_server
{
    class Program
    {
        static void Main(string[] args)
        {
			SocketServer ss = new SocketServer ();
			ss.StartServer ();
		}
    }

	class SocketServer
	{
		public SocketServer(){}

		private Socket _socket;

		public void StartServer()
		{
			IPAddress ip = IPAddress.Parse(fake_server.Config.server);
			int port = fake_server.Config.port;
			_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			try
			{
				_socket.Bind(new IPEndPoint(ip, port));
				_socket.Listen(10);
				Console.WriteLine("Server started {0}:{1} ...",ip.ToString(),port.ToString());  


				Thread thread = new Thread(StartAcceptClients);  
				thread.Start();
			}
			catch
			{
				Console.WriteLine("Start failed");
				Console.ReadLine ();
			}

		}

		void StartAcceptClients()
		{
			while (true)  
			{  
				Socket client = _socket.Accept();  
				Thread receiveThread = new Thread(HandleClientConnected);  
				receiveThread.Start(client);
			}  
		}

		void HandleClientConnected(Object client)
		{
			Socket s = (Socket)client;
			CommunicationManager cm = new CommunicationManager (s);

			byte[] lenBytes = new byte[4];
			byte[] recvBytes = new byte[Config.buffer_max_length];
			int bytes;
			while (true)
			{
				int len = 0;
				bytes = s.Receive(lenBytes, 4, 0);
				if(bytes > 0)
				{
					System.Array.Reverse (lenBytes);
					len = System.BitConverter.ToInt32(lenBytes, 0);

				}
				if (bytes < 0)
				{
					break;
				}


				if (len > 0)
				{
					bytes = s.Receive(recvBytes, len , 0);
					cm.Receive(recvBytes, len);

				}
				if (bytes < 0)
				{
					break;
				}



			}

			s.Close();

		}
			
	}

}
