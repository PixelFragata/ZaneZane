using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Remotion.Linq.Parsing.Structure.IntermediateModel;
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
        public DateTime LastDateUpdate { get; private set; } 

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

                if (await VerififyMovimentoEstoque(dto))
                {
                    var servicoRep = new Repository<Servico>(Context);
                    var servico = await servicoRep.GetById(dto.ServicoId);

                    var tipoEntradaRep = new Repository<TipoEntrada>(Context);
                    var tipoEntrada = await tipoEntradaRep.GetById(dto.TipoEntradaId);

                    var estoqueRep = new Repository<Estoque>(Context);
                    var estoque = await estoqueRep.GetById(dto.EstoqueId);

                    var entity = new MovimentoEstoque();
                    entity.UpdateEntity(dto);

                    entity.Estoque = estoque;
                    entity.Servico = servico;
                    entity.TipoEntrada = tipoEntrada;

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
                cmd.Cmd = ServerCommands.LogResultDeny;
                cmd.Json = await SerializerAsync.SerializeJson(false);
            }
            catch (Exception e)
            {
                ConsoleEx.WriteError(e);

            }

            return cmd; 
        }

        public async Task<bool> VerififyMovimentoEstoque(MovimentoEstoqueDto dto)
        {
            try
            {
                var movimentoEstoques = await MyRepository.Get(t => t.Codigo.Equals(dto.Codigo));

                var servicoRep = new Repository<Servico>(Context);
                var servico = await servicoRep.GetById(dto.ServicoId);

                var tipoEntradaRep = new Repository<TipoEntrada>(Context);
                var tipoEntrada = await tipoEntradaRep.GetById(dto.TipoEntradaId);

                var estoqueRep = new Repository<Estoque>(Context);
                var estoque = await estoqueRep.GetById(dto.EstoqueId);

                if (movimentoEstoques == null || movimentoEstoques.Any())
                {
                    return false;
                }

                if (tipoEntrada == null || estoque == null || servico == null)
                {
                    ConsoleEx.WriteLine(ServerCommands.RequisitoNaoCadastrado);
                    return false;
                }

                if (!servico.ControlaEstoque)
                {
                    ConsoleEx.WriteLine(ServerCommands.NaoControlaEstoque);
                    return false;
                }

                var entity = new MovimentoEstoque();
                entity.UpdateEntity(dto);

                entity.Estoque = estoque;
                entity.Servico = servico;
                entity.TipoEntrada = tipoEntrada;

                var mySaldo = await GetSaldoEstoque(servico, estoque);

                int multiplicadorSaldo = 1;

                if (!entity.IsEntrada)
                {
                    multiplicadorSaldo = -1;
                }


                if (mySaldo == null || mySaldo.Quantidade + (multiplicadorSaldo * entity.Quantidade) <= 0)
                {
                    ConsoleEx.WriteLine(ServerCommands.SaldoInsuficiente);
                    return false;
                }

                return true;
            }
            catch (Exception e)
            {
                ConsoleEx.WriteError(e);
                return false;
            }
        }

        public async Task<SaldoEstoque> GetSaldoEstoque(Servico servico, Estoque estoque)
        {
            try
            {
                var updateDate = DateTime.Now;
                var saldo = new SaldoEstoque
                {
                    Data = updateDate,
                    EstoqueId = estoque.Id,
                    Estoque = estoque,
                    Servico = servico,
                    ServicoId = servico.Id,
                    Codigo = servico.Codigo + estoque.Codigo + updateDate.ToString(),
                    Quantidade = 0
                };

                var saldoRep = new Repository<SaldoEstoque>(Context);
                var lastRegister = saldoRep.Get(null, o => o.OrderByDescending(s => s.Id)).Result.FirstOrDefault();

                LastDateUpdate = lastRegister == null ? DateTime.MinValue : lastRegister.Data;

                var lastSaldos = await saldoRep.Get(s =>
                    s.Data == LastDateUpdate && s.EstoqueId == estoque.Id && s.ServicoId == servico.Id);

                if (lastSaldos != null)
                {
                    var saldoEstoques = lastSaldos.ToList();

                    var lastSaldo = saldoEstoques?
                        .Find(s => s.EstoqueId == estoque.Id && s.ServicoId == servico.Id);

                    if (lastSaldo != null)
                    {
                        saldo.Quantidade = lastSaldo.Quantidade;
                    }
                }

                var movimentos = await MyRepository.Get(m =>
                    m.Data > LastDateUpdate && m.EstoqueId == estoque.Id && m.ServicoId == servico.Id);

                if (movimentos == null) return saldo;

                var movimentoEstoques = movimentos.ToList();

                if (movimentoEstoques?.Count > 0)
                {
                    var servicoMovimentos = movimentoEstoques
                        .FindAll(m => m.ServicoId == servico.Id && m.EstoqueId == estoque.Id);

                    foreach (var movimento in servicoMovimentos)
                    {
                        int multiplicadorSaldo = 1;

                        if (!movimento.IsEntrada)
                        {
                            multiplicadorSaldo = -1;
                        }

                        saldo.Quantidade += movimento.Quantidade * multiplicadorSaldo;
                    }   
                }

                return saldo;
            }
            catch (Exception e)
            {
                ConsoleEx.WriteError(e);
                return null;
            }
        }

        public async Task<List<SaldoEstoque>> UpdateSaldoEstoque()
        {
            var saldoList = new List<SaldoEstoque>();
            try
            {
                var updateDate = DateTime.Now;
                var saldoRep = new Repository<SaldoEstoque>(Context);
                LastDateUpdate = saldoRep.Get(null, o => o.OrderByDescending(s => s.Id)).Result.FirstOrDefault().Data;
                var lastSaldos = await saldoRep.Get(s => s.Data == LastDateUpdate);

                var movimentos = await MyRepository.Get(m => m.Data > LastDateUpdate);

                var servicoRep = new Repository<Servico>(Context);
                var servicos = await servicoRep.Get(s => s.ControlaEstoque);

                var estoqueRep = new Repository<Estoque>(Context);
                var estoques = await estoqueRep.Get();

                var servicoList = servicos.ToList();
                var estoqueList = estoques.ToList();
                var movimentoEstoques = movimentos.ToList();
                var saldoEstoques = lastSaldos.ToList();

                if(movimentoEstoques?.Count() > 0)
                {
                    if (servicoList?.Count > 0 && estoqueList?.Count > 0)
                    {
                        
                        foreach (var servico in servicoList)
                        {
                            foreach (var estoque in estoqueList)
                            {
                                var saldo = new SaldoEstoque
                                {
                                    Data = updateDate,
                                    EstoqueId = estoque.Id,
                                    Estoque = estoque,
                                    Servico = servico,
                                    ServicoId = servico.Id,
                                    Codigo = servico.Codigo + estoque.Codigo + updateDate.ToString()
                                };

                                var lastSaldo = saldoEstoques
                                    .Find(s => s.EstoqueId == estoque.Id && s.ServicoId == servico.Id);

                                if (lastSaldo != null)
                                {
                                    saldo.Quantidade = lastSaldo.Quantidade;
                                }
                                else
                                {
                                    saldo.Quantidade = 0;
                                }

                                var servicoMovimentos = movimentoEstoques
                                    .FindAll(m => m.ServicoId == servico.Id && m.EstoqueId == estoque.Id);

                                foreach (var movimento in servicoMovimentos)
                                {
                                    int multiplicadorSaldo = 1;

                                    if (!movimento.IsEntrada)
                                    {
                                        multiplicadorSaldo = -1;
                                    }

                                    saldo.Quantidade += movimento.Quantidade * multiplicadorSaldo;
                                }

                                saldoList.Add(saldo);
                            }
                        }
                    }

                    await saldoRep.InsertList(saldoList);
                    await saldoRep.Save();
                }
            }
            catch (Exception e)
            {
                ConsoleEx.WriteError(e);
                throw;
            }

            return saldoList;
        }

    }
}
