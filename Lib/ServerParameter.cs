using System;

namespace Lib
{
    public class ServerParameter
    {
        public ClientConnectionManager ClientManager { get; private set; }
        public IProtocol Protocol { get; private set; }
        public ParameterizedAction<string> Terminate { get; private set; }

        public ServerParameter(ClientConnectionManager clientManager, IProtocol protocol, ParameterizedAction<string> terminate)
        {
            ClientManager = clientManager;
            Protocol = protocol;
            Terminate = terminate;
        }
    }
}
