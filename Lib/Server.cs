using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Lib
{
    public sealed class Server : IObservable
    {
        private object Locker { get; set; }
        private TcpListener Listener { get; set; }
        public IPEndPoint LocalEndPoint { get; private set; }
        public IProtocol Protocol { get; private set; }
        public bool IsListeningThreadAlive { get; private set; }
        public Dictionary<string, ClientConnectionManager> Clients { get; private set; }
        public DateTime StartedTime { get; private set; }
        public event ParameterizedDelegate ServerConnectionEvent;

        public Server(IProtocol protocol, IPEndPoint endPoint)
        {
            Locker = new object();
            Listener = new TcpListener(endPoint);
            LocalEndPoint = endPoint;
            Protocol = protocol;
            IsListeningThreadAlive = false;
            Clients = new Dictionary<string, ClientConnectionManager>();
        }

        public void Start()
        {
            try
            {
                Listener.Start(64);  // Start listening.
            }
            catch (SocketException e)  // Port have been used.
            {
                throw e;
            }
            IsListeningThreadAlive = true;
            StartedTime = DateTime.Now;
            ThreadPool.QueueUserWorkItem(_ =>  // Dispatch task to thread in the thread pool.
            {
                while (IsListeningThreadAlive)  // Listening task.
                    if (Listener.Pending())  // If detect any request from the client, execute following code.
                    {
                        TcpClient tcpClient = Listener.AcceptTcpClient();
                        ClientConnectionManager clientManager = new ClientConnectionManager(tcpClient, DateTime.Now.ToString());
                        lock (Locker)
                            Clients.Add(tcpClient.Client.RemoteEndPoint.ToString(), clientManager);  // Add this client connection onto dictionary.
                        ThreadPool.QueueUserWorkItem(ClientCommunicating, new ServerParameter(clientManager, Protocol, Terminate));  // Dispatch communication task.
                        Notify();  // Accept a new client connection and notify all of observers.
                    }
            });
        }

        public void Stop()
        {
            IsListeningThreadAlive = false;  // Reject all connection request.
            Thread.Sleep(20);  // Waiting for breaking out of the while statement.
            Listener.Stop();  // Stop listener.
            int len = Clients.Keys.Count;  // Do not iterate immediately by using foreach statement, which may conduce on a exception.
            string[] clients = new string[len];
            Clients.Keys.CopyTo(clients, 0);
            foreach (string client in clients)
            {
                Clients[client].Disconnecting();  // Closing all session to the clients.
                Clients.Remove(client);  // Remove client from client collection.
            }
        }

        public void ChangeProtocol(IProtocol protocol) => Protocol = protocol;

        /// <summary>
        /// Internal api for closing a client connection.
        /// Close a connection belong current server.
        /// Whatever actively or passively invoke this method, the server will kill the client connection and notify all observers.
        /// </summary>
        public void Terminate(string clientEndPoint)
        {
            if (Clients.ContainsKey(clientEndPoint))
            {
                Clients[clientEndPoint].Disconnecting();
                lock (Locker)
                    Clients.Remove(clientEndPoint);
                Notify();
            }
        }

        public string[] GetClients()
        {
            int len = Clients.Count;
            string[] clients = new string[len];
            Clients.Keys.CopyTo(clients, 0);
            return clients;
        }

        private static void ClientCommunicating(object state)
        {
            ServerParameter args = state as ServerParameter;
            ClientConnectionManager clientManager = args.ClientManager;
            IProtocol protocol = args.Protocol;
            ParameterizedAction<string> terminate = args.Terminate;
            string clientEndPoint = clientManager.TcpClient.Client.RemoteEndPoint.ToString();
            byte[] receivedBuffer = new byte[1024];

            clientManager.TcpClient.ReceiveTimeout = 2000;  // For more flexibly control the thread communication with the client.
            while (clientManager.ClientState)
            {
                try
                {
                    int receivedBytes = clientManager.TcpClient.Client.Receive(receivedBuffer, 0, receivedBuffer.Length, SocketFlags.None);
                    if (receivedBytes == 0)
                        throw new SocketException(10053);
                    string message = Encoding.UTF8.GetString(receivedBuffer, 0, receivedBytes);
                    message = protocol.GetResponse(message);
                    clientManager.TcpClient.Client.Send(Encoding.UTF8.GetBytes(message));
                }
                catch (SocketException e)
                {
                    if (e.ErrorCode == 10053)  // Remote client actively close the connection that has been established.
                    {
                        terminate(clientEndPoint);
                        break;
                    }
                }
            }
        }

        public void Register(IObserver observer) => ServerConnectionEvent += observer.Update;

        public void Remove(IObserver observer) => ServerConnectionEvent -= observer.Update;

        public void Notify()
        {
            if (ServerConnectionEvent != null)
                ServerConnectionEvent.Invoke(LocalEndPoint.ToString());
        }
    }
}
