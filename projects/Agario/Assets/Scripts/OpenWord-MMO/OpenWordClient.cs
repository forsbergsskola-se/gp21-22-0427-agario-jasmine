using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
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

    public void ReceiveResponse() {
        var messageBytes = _client.Receive(ref _serverEndPoint);
        NetworkMessage message = UnpackJson(messageBytes);
        Debug.Log("msg: " + message.Response);

        //Check for error
        //Set error text
        
        //Display word sentence
    }

    private NetworkMessage UnpackJson(byte[] messageBytes) {
        string messageJson = Encoding.ASCII.GetString(messageBytes);
        Debug.Log(messageJson);
        return JsonUtility.FromJson<NetworkMessage>(messageJson);
    }

}

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
