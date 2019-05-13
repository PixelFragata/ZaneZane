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
    public class FuncionarioManager : EntityManager<Funcionario>
    {

        public override async Task<Command> GetAll(Command command)
        {
            Command cmd = new Command(command);
            try
            {
                var entities = await MyRepository.Get(null,null,"Endereco");
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
                
                var funcionarios = await MyRepository.Get(t => t.Codigo.Equals(dto.Codigo));

                if (funcionarios != null && !funcionarios.Any())
                {
                    var entity = new Funcionario();
                    entity.UpdateEntity(dto);
                    var myCmd = new Command();
                    if (entity.EnderecoId <= 0) 
                    {
                        var enderecoCmd = new Command { Json = await SerializerAsync.SerializeJson(dto.Endereco) };

                        if (string.IsNullOrEmpty(entity.Endereco.Cep))
                        {
                            myCmd = await LocalizationManager.GetAddress(enderecoCmd);
                        }
                        else
                        {
                            myCmd = await LocalizationManager.GetAddressByZipCode(enderecoCmd);
                        }
                        entity.Endereco.UpdateEntity(await SerializerAsync.DeserializeJson<EnderecoDto>(myCmd.Json));
                        entity.EnderecoId = entity.Endereco.Id;
                    }

                    if (entity.EnderecoId > 0)
                    {
                        entity.Endereco = null;
                    }

                    var insertEntity = await MyRepository.Insert(entity);
                    if (insertEntity != null)
                    {
                        cmd.Cmd = ServerCommands.LogResultOk;
                        cmd.Json = await SerializerAsync.SerializeJson(true);
                        await MyRepository.Save();
                        cmd.EntityId = entity.Id;
                    }
                    else
                    {
                        cmd.Cmd = ServerCommands.RepeatedHumanCode;
                        ConsoleEx.WriteLine(ServerCommands.RepeatedHumanCode);
                    }

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

                var funcionario = await MyRepository.GetById(cmd.EntityId);

                if (funcionario != null)
                {
                    funcionario.UpdateEntity(dto);
                    var enderecoRep = new Repository<Endereco>(Context);
                    var endereco = await enderecoRep.GetById(dto.Endereco.Id);

                    if (endereco != null)
                    {
                        endereco.UpdateEntity(dto.Endereco);
                        funcionario.Endereco = endereco;
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
