using System;
using System.Text.RegularExpressions;

namespace UDPServer {
    public enum Error {
        None,
        TooLong,
        ContainsWhitespace,
        TooLongAndContainsWhitespace
    }

    public static class WordValidator {
        public static Result ProcessWord(string word) {
            Result result = ValidateWord(word);
            SetErrorMessage(result);

            return result;
        }

        private static Result ValidateWord(string word) {
            Result result = new Result {
                Error = Error.None,
                ErrorMessage = ""
            };
            
            //Regex pattern allowing 0-20 characters
            Regex lengthPattern = new Regex(@"^.{0,20}$");
            
            //Regex pattern not allowing whitespaces
            Regex charPattern = new Regex(@"\A\S{0,}\z");

            if (!lengthPattern.IsMatch(word) && !charPattern.IsMatch(word)) {
                result.Error = Error.TooLongAndContainsWhitespace;
            } else if (!lengthPattern.IsMatch(word)) { 
                result.Error = Error.TooLong;
            } else if (!charPattern.IsMatch(word)) {
                result.Error = Error.ContainsWhitespace;
            }

            return result;
        }
        
        private static void SetErrorMessage(Result result) {
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
        }
    }
}