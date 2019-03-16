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
    public class UnidadeMedidaManager : EntityManager<UnidadeMedida>
    {

        public override async Task<Command> Add(Command command)
        {
            Command cmd = new Command(command);
            try
            {
                var dto = await SerializerAsync.DeserializeJson<UnidadeMedidaDto>(command.Json);

                var entity = await MyRepository.Get(t => t.Sigla.Equals(dto.Sigla));

                if (entity != null && !entity.Any())
                {
                    cmd.Cmd = ServerCommands.LogResultOk;
                    await MyRepository.Insert(new UnidadeMedida { Sigla = dto.Sigla, Descricao = dto.Description });
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
                var dto = await SerializerAsync.DeserializeJson<UnidadeMedidaDto>(command.Json);

                var entity = await MyRepository.GetById(cmd.EntityId);

                if (entity != null)
                {
                    if (dto.Sigla != null) entity.Sigla = dto.Sigla;
                    if (dto.Description != null) entity.Descricao = dto.Description;
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
