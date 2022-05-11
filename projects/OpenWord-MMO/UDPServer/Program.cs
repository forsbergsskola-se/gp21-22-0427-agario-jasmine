using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;

public static class Program {
    static void Main(string[] arguments) {
        // TODO: Setup server
        // TODO: Connect to server & confirm
        // TODO: Send word to server
        // TODO: Validate word or send error message back
        // TODO: Store words in meaning
        // TODO: Send back meaning
        // TODO: Allow another word

        
        string wordSequence = "Word sequence is: ";
        byte[] returnBytes;
        
        var serverEndpoint = new IPEndPoint(IPAddress.Loopback, 2222);

        while (true) {
            var server = new UdpClient(serverEndpoint);

            IPEndPoint? clientEndpoint = default;
            
            var response = server.Receive(ref clientEndpoint);
            string responseString = Encoding.ASCII.GetString(response);

            Regex regex = new Regex(@"^\S{0,20}$");

            while (!regex.IsMatch(responseString)) {
                Console.WriteLine("Error: Word is longer than 20 characters or contain whitespaces, please try again!");
                response = server.Receive(ref clientEndpoint);
                responseString = Encoding.ASCII.GetString(response);
            }
            
            Console.WriteLine($"Packet received from: {clientEndpoint} saying: {responseString}");
            wordSequence += " " + responseString;
            
            returnBytes = Encoding.ASCII.GetBytes(wordSequence);
            server.Send(returnBytes, returnBytes.Length, clientEndpoint);

            server.Close();
        }
    }
}