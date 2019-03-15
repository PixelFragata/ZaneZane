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
    public class UnidadeMedidaManager : IEntityManager
    {

        public async Task<Command> GetAll(Command command)
        {
            Command cmd = new Command(command);

            using (var context = new ZZContext())
            {
                var rep = new Repository<UnidadeMedida>(context);
                var entities = await rep.Get();
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

            return cmd;
        }

        public async Task<Command> Add(Command command)
        {
            Command cmd = new Command(command);
            var dto = await SerializerAsync.DeserializeJson<UnidadeMedidaDto>(command.Json);
            using (var context = new ZZContext())
            {
                var rep = new Repository<UnidadeMedida>(context);
                var entity = await rep.Get(t => t.Sigla.Equals(dto.Sigla));

                if (!entity.Any())
                {
                    cmd.Cmd = ServerCommands.LogResultOk;
                    await rep.Insert(new UnidadeMedida { Sigla = dto.Sigla, Descricao = dto.Description});
                    cmd.Json = await SerializerAsync.SerializeJson(true);
                    await rep.Save();
                }
                else
                {
                    cmd.Cmd = ServerCommands.LogResultDeny;
                }
            }

            return cmd;
        }

        public async Task<Command> Edit(Command command)
        {
            Command cmd = new Command(command);
            var dto = await SerializerAsync.DeserializeJson<UnidadeMedidaDto>(command.Json);
            using (var context = new ZZContext())
            {
                var rep = new Repository<UnidadeMedida>(context);
                var entity = await rep.GetById(cmd.EntityId);

                if (entity != null)
                {
                    if (dto.Sigla != null) entity.Sigla = dto.Sigla;
                    if (dto.Description != null) entity.Descricao = dto.Description;
                    cmd.Cmd = ServerCommands.LogResultOk;
                    cmd.Json = await SerializerAsync.SerializeJson(true);
                    await rep.Save();
                }
                else
                {
                    cmd.Cmd = ServerCommands.LogResultDeny;
                    cmd.Json = await SerializerAsync.SerializeJson(false);
                }
            }

            return cmd;
        }

        public async Task<Command> Delete(Command command)
        {
            Command cmd = new Command(command);
            cmd.Json = await SerializerAsync.DeserializeJson<string>(command.Json);
            using (var context = new ZZContext())
            {
                var rep = new Repository<UnidadeMedida>(context);
                var entity = await rep.GetById(cmd.EntityId);

                if (entity != null)
                {
                    entity.IsActive = false;
                    cmd.Cmd = ServerCommands.LogResultOk;
                    cmd.Json = await SerializerAsync.SerializeJson(true);
                    await rep.Save();
                }
                else
                {
                    cmd.Cmd = ServerCommands.LogResultDeny;
                    cmd.Json = await SerializerAsync.SerializeJson(false);
                }
            }

            return cmd;
        }

    }
}
