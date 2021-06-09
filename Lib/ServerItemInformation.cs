namespace Lib
{
    public class ServerItemInformation
    {
        public string ServerIPEndPoint { get; private set; }
        public string ServerStartingTime { get; private set; }

        public ServerItemInformation(string endpoint, string time)
        {
            ServerIPEndPoint = endpoint;
            ServerStartingTime = time;
        }
    }
}
