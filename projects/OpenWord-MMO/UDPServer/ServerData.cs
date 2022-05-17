using System;
using System.Text.RegularExpressions;

namespace UDPServer {
    [Serializable]
    public class NetworkMessage {
        public string Response { get; set; } = "";
        public Result Result { get; set; }
    }

    [Serializable]
    public class Result {
        public Error Error { get; set; }
        public string ErrorMessage { get; set; } = "";
    }
}