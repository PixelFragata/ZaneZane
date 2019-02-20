using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZZ_ERP.Infra.CrossCutting.Connections.Commons;
using ZZ_ERP.Infra.CrossCutting.Connections.Connections;
using ZZ_ERP.Infra.CrossCutting.Connections.Functions;

namespace ZZ_ERP.DataApplication.Clients
{
    public class ControllerClient : IClient
    {
        public override async Task Login(ZZClientManager manager, ServerConnection connection)
        {
            Manager = manager;
            Connection = connection;
            ConsoleEx.WriteLine("Controller logando");
            await connection.WriteServer("",542,ServerCommands.LogResultOk,"");
        }

        public override async Task Logout()
        {

        }

        public override async Task Command(Command command)
        {

        }
    }
}
