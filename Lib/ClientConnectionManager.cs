using System.Net.Sockets;

namespace Lib
{
    public class ClientConnectionManager
    {
        public TcpClient TcpClient { get; private set; }
        public bool ClientState { get; private set; }
        public string ConnectingTime { get; private set; }

        public ClientConnectionManager(TcpClient tcpClient, string connectingTime)
        {
            TcpClient = tcpClient;
            ConnectingTime = connectingTime;
            ClientState = true;
        }

        public void Disconnecting()
        {
            ClientState = false;
            TcpClient.Close();
        }
    }
}
