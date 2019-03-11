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
            try
            {
                if (Manager != null && Connection != null)
                {
                    if (command.Cmd != null)
                    {
                        if (command.Cmd.Equals(ServerCommands.AddClientAuthorized))
                        {
                            if (Manager.Server.AddAuthorizedUser(
                                await SerializerAsync.DeserializeJson<string>(command.Json)))
                            {
                                command.Cmd = ServerCommands.LogResultOk;
                            }
                            else
                            {
                                command.Cmd = ServerCommands.LogResultDeny;
                            }

                            await Connection.WriteServer(command);
                        }
                        else if (command.Cmd.Equals(ServerCommands.RemoveClientAuthorized))
                        {
                            if (Manager.Server.RemoveAuthorizedUser(
                                await SerializerAsync.DeserializeJson<string>(command.Json)))
                            {
                                command.Cmd = ServerCommands.LogResultOk;
                            }
                            else
                            {
                                command.Cmd = ServerCommands.LogResultDeny;
                            }

                            await Connection.WriteServer(command);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                ConsoleEx.WriteError("Erro ao receber command do controller ", e);
                throw;
            }
        }
    }
}
