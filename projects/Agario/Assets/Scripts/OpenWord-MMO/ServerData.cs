using System;
using UnityEngine;

namespace OpenWord_MMO {
    public class ServerData : MonoBehaviour
    {
        [Serializable]
        public class NetworkMessage {
            public string Response = "";
            public Result Result;
        }
    
        [Serializable]
        public class Result {
            public Error Error;
            public string ErrorMessage = "";
        }
    
        public enum Error {
            None,
            TooLong,
            ContainsWhitespace,
            TooLongAndContainsWhitespace
        }
    }
}
