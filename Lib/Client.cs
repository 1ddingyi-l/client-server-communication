using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Lib
{
    public class Client : IObservable
    {
        private TcpClient TcpClient { get; set; }
        public IPEndPoint RemoteEndPoint { get; private set; }
        public EndPoint LocalEndPoint { get; private set; }
        public bool IsConnecting { get; private set; }
        public DateTime ConnectedTime { get; private set; }
        public event ParameterizedDelegate ClientEvent;

        public Client(IPEndPoint endPoint)
        {
            TcpClient = new TcpClient();
            RemoteEndPoint = endPoint;
            IsConnecting = false;
        }

        public void Connect()
        {
            try
            {
                TcpClient.Connect(RemoteEndPoint);
                LocalEndPoint = TcpClient.Client.LocalEndPoint;  // Set endpoint.
                IsConnecting = true;
                ConnectedTime = DateTime.Now;
                ThreadPool.QueueUserWorkItem(_ =>
                {
                    while (IsConnecting)
                    {
                        if (TcpClient.Client.Poll(100, SelectMode.SelectRead))  // Set 100 miliseconds timeout.
                        {
                            byte[] receivedBuffer = new byte[10];
                            int receivedBytes = TcpClient.Client.Receive(receivedBuffer, 0, receivedBuffer.Length, SocketFlags.None);
                            /*
                             If the server close the connection to this client, that receivedBytes should be zero.
                             So we can know whether the connection is closed based on the value of receivedBytes.
                             */
                            if (receivedBytes == 0)
                                IsConnecting = false;
                        }
                    }
                    Notify();  // Notify all observers at first.
                    TcpClient.Close();
                });
            }
            catch (SocketException e)
            {
                // A error occur when connecting to the server.
                throw e;
            }
        }

        public void Disconnect() => IsConnecting = false;  // A safe method to close current client connection.

        public string SendAndReceiveOnce(string message)
        {
            byte[] receivedBuffer = new byte[1024];

            try
            {
                if (IsConnecting)
                {
                    TcpClient.Client.Send(Encoding.UTF8.GetBytes(message));
                    int receivedBytes = TcpClient.Client.Receive(receivedBuffer, 0, receivedBuffer.Length, SocketFlags.None);
                    message = Encoding.UTF8.GetString(receivedBuffer, 0, receivedBytes);
                }
                else
                    MessageNotifyer.ShowError("The server have closed the connection.");
            }
            catch (SocketException e)
            {
                throw e;  // A error occur when attempting to access the socket;
                /*
                 Possible errors:
                    Host stop server. Error code: 10053.
                    Client socket have not connected. Error code: 10057.
                 */
            }
            return message;
        }

        public void Register(IObserver observer) => ClientEvent += observer.Update;

        public void Remove(IObserver observer) => ClientEvent -= observer.Update;

        public void Notify()
        {
            if (ClientEvent != null)  // Lack this step will conduce on NullReferenceException.
                ClientEvent.Invoke(LocalEndPoint.ToString());
        }
    }
}
