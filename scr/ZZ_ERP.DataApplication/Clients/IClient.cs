using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MongoRepository.Connections;
using ZZ_ERP.Infra.CrossCutting.Connections.Commons;

namespace ZZ_ERP.DataApplication.Clients
{
    public abstract class IClient : IDisposable
    {
        public ServerConnection Connection;
        public ZZClientManager Manager;
        protected Command Cmd;

        /// <summary>
        /// Login do cliente, passando a instancia do PartyRoster e a conecção.
        /// </summary>
        /// <param name="manager">Instance.</param>
        /// <param name="connect">Connect.</param>
        public abstract Task Login(ZZClientManager manager, ServerConnection connect);

        /// <summary>
        /// Logout do cliente, retirando o cliente da lista de usuarios logados.
        /// </summary>
        public abstract Task Logout();

        /// <summary>
        /// Processamento do comando enviado pelo cliente.
        /// </summary>
        /// <param name="command">Command.</param>
        public abstract Task Command(Command command);

        public void Dispose()
        {
        }
    }
}
