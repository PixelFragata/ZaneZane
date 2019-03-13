using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                        if (command.Cmd.Equals(ServerCommands.GetAllTiposServiço))
                        {
                            Cmd = new Command();
                            Cmd.Id = command.Id;
                            Cmd.IsWait = command.IsWait;
                            using (var context = new ZZContext())
                            {
                                var tiposRep = new Repository<TipoServico>(context);
                                var tiposList = await tiposRep.Get();
                                if (tiposList.Any())
                                {
                                    Cmd.Cmd = ServerCommands.LogResultOk;
                                    var nameTipos = tiposList.Select(t => t.DescricaoServico).ToList();
                                    Cmd.Json = await SerializerAsync.SerializeJsonList(nameTipos);
                                }
                                else
                                {
                                    Cmd.Cmd = ServerCommands.LogResultDeny;
                                }
                            }
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
