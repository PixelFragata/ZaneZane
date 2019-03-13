using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZZ_ERP.DataApplication.EntitiesManager;
using ZZ_ERP.Domain.Entities;
using ZZ_ERP.Infra.CrossCutting.Connections.Commons; 
using ZZ_ERP.Infra.CrossCutting.Connections.Connections;
using ZZ_ERP.Infra.CrossCutting.Connections.Functions;
using ZZ_ERP.Infra.Data.Contexts;
using ZZ_ERP.Infra.Data.Repositories;

namespace ZZ_ERP.DataApplication.Clients
{
    public class UserClient : IClient
    {
        public override async Task Login(ZZClientManager manager, ServerConnection connection)
        {
            Manager = manager;
            Connection = connection;
            await connection.WriteServer("", 542, ServerCommands.LogResultOk, "");
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
                        if (command.Cmd.Equals(ServerCommands.GetAllTiposServico))
                        {
                            Cmd = await TiposServicosManager.GetAll(command);
                            await Connection.WriteServer(Cmd);
                        }
                        else if (command.Cmd.Equals(ServerCommands.AddTipoServico))
                        {
                            Cmd = await TiposServicosManager.Add(command);
                            await Connection.WriteServer(Cmd);
                        }
                        else if (command.Cmd.Equals(ServerCommands.EditTipoServico))
                        {
                            Cmd = await TiposServicosManager.Edit(command);
                            await Connection.WriteServer(Cmd);
                        }
                        else if (command.Cmd.Equals(ServerCommands.DeleteTipoServico))
                        {
                            Cmd = await TiposServicosManager.Delete(command);
                            await Connection.WriteServer(Cmd);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                ConsoleEx.WriteError("Erro ao receber command do client " ,e);
                throw;
            }
            
        }
    }
}
