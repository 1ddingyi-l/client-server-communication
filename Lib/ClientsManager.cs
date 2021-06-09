using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;

namespace Lib
{
    public class ClientsManager : IObservable, IObserver
    {
        private object Locker { get; set; }
        private Dictionary<string, Tuple<Client, AccessDBOperator>> Clients { get; set; }
        public Tuple<Client, AccessDBOperator> this[string client] { get => Clients[client]; }
        public event ParameterizedDelegate ClientsManagerEvent;

        public ClientsManager()
        {
            Clients = new Dictionary<string, Tuple<Client, AccessDBOperator>>();
            Locker = new object();
        }

        ~ClientsManager() => DisconnectAllConnection();

        public void ClientAddedAndConnecting(IPEndPoint endPoint)
        {
            Client client = new Client(endPoint);
            AccessDBOperator accessDBOperator = new AccessDBOperator();
            client.Connect();
            client.Register(this);  // Register event.
            accessDBOperator.Start();
            lock (Locker)
                Clients.Add(client.LocalEndPoint.ToString(), new Tuple<Client, AccessDBOperator>(client, accessDBOperator));
            Notify();  // A new client have been added onto Clients.
        }

        public void ClientRemovedAndDisconnecting(string localEndPoint)
        {
            Clients[localEndPoint].Item1.Remove(this);
            Clients[localEndPoint].Item1.Disconnect();
            Clients[localEndPoint].Item2.Close();  // Update end time.
            lock (Locker)
                Clients.Remove(localEndPoint);
            Notify();
        }

        public void DisconnectAllConnection()
        {
            foreach (string client in GetClientLocalEndPoints())
                ClientRemovedAndDisconnecting(client);
        }

        public string SendTo(string localEndPoint, string message)
        {
            string remoteEndPoint = Clients[localEndPoint].Item1.RemoteEndPoint.ToString();
            string receivedMessage = Clients[localEndPoint].Item1.SendAndReceiveOnce(message);
            Clients[localEndPoint].Item2.InsertRows(localEndPoint, remoteEndPoint, message, receivedMessage);
            return receivedMessage;
        }

        public string[] GetClientLocalEndPoints()
        {
            int len = Clients.Count;
            string[] clientLocalEndPoints = new string[len];
            Clients.Keys.CopyTo(clientLocalEndPoints, 0);
            return clientLocalEndPoints;
        }

        public void Register(IObserver observer) => ClientsManagerEvent += observer.Update;

        public void Remove(IObserver observer) => ClientsManagerEvent -= observer.Update;

        public void Notify()
        {
            if (ClientsManagerEvent != null)
                ClientsManagerEvent.Invoke(null);
        }

        public void Update(object state)
        {
            string localEndPoint = state as string;
            ClientRemovedAndDisconnecting(localEndPoint);
            Notify();  // A client haved by ClientsManager is closed.
        }
    }
}
