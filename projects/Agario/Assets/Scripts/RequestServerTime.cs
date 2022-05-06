using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class RequestServerTime : MonoBehaviour
{
    public void SendRequest() {
        TcpClient tcpClient = new TcpClient("127.0.0.1", 44);

        var stream = tcpClient.GetStream();
        byte[] bytes = new byte[tcpClient.ReceiveBufferSize];
        stream.Read(bytes, 0, bytes.Length);
        string info = Encoding.ASCII.GetString(bytes);
        //Debug.Log(info);
        //TODO: Output to text in Unity
    }
}
