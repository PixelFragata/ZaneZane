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
    public class CompraManualManager : EntityManager<CompraManual>
    {

        public override async Task<Command> GetAll(Command command)
        {
            Command cmd = new Command(command);
            try
            {
                var entities = await MyRepository.Get(null,null, "TipoEntrada,Fornecedor,Itens,Itens.Servico,Itens.Unidade");
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
                var dto = await SerializerAsync.DeserializeJson<CompraManualDto>(command.Json);

                var compras = await MyRepository.Get(t => t.Codigo.Equals(dto.Codigo));

                if (compras != null && !compras.Any())
                {
                    var unidadeMedidaRep = new Repository<UnidadeMedida>(Context);
                    var servicoRep = new Repository<Servico>(Context);

                    var tipoEntradaRep = new Repository<TipoEntrada>(Context);
                    var tipoEntrada = await tipoEntradaRep.GetById(dto.TipoEntradaId);

                    var fornecedorRep = new Repository<Fornecedor>(Context);
                    var fornecedor = await fornecedorRep.GetById(dto.FornecedorId);

                    var entity = new CompraManual();
                    entity.UpdateEntity(dto);

                    if (tipoEntrada != null)
                    {
                        entity.TipoEntrada = tipoEntrada;
                    }

                    if (fornecedor != null)
                    {
                        entity.Fornecedor = fornecedor;
                    }

                    if (entity.DataEmissao == DateTime.MinValue)
                    {
                        entity.DataEmissao = DateTime.Now;
                    }

                    if (String.IsNullOrEmpty(entity.Codigo))
                    {
                        if (!string.IsNullOrEmpty(entity.Fornecedor.NomeFantasia))
                        {
                            entity.Codigo = entity.Fornecedor.NomeFantasia + DateTime.Now.ToString();
                        }
                    }

                    if (entity.Itens != null && entity.Itens.Count > 0)
                    {
                        entity.ValorTotal = 0;
                        foreach (var item in entity.Itens)
                        {
                            var unidadeMedida = await unidadeMedidaRep.GetById(item.UnidadeId);
                            var servico = await servicoRep.GetById(item.ServicoId);

                            if (unidadeMedida != null)
                            {
                                item.Unidade = unidadeMedida;
                            }

                            if (servico != null)
                            {
                                item.Servico = servico;
                                item.Codigo = servico.Codigo + DateTime.Now.ToString();
                            }

                            entity.ValorTotal += item.ValorTotal;
                        }
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
                var dto = await SerializerAsync.DeserializeJson<CompraManualDto>(command.Json);

                var compras = await MyRepository.Get(c=> c.Id==cmd.EntityId,null, "TipoEntrada,Fornecedor,Itens,Itens.Servico,Itens.Unidade");
                var compraManual = compras.FirstOrDefault();

                if (compraManual != null)
                {
                    compraManual.UpdateEntity(dto);

                    if (compraManual.Itens != null && compraManual.Itens.Count > 0)
                    {
                        compraManual.ValorTotal = 0;
                        foreach (var item in compraManual.Itens)
                        {
                            compraManual.ValorTotal += item.ValorTotal;
                        }
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
