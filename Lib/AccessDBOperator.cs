using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OleDb;

namespace Lib
{
    public class AccessDBOperator
    {
        public string ConnectionString { get; private set; }
        public long ConnectionID { get; private set; }

        public AccessDBOperator() => ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];

        public List<string> GetTableName()
        {
            List<string> tables = new List<string>();
            Queue<string> queue = new Queue<string>();
            using (var connection = new OleDbConnection(ConnectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = CommonAccessSQLTemplates.ShowTables;
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                        queue.Enqueue(reader.GetString(0));
                }
                connection.Close();
            }
            while (queue.Count != 0)
                tables.Add(queue.Dequeue().ToString());
            return tables;
        }

        public void InsertRows(string sender, string receiver, string sentMessage, string receivedMessage)
            => ExecuteNonQuery($"insert into Messages(recordedTime, sender, receiver, sentMessage, receivedMessage, connectionID) " +
                        $"values('{DateTime.Now}', '{sender}', '{receiver}', '{sentMessage}', '{receivedMessage}', {ConnectionID})");

        public void Start()
        {
            List<string> tables = GetTableName();
            if (!tables.Contains("Connections"))  // check validity of tables.
                CreateTableConnection();
            if (!tables.Contains("Messages"))  // If have not this table in database create it.
                CreateTableMessage();
            using (var connection = new OleDbConnection(ConnectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = $"insert into Connections(beginTime, endTime, isActive) values('{DateTime.Now}', '{DateTime.Now}', '1')";
                command.ExecuteNonQuery();
                command.CommandText = $"select @@identity as ID";
                ConnectionID = long.Parse(command.ExecuteScalar().ToString());
                connection.Close();
            }
        }

        public void Close()
        {
            DateTime now = DateTime.Now;
            ExecuteNonQuery($"update Connections set endTime = '{now}', isActive = '0' where connectionID = {ConnectionID}");
        }

        public void ExecuteNonQuery(string sql)
        {
            using (var connection = new OleDbConnection(ConnectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = sql;
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        private void CreateTableMessage() => ExecuteNonQuery(CommonAccessSQLTemplates.CreateMessagesTable);

        private void CreateTableConnection() => ExecuteNonQuery(CommonAccessSQLTemplates.CreateConnectionsTable);
    }
}
