using System;

namespace Lib
{
    public class ClientItemInformation
    {
        public string ClientLocalEndPoint { get; private set; }
        public string ClientRemoteEndPoint { get; private set; }
        public string ClientConnectingTime { get; private set; }

        public ClientItemInformation(string clientLocalEndPoint, string clientRemoteEndPoint, string clientConnectingTime)
        {
            ClientLocalEndPoint = clientLocalEndPoint;
            ClientRemoteEndPoint = clientRemoteEndPoint;
            ClientConnectingTime = clientConnectingTime;
        }
    }
}
