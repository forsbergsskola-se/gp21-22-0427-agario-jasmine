﻿using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace TimeServer
{
    class Program
    {
        static void Main(string[] args) {
            TcpListener tcpListener = new TcpListener(IPAddress.Any, 44);
            tcpListener.Start();
            
            Console.WriteLine("Time Server waiting on connection");
            
            var tcpClient = tcpListener.AcceptTcpClient();
            Console.WriteLine($"Client {tcpClient.Client.RemoteEndPoint} connected.");

            var stream = tcpClient.GetStream();
            
            stream.Write(Encoding.ASCII.GetBytes("Hello, this is Jasmines Time Server. The current time is: " + DateTime.Now));
            stream.Close();
            tcpListener.Stop();
        }
    }
}