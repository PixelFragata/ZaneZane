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
    public class MovimentoEstoqueManager : EntityManager<MovimentoEstoque>
    {

        public override async Task<Command> GetAll(Command command)
        {
            Command cmd = new Command(command);
            try
            {
                var entities = await MyRepository.Get(null,null, "TipoEntrada,Estoque,Servico,Servico.UnidadeMedida,Servico.TipoServico");
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
                var dto = await SerializerAsync.DeserializeJson<MovimentoEstoqueDto>(command.Json);

                var movimentoEstoques  = await MyRepository.Get(t => t.Codigo.Equals(dto.Codigo));

                if (movimentoEstoques != null && !movimentoEstoques.Any())
                {
                    var servicoRep = new Repository<Servico>(Context);
                    var servico = await servicoRep.GetById(dto.ServicoId);

                    var tipoEntradaRep = new Repository<TipoEntrada>(Context);
                    var tipoEntrada = await tipoEntradaRep.GetById(dto.TipoEntradaId);

                    var estoqueRep = new Repository<Estoque>(Context);
                    var estoque = await estoqueRep.GetById(dto.EstoqueId);

                    if ((servico == null && (dto.Servico == null || !dto.Servico.ControlaEstoque)) ||
                        (servico!= null && !servico.ControlaEstoque))
                    {
                        cmd.Cmd = ServerCommands.NaoControlaEstoque;
                        ConsoleEx.WriteLine(ServerCommands.NaoControlaEstoque);
                        return cmd;
                    }
                    var entity = new MovimentoEstoque();
                    entity.UpdateEntity(dto);

                    if (tipoEntrada != null)
                    {
                        entity.TipoEntrada = tipoEntrada;
                    }
                    if (estoque != null)
                    {
                        entity.Estoque = estoque;
                    }
                    if (servico != null)
                    {
                        entity.Servico = servico;
                    }

                    if (String.IsNullOrEmpty(entity.Codigo))
                    {
                        if (!string.IsNullOrEmpty(entity.Servico.Codigo))
                        {
                            entity.Codigo = entity.Servico.Codigo + DateTime.Now.ToString();
                        }
                    }

                    if (entity.DataMovimento == DateTime.MinValue)
                    {
                        entity.DataMovimento = entity.Data;
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
                var dto = await SerializerAsync.DeserializeJson<MovimentoEstoqueDto>(command.Json);

                var compras = await MyRepository.Get(c=> c.Id==cmd.EntityId,null, "TipoEntrada,Estoque,Servico,Servico.UnidadeMedida,Servico.TipoServico");
                var compraManual = compras.FirstOrDefault();

                if (compraManual != null)
                {
                    compraManual.UpdateEntity(dto);

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
