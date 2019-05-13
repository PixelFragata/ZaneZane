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
    public class FuncionarioEstoqueManager : EntityManager<FuncionarioEstoque>
    { 

        public override async Task<Command> Add(Command command)
        {
            Command cmd = new Command(command);
            try
            {
                 var dto = await SerializerAsync.DeserializeJson<DtoLigacao>(command.Json);

                if (dto.FirstDtoId > 0 && dto.SecondDtoId > 0)
                {
                    var funcionarioEstoques = await MyRepository.Get(t => t.FuncionarioId == dto.FirstDtoId
                                                                          && t.EstoqueId == dto.SecondDtoId);

                    var funcRep = new Repository<Funcionario>(Context);
                    var funcionario = await funcRep.GetById(dto.FirstDtoId);

                    var estoqueRep = new Repository<Estoque>(Context);
                    var estoque = await estoqueRep.GetById(dto.SecondDtoId);

                    if (funcionarioEstoques != null && !funcionarioEstoques.Any() && funcionario != null && estoque != null)
                    {
                        var entity = new FuncionarioEstoque();
                        entity.UpdateEntity(dto);
                        entity.Codigo = DateTime.Now.ToString();
                        entity.Funcionario = funcionario;
                        entity.Estoque = estoque;

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
                cmd.Cmd = ServerCommands.LogResultDeny;
                cmd.Json = await SerializerAsync.SerializeJson(false);
            }
            catch (Exception e)
            {
                ConsoleEx.WriteError(e);

            }

            return cmd;
        }

    }
}
