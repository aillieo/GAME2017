using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using System;
using System.Threading;
using UnityEngine;

namespace GNetwork{

	public class SocketClient{

		public SocketClient(){}

		private Socket _socket;

		private byte[] _buffer = new byte[GConfig.Constant.buffer_max_length];
		private int _offset = 0;

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
			int bytes = 0;
			byte[] recvBuffer = new byte[GConfig.Constant.buffer_max_length];
			while (true)
			{
				bytes = _socket.Receive(recvBuffer);
                if (bytes > 0)
                {
                    lock (this)
                    {
                        Array.Copy(recvBuffer, 0, _buffer, _offset, bytes);
                        _offset += bytes;
                    }
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


        public void GetBuffer(out byte[] buffer, out int offset)
		{
			buffer = _buffer;
			offset = _offset;
		}


        public void CleanBufferBytes(int bytes)
        {
            Array.Copy(_buffer, bytes, _buffer, 0, _offset - bytes);
            _offset -= bytes;
        }

	}


}
