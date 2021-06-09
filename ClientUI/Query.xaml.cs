using System;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Windows;

namespace ClientUI
{
    /// <summary>
    /// Query.xaml 的交互逻辑
    /// </summary>
    public partial class Query : Window
    {
        public Query()
        {
            InitializeComponent();
        }

        public void QueryEvent(object sender, EventArgs e)
        {
            if (dpBeginTime.Text == string.Empty || dpEndTime.Text == string.Empty || combo.SelectedItem == null)
                return;
            DateTime beginTime = DateTime.Parse(dpBeginTime.Text);
            DateTime endTime = DateTime.Parse(dpEndTime.Text);
            using (var connection = new OleDbConnection(ConfigurationManager.AppSettings["ConnectionString"]))
            {
                connection.Open();
                var command = connection.CreateCommand();
                if (combo.Text == "Messages")
                    command.CommandText = $"select * from Messages where recordedTime >= #{beginTime}# and recordedTime <= #{endTime}#";
                else
                    command.CommandText = $"select * from Connections where beginTime >= #{beginTime}# and endTime <= #{endTime}#";
                var reader = command.ExecuteReader();
                DataTable dataTable = new DataTable();
                dataTable.Load(reader);
                result.ItemsSource = dataTable.DefaultView;
                reader.Close();
                connection.Close();
            }
        }
    }
}
