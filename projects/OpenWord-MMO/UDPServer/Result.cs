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
    
    public enum Error {
        None,
        TooLong,
        ContainsWhitespace,
        TooLongAndContainsWhitespace
    }
    
    public static class WordValidator {
        public static Result ProcessWord(string word) {
            Regex lengthPattern = new Regex(@"^.{0,20}$");
            Regex charPattern = new Regex(@"\A\S{0,}\z");
            
            Result result = new Result {
                Error = Error.None,
                ErrorMessage = ""
            };

            if (!lengthPattern.IsMatch(word) && !charPattern.IsMatch(word)) {
                result.Error = Error.TooLongAndContainsWhitespace;
            } else if (!lengthPattern.IsMatch(word)) { 
                result.Error = Error.TooLong;
            } else if (!charPattern.IsMatch(word)) {
                result.Error = Error.ContainsWhitespace;
            }
            
            switch (result.Error) {
                case Error.None:
                    break;
                case Error.TooLong:
                    result.ErrorMessage = "Error: The word is longer than 20 characters, please try again!";
                    break;
                case Error.ContainsWhitespace:
                    result.ErrorMessage = "Error: The word contains white spaces, please try again!";
                    break;
                case Error.TooLongAndContainsWhitespace:
                    result.ErrorMessage = "Error: The word is longer than 20 characters and contain whitespaces, please try again!";
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return result;
        }
    }
}