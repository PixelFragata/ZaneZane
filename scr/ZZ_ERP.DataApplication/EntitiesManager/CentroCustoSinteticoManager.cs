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
    public class CentroCustoSinteticoManager : EntityManager<CentroCustoSintetico>
    { 

        public override async Task<Command> Add(Command command)
        {
            Command cmd = new Command(command);
            try
            {
                var dto = await SerializerAsync.DeserializeJson<TipoDto>(command.Json);

                var tipos = await MyRepository.Get(t => t.Descricao.Equals(dto.Description));

                if (tipos != null && !tipos.Any())
                {
                    var entity = new CentroCustoSintetico();
                    entity.UpdateEntity(dto);
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
                var dto = await SerializerAsync.DeserializeJson<TipoDto>(command.Json);

                var tipoServico = await MyRepository.GetById(cmd.EntityId);

                if (tipoServico != null)
                {
                    tipoServico.Descricao = dto.Description;
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
