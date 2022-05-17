using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using UDPServer;

public static class Program {
    static void Main(string[] arguments) {
        var serverEndpoint = new IPEndPoint(IPAddress.Loopback, 2222);
        var server = new UdpClient(serverEndpoint);
        
        NetworkMessage networkMessage = new NetworkMessage();
        
        while (true) {
            IPEndPoint? clientEndpoint = default;
            var responseBytes = server.Receive(ref clientEndpoint);
            string response = Encoding.ASCII.GetString(responseBytes).Trim();

            networkMessage.Result = WordValidator.ProcessWord(response);
            
            if (networkMessage.Result.Error != Error.None) {
                SendNetworkMessage(networkMessage, server, clientEndpoint);
                continue;
            }

            Console.WriteLine($"Packet received from: {clientEndpoint} saying: {response}");
            
            networkMessage.Response += " " + response;
            SendNetworkMessage(networkMessage, server, clientEndpoint);
        }
    }

    private static void SendNetworkMessage(NetworkMessage networkMessage, UdpClient? server, IPEndPoint? clientEndpoint) {
        string messageJson = JsonSerializer.Serialize(networkMessage);
        byte[] messageBytes = Encoding.ASCII.GetBytes(messageJson);
        server?.Send(messageBytes, messageBytes.Length, clientEndpoint);
    }
}