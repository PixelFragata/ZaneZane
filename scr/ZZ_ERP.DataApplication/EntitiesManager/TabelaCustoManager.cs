﻿using System;
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
    public class TabelaCustoManager : EntityManager<TabelaCusto>
    { 

        public override async Task<Command> Add(Command command)
        {
            Command cmd = new Command(command);
            try
            {
                var dto = await SerializerAsync.DeserializeJson<TabelaDto>(command.Json);

                var tabelas = await MyRepository.Get(t => t.ServicoId == dto.ServicoId && t.Descricao.Equals(dto.Description));

                if (tabelas != null && !tabelas.Any())
                {
                    var servicoRep = new Repository<Servico>(Context);
                    var servico = await servicoRep.GetById(dto.ServicoId);
                    if (servico != null)
                    {
                        var entity = new TabelaCusto();
                        entity.UpdateEntity(dto);
                        entity.DataTabela = DateTime.Now;
                        entity.Servico = servico;
                   
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
                var dto = await SerializerAsync.DeserializeJson<TabelaDto>(command.Json);

                var os = await MyRepository.GetById(cmd.EntityId);

                if (os != null)
                {
                    os.Descricao = dto.Description;
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
