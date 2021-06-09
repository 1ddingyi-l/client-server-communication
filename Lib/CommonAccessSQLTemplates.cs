using System;

namespace Lib
{
    public static class CommonAccessSQLTemplates
    {
        public static string CreateMessagesTable { get; private set; }
            = "create table Messages(" +
            "messageID autoincrement primary key," +
            "recordedTime date not null," +
            "sender text(100) not null," +
            "receiver text(100) not null," +
            "sentMessage memo not null," +
            "receivedMessage memo not null," +
            "connectionID long not null)";

        public static string CreateConnectionsTable { get; private set; }
            = "create table Connections(" +
            "connectionID autoincrement primary key," +
            "beginTime date not null," +
            "endTime date not null," +
            "isActive text(1) not null)";

        public static string ShowTables { get; private set; } = "select name from MSysObjects where type=1 and flags=0";
    }
}
