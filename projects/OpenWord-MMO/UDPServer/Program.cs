using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using UDPServer;

public static class Program {
    static void Main(string[] arguments) {
        var serverEndpoint = new IPEndPoint(IPAddress.Loopback, 2222);
        var server = new UdpClient(serverEndpoint);
        
        NetworkMessage networkMessage = new NetworkMessage();
        
        while (true) {
            IPEndPoint? clientEndpoint = default;
            var response = server.Receive(ref clientEndpoint);
            string responseString = Encoding.ASCII.GetString(response).Trim();

            networkMessage.Result = WordValidator.ProcessWord(responseString);
            if (networkMessage.Result.Error != Error.None) {
                SendNetworkMessage(networkMessage, server, clientEndpoint);
                continue;
            }

            Console.WriteLine($"Packet received from: {clientEndpoint} saying: {responseString}");
            
            networkMessage.Response += " " + responseString;
            SendNetworkMessage(networkMessage, server, clientEndpoint);
        }
    }

    private static void SendNetworkMessage(NetworkMessage networkMessage, UdpClient? server, IPEndPoint? clientEndpoint) {
        string messageJson = JsonSerializer.Serialize(networkMessage);
        Console.WriteLine(messageJson);
        byte[] messageBytes = Encoding.ASCII.GetBytes(messageJson);
        server?.Send(messageBytes, messageBytes.Length, clientEndpoint);
    }
}