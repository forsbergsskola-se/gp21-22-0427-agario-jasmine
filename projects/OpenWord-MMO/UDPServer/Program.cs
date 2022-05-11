using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

public static class Program {
    static void Main(string[] arguments) {
        string wordSequence = "";

        // TODO: Setup server
        // TODO: Connect to server & confirm
        // TODO: Send word to server
        // TODO: Validate word or send error message back
        // TODO: Store words in meaning
        // TODO: Send back meaning
        // TODO: Allow another word

        var serverEndpoint = new IPEndPoint(IPAddress.Loopback, 2222);
        var server = new UdpClient(serverEndpoint);
        
        while (true) {
            IPEndPoint clientEndpoint = default;
            
            var response = server.Receive(ref clientEndpoint);
            Console.WriteLine($"Packet received from: {clientEndpoint} saying: {Encoding.ASCII.GetString(response)}");
        }
    }
}