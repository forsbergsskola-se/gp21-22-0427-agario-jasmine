using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class OpenWordClient : MonoBehaviour {
    [SerializeField] private InputField _inputField;

    private IPEndPoint _serverEndPoint = new IPEndPoint(IPAddress.Loopback, 2222);
    private IPEndPoint _clientEndPoint = new IPEndPoint(IPAddress.Loopback, 2223);

    private UdpClient _client;

    void Start() {
        ConnectToServer();
    }

    private void ConnectToServer() {
        _client = new UdpClient(_clientEndPoint);
        Debug.Log("Connected");
    }

    public void SendWord() {
        byte[] bytes = Encoding.ASCII.GetBytes(_inputField.text);
        _client.Send(bytes, bytes.Length, _serverEndPoint);
        ReceiveResponse();
    }

    public delegate void ErrorMessageReceived(string errorMessage);
    public static event ErrorMessageReceived OnErrorMessageReceived;

    public void ReceiveResponse() {
        var messageBytes = _client.Receive(ref _serverEndPoint);
        NetworkMessage message = UnpackJson(messageBytes);

        if (message.Result.Error != Error.None) {
            OnErrorMessageReceived?.Invoke(message.Result.ErrorMessage);
        } //Else invoke on message recieved

        //Display word sentence
    }

    private NetworkMessage UnpackJson(byte[] messageBytes) {
        string messageJson = Encoding.ASCII.GetString(messageBytes);
        Debug.Log(messageJson);
        return JsonUtility.FromJson<NetworkMessage>(messageJson);
    }

}

//Move to other script
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
