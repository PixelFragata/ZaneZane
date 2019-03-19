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
    public class ServicoManager : EntityManager<Servico>
    { 

        public override async Task<Command> Add(Command command)
        {
            Command cmd = new Command(command);
            try
            {
                var dto = await SerializerAsync.DeserializeJson<ServicoDto>(command.Json);

                var servicos = await MyRepository.Get(t => t.Codigo.Equals(dto.Codigo));

                if (servicos != null && !servicos.Any())
                {
                    var unidadeMedidaRep = new Repository<UnidadeMedida>(Context);
                    var unidadeMedida = await unidadeMedidaRep.GetById(dto.UnidadeMedidaId);

                    var tipoServicoRep = new Repository<TipoServico>(Context);
                    var tipoServico = await tipoServicoRep.GetById(dto.TipoServicoId);

                    var centroCustoRep = new Repository<CentroCustoSintetico>(Context);
                    var centroCusto = await centroCustoRep.GetById(dto.UnidadeMedidaId);

                    if (unidadeMedida != null && tipoServico != null && centroCusto != null)
                    {
                        cmd.Cmd = ServerCommands.LogResultOk;
                        var servico = new Servico{UnidadeMedida = unidadeMedida, TipoServico = tipoServico, CentroCusto = centroCusto};
                        servico.UpdateEntity(dto);
                        await MyRepository.Insert(servico);
                        cmd.Json = await SerializerAsync.SerializeJson(true);
                        await MyRepository.Save();
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
                var dto = await SerializerAsync.DeserializeJson<ServicoDto>(command.Json);

                var servico = await MyRepository.GetById(cmd.EntityId);

                if (servico != null)
                {
                    servico.UpdateEntity(dto);
                    var unidadeMedidaRep = new Repository<UnidadeMedida>(Context);
                    var unidadeMedida = await unidadeMedidaRep.GetById(dto.UnidadeMedidaId);

                    if (unidadeMedida != null)
                    {
                        servico.UnidadeMedida = unidadeMedida;
                    }
                    var tipoServicoRep = new Repository<TipoServico>(Context);
                    var tipoServico = await tipoServicoRep.GetById(dto.TipoServicoId);

                    if (tipoServico != null)
                    {
                        servico.TipoServico = tipoServico;
                    }

                    var centroCustoRep = new Repository<CentroCustoSintetico>(Context);
                    var centroCusto = await centroCustoRep.GetById(dto.UnidadeMedidaId);

                    if (centroCusto != null)
                    {
                        servico.CentroCusto = centroCusto;
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
