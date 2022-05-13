using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;

public static class Program {
    static void Main(string[] arguments) {
        string wordSequence = "Word sequence is: ";
        const string errorMessage = "Error: Word is longer than 20 characters or contain whitespaces, please try again!";
        var errorBytes = Encoding.ASCII.GetBytes(errorMessage);

        var serverEndpoint = new IPEndPoint(IPAddress.Loopback, 2222);
        var server = new UdpClient(serverEndpoint);
        
        while (true) {
            IPEndPoint? clientEndpoint = default;
            var response = server.Receive(ref clientEndpoint);
            string responseString = Encoding.ASCII.GetString(response).Trim();

            if (!WordIsValid(responseString)) {
                server.Send(errorBytes, errorBytes.Length, clientEndpoint);           
                continue;
            }
            
            Console.WriteLine($"Packet received from: {clientEndpoint} saying: {responseString}");
            wordSequence += " " + responseString;
            
            byte[] returnBytes = Encoding.ASCII.GetBytes(wordSequence);
            server.Send(returnBytes, returnBytes.Length, clientEndpoint);
        }
    }

    private static bool WordIsValid(string word) {
        //Regex expression for only allowing words without whitespaces and 20 or less characters.
        Regex regex = new Regex(@"^\S{0,20}$");
        return regex.IsMatch(word);
    }
}