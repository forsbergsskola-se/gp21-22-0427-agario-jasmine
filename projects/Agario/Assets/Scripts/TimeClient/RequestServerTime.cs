using System;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class RequestServerTime : MonoBehaviour {
    [SerializeField] Text dateAndTimeText;

    public void SendRequest() {
        TcpClient tcpClient = new TcpClient("127.0.0.1", 44);

        var stream = tcpClient.GetStream();
        
        byte[] bytes = new byte[tcpClient.ReceiveBufferSize];
        stream.Read(bytes, 0, bytes.Length);
        
        string message = Encoding.ASCII.GetString(bytes);

        UpdateText(message);
    }

    private void UpdateText(string message) {
        dateAndTimeText.text = message;
    }
}
