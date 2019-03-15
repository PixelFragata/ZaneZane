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
                        if(!string.IsNullOrWhiteSpace(command.Tela))
                        {
                            var managerName = "ZZ_ERP.DataApplication.EntitiesManager." + command.Tela + "Manager";
                            var type = Type.GetType(managerName);
                            var manager = (IEntityManager) Activator.CreateInstance(type);

                            if (command.Cmd.Equals(ServerCommands.GetAll))
                            {
                                Cmd = await manager.GetAll(command);
                                await Connection.WriteServer(Cmd);
                            }
                            else if (command.Cmd.Equals(ServerCommands.Add))
                            {
                                Cmd = await manager.Add(command);
                                await Connection.WriteServer(Cmd);
                            }
                            else if (command.Cmd.Equals(ServerCommands.Edit))
                            {
                                Cmd = await manager.Edit(command);
                                await Connection.WriteServer(Cmd);
                            }
                            else if (command.Cmd.Equals(ServerCommands.Disable))
                            {
                                Cmd = await manager.Delete(command);
                                await Connection.WriteServer(Cmd);
                            }
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
