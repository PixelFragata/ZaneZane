using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MongoRepository.Connections;
using ZZ_ERP.Infra.CrossCutting.Connections.Commons;

namespace ZZ_ERP.DataApplication.Clients
{
    public class UserClient : IClient
    {
        public override async Task Login(ZZClientManager manager, ServerConnection connect)
        {
            Manager = manager;
            Connection = connect;
        }

        public override async Task Logout()
        {

        }

        public override async Task Command(Command command)
        {

        }
    }
}
