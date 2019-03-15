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
    public class TipoServicoManager : IEntityManager
    {

        public async Task<Command> GetAll(Command command)
        {
            Command cmd = new Command(command);

            using (var context = new ZZContext())
            {
                var tiposRep = new Repository<TipoServico>(context);
                var tiposList = await tiposRep.Get();
                var tipoServicos = tiposList.ToList();

                if (tipoServicos.Any())
                {
                    cmd.Cmd = ServerCommands.LogResultOk;
                    var nameTipos = tipoServicos.Select(t => t.ConvertDto()).ToList();
                    cmd.Json = await SerializerAsync.SerializeJsonList(nameTipos);
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
            cmd.Json = await SerializerAsync.DeserializeJson<string>(command.Json);
            using (var context = new ZZContext())
            {
                var rep = new Repository<TipoServico>(context);
                var tipos = await rep.Get(t => t.DescricaoServico.Equals(cmd.Json));

                if (!tipos.Any())
                {
                    cmd.Cmd = ServerCommands.LogResultOk;
                    await rep.Insert(new TipoServico {DescricaoServico = cmd.Json });
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
            var dto = await SerializerAsync.DeserializeJson<TipoServicoDto>(command.Json);
            using (var context = new ZZContext())
            {
                var rep = new Repository<TipoServico>(context);
                var tipoServico = await rep.GetById(cmd.EntityId);

                if (tipoServico != null)
                {
                    tipoServico.DescricaoServico = dto.Description;
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
                var rep = new Repository<TipoServico>(context);
                var tipo = await rep.GetById(cmd.EntityId);

                if (tipo != null)
                {
                    tipo.IsActive = false;
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
