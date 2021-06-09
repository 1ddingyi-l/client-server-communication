using Lib;
using System;
using System.Net;
using System.Net.Sockets;
using System.Windows;
using System.Configuration;
using System.Windows.Threading;

namespace ClientUI
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window, IObserver
    {
        public ClientsManager ClientsManager { get; private set; }

        public MainWindow()
        {
            InitializeComponent();
            ClientsManager = new ClientsManager();
            ClientsManager.Register(this);
        }

        public void UpdateUI()
        {
            Dispatcher.Invoke(() =>
            {
                listv.Items.Clear();
                foreach (string client in ClientsManager.GetClientLocalEndPoints())
                    listv.Items.Add(new ClientItemInformation(
                        ClientsManager[client].Item1.LocalEndPoint.ToString(), 
                        ClientsManager[client].Item1.RemoteEndPoint.ToString(), 
                        ClientsManager[client].Item1.ConnectedTime.ToString()));
            });
        }

        public void CreateQueryWindow(object sender, EventArgs e)
        {
            Query form = new Query();
            form.ShowDialog();
        }

        public void WindowClose(object sender, EventArgs e)
        {
            ClientsManager.Remove(this);
            ClientsManager.DisconnectAllConnection();
            UpdateUI();
        }

        public void CloseConnection(object sender, EventArgs e)
        {
            int len = listv.SelectedItems.Count;
            ClientItemInformation[] items = new ClientItemInformation[len];
            listv.SelectedItems.CopyTo(items, 0);
            foreach (ClientItemInformation item in items)
                ClientsManager.ClientRemovedAndDisconnecting(item.ClientLocalEndPoint);
        }

        public void AddConnection(object sender, EventArgs e)
        {
            if (tboxEndPoint.Text == string.Empty)
                return;
            string[] im = tboxEndPoint.Text.Split(':');
            try
            {
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(im[0]), int.Parse(im[1]));
                ClientsManager.ClientAddedAndConnecting(endPoint);
            }
            catch (ArgumentOutOfRangeException)
            {
                MessageNotifyer.ShowError("This ip address is invalid!");
                return;
            }
            catch (FormatException)
            {
                MessageNotifyer.ShowError("This ip address is invalid!");
                return;
            }
            catch (SocketException er)
            {
                if (er.ErrorCode == 10061)
                    MessageNotifyer.ShowError("The server does not exist!");
            }
        }

        public void SendingToServer(object sender, EventArgs e)
        {
            if (listv.SelectedItem != null)
            {
                ClientItemInformation clientItemInformation = listv.SelectedItem as ClientItemInformation;
                string localEndPoint = clientItemInformation.ClientLocalEndPoint;
                string message = tboxMessage.Text;
                Dispatcher.Invoke(() =>
                {
                    tblock.Text = ClientsManager.SendTo(localEndPoint, message);
                });
            }
        }

        public void Update(object state) => UpdateUI();
    }
}
