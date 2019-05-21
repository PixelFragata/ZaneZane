using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using ZZ_ERP.Infra.CrossCutting.Connections.Functions;
using ZZ_ERP.Infra.CrossCutting.DTO.EntitiesDTO;

namespace ZZ_ERP.Domain.Entities
{
    public class CompraManual : Entity
    {
        public DateTime DataEmissao { get; set; }
        public string Observacao { get; set; }
        public float ValorTotal { get; set; }
        public bool ControlaEstoque { get; set; }
        public bool MovimentouEstoque { get; set; }

        public long TipoEntradaId { get; set; }
        public virtual TipoEntrada TipoEntrada { get; set; }

        public long FornecedorId { get; set; }
        public virtual Fornecedor Fornecedor { get; set; }

        public virtual List<ItemCompra> Itens { get; set; }

        public CompraManual()
        {
            MovimentouEstoque = false;
            Itens = new List<ItemCompra>();
        }

        public override EntityDto ConvertDto()
        {
            try
            {
                var dto = new CompraManualDto
                {
                    Id = Id,
                    Codigo = Codigo,
                    DataEmissao = DataEmissao,
                    Observacao = Observacao,
                    ValorTotal = ValorTotal,
                    ControlaEstoque = ControlaEstoque,
                    TipoEntradaId = TipoEntradaId,
                    FornecedorId = FornecedorId
                };

                if (TipoEntrada != null)
                {
                    dto.TipoEntrada = (TipoEntradaDto)TipoEntrada.ConvertDto();
                }

                if (Fornecedor != null)
                {
                    dto.Fornecedor = (UserDadosDto)Fornecedor.ConvertDto();
                }

                if (Itens != null && Itens.Count > 0)
                {
                    foreach (var item in Itens)
                    {
                        dto.Itens.Add((ItemCompraDto)item.ConvertDto());
                    }
                }

                return dto;
            }
            catch (Exception e)
            {
                ConsoleEx.WriteError(e);
                return null;
            }
        }

        public override void UpdateEntity(EntityDto dto)
        {
            try
            {
                var compra = (CompraManualDto)dto;
                Codigo = compra.Codigo;
                DataEmissao = compra.DataEmissao;
                DataEmissao = compra.DataEmissao;
                Observacao = compra.Observacao;
                ValorTotal = compra.ValorTotal;
                ControlaEstoque = compra.ControlaEstoque;
                MovimentouEstoque = compra.MovimentouEstoque;
                TipoEntradaId = compra.TipoEntradaId;
                FornecedorId = compra.FornecedorId;

                if (compra.TipoEntrada != null)
                {
                    TipoEntrada.UpdateEntity(compra.TipoEntrada);
                }

                if (compra.Fornecedor != null)
                {
                    Fornecedor.UpdateEntity(compra.Fornecedor);
                }

                if (compra.Itens != null && compra.Itens.Count > 0)
                {
                    Itens = new List<ItemCompra>();
                    foreach (var itemDto in compra.Itens)
                    {
                        var item = new ItemCompra();
                        item.UpdateEntity(itemDto);
                        Itens.Add(item);
                    }
                }
            }
            catch (Exception e)
            {
                ConsoleEx.WriteError(e);
            }
        }
    }
}
