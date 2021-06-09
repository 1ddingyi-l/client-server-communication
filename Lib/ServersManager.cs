using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace Lib
{
    public class ServersManager : IObservable, IObserver
    {
        private object Locker { get; set; }
        private Dictionary<string, Server> Servers { get; set; }
        public event ParameterizedDelegate ServersManagerEvent;
        public Server this[string server] { get => Servers[server]; }

        public ServersManager()
        {
            Locker = new object();
            Servers = new Dictionary<string, Server>();
        }

        ~ServersManager() => StopAllServer();

        public void ServerAddedAndStarting(IProtocol protocol, IPEndPoint endPoint)
        {
            Server server = new Server(protocol, endPoint);
            server.Register(this);
            try
            {
                server.Start();
            }
            catch (SocketException e)
            {
                throw e;  // Port have been used!
            }
            lock (Locker)
                Servers.Add(endPoint.ToString(), server);
            Notify();  // Add a new server.
        }

        public void ServerRemovedAndStopping(string server)
        {
            Servers[server].Stop();
            Servers[server].Remove(this);
            lock (Locker)
                Servers.Remove(server);
            Notify();  // Remove a server.
        }

        public void StopAllServer()
        {
            int len = Servers.Count;
            string[] servers = new string[len];
            Servers.Keys.CopyTo(servers, 0);
            foreach (string server in servers)
                ServerRemovedAndStopping(server);
        }

        public string[] GetServers()
        {
            int len = Servers.Count;
            string[] servers = new string[len];
            Servers.Keys.CopyTo(servers, 0);
            return servers;
        }

        public void Register(IObserver observer) => ServersManagerEvent += observer.Update;

        public void Remove(IObserver observer) => ServersManagerEvent -= observer.Update;

        public void Notify()
        {
            if (ServersManagerEvent != null)
                ServersManagerEvent.Invoke(null);  // From this servers manager.
        }

        public void Update(object state)
        {
            if (ServersManagerEvent != null)
                ServersManagerEvent.Invoke(state);  // From server.
        }
    }
}
