using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace OpenWord_MMO {
    public class OpenWordClient : MonoBehaviour {
        [SerializeField] private InputField inputField;
        
        public delegate void ErrorMessageReceived(string errorMessage);
        public static event ErrorMessageReceived OnErrorMessageReceived;

        public delegate void MessageReceived(string message);
        public static event MessageReceived OnMessageReceived;

        private IPEndPoint _serverEndPoint = new IPEndPoint(IPAddress.Loopback, 2222);
        private IPEndPoint _clientEndPoint = new IPEndPoint(IPAddress.Loopback, 2223);
        private UdpClient _client;
        
        void Start() {
            ConnectToServer();
        }

        private void ConnectToServer() {
            _client = new UdpClient(_clientEndPoint);
        }

        public void SendWord() {
            byte[] bytes = Encoding.ASCII.GetBytes(inputField.text);
            _client.Send(bytes, bytes.Length, _serverEndPoint);
            ReceiveResponse();
        }

        private void ReceiveResponse() {
            byte[] messageBytes = _client.Receive(ref _serverEndPoint);
            ServerData.NetworkMessage message = UnpackJson(messageBytes);

            if (message.Result.Error != ServerData.Error.None) {
                OnErrorMessageReceived?.Invoke(message.Result.ErrorMessage);
            } else {
                OnMessageReceived?.Invoke(message.Response);
            }
        }

        private ServerData.NetworkMessage UnpackJson(byte[] messageBytes) {
            string messageJson = Encoding.ASCII.GetString(messageBytes);
            Debug.Log(messageJson);
            return JsonUtility.FromJson<ServerData.NetworkMessage>(messageJson);
        }
    }
}