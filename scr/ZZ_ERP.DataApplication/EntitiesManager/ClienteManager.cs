using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ZZ_ERP.Domain.Entities;
using ZZ_ERP.Infra.CrossCutting.Connections.Commons;
using ZZ_ERP.Infra.CrossCutting.Connections.Functions;
using ZZ_ERP.Infra.CrossCutting.DTO.EntitiesDTO;
using ZZ_ERP.Infra.Data.Contexts;
using ZZ_ERP.Infra.Data.Repositories;

namespace ZZ_ERP.DataApplication.EntitiesManager
{
    public class ClienteManager : EntityManager<Cliente>
    {

        public override async Task<Command> GetAll(Command command)
        {
            Command cmd = new Command(command);
            try
            {
                var entities = await MyRepository.Get(null,null,"Endereco,Endereco.Cidade,Endereco.Cidade.Estado");
                var list = entities.ToList();

                if (list.Any())
                {
                    cmd.Cmd = ServerCommands.LogResultOk;
                    var dtos = list.Select(t => t.ConvertDto()).ToList();
                    cmd.Json = await SerializerAsync.SerializeJsonList(dtos);
                }
                else
                {
                    cmd.Cmd = ServerCommands.LogResultDeny;
                }
            }
            catch (Exception e)
            {
                ConsoleEx.WriteError(e);

            }

            return cmd;
        }
        public override async Task<Command> Add(Command command)
        {
            Command cmd = new Command(command);
            try
            {
                var dto = await SerializerAsync.DeserializeJson<UserDadosDto>(command.Json);

                var clientes = await MyRepository.Get(t => t.Codigo.Equals(dto.Codigo));

                if (clientes != null && !clientes.Any())
                {

                    cmd.Cmd = ServerCommands.LogResultOk;
                    var cliente = new Cliente();
                    cliente.UpdateEntity(dto);
                    await MyRepository.Insert(cliente);
                    cmd.Json = await SerializerAsync.SerializeJson(true);
                    await MyRepository.Save();
                    
                }
                else
                {
                    cmd.Cmd = ServerCommands.LogResultDeny;
                }
            }
            catch (Exception e)
            {
                ConsoleEx.WriteError(e);

            }

            return cmd;
        }

        public override async Task<Command> Edit(Command command)
        {
            Command cmd = new Command(command);
            try
            {
                var dto = await SerializerAsync.DeserializeJson<UserDadosDto>(command.Json);

                var cliente = await MyRepository.GetById(cmd.EntityId);

                if (cliente != null)
                {
                    cliente.UpdateEntity(dto);
                    var enderecoRep = new Repository<Endereco>(Context);
                    var endereco = await enderecoRep.GetById(dto.Endereco.Id);

                    if (endereco != null)
                    {
                        cliente.Endereco = endereco;
                    }

                    cmd.Cmd = ServerCommands.LogResultOk;
                    cmd.Json = await SerializerAsync.SerializeJson(true);
                    await MyRepository.Save();
                }
                else
                {
                    cmd.Cmd = ServerCommands.LogResultDeny;
                    cmd.Json = await SerializerAsync.SerializeJson(false);
                }
            }
            catch (Exception e)
            {
                ConsoleEx.WriteError(e);

            }

            return cmd;
        }

    }
}
