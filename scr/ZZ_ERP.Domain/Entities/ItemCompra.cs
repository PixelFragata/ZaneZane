using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using ZZ_ERP.Infra.CrossCutting.Connections.Functions;
using ZZ_ERP.Infra.CrossCutting.DTO.EntitiesDTO;

namespace ZZ_ERP.Domain.Entities
{
    public class ItemCompra : Entity
    {
        public string Observacao { get; set; }
        public float Quantidade { get; set; }
        public float ValorUnitario { get; set; }
        public float ValorTotal { get; set; }

        public long ServicoId { get; set; }
        public virtual Servico Servico { get; set; }

        public long UnidadeId { get; set; }
        public virtual UnidadeMedida Unidade { get; set; }

        public long CompraManualId { get; set; }
        public virtual CompraManual CompraManual { get; set; }

        public override EntityDto ConvertDto()
        {
            try
            {
                var dto = new ItemCompraDto
                {
                    Id = Id,
                    Codigo = Codigo,
                    Observacao = Observacao,
                    UnidadeId = UnidadeId,
                    Quantidade = Quantidade,
                    ValorUnitario = ValorUnitario,
                    ValorTotal = ValorTotal,
                    ServicoId = ServicoId,
                    CompraManualId = CompraManualId
                };

                if (Unidade != null)
                {
                    dto.Unidade = (TipoSiglaDto)Unidade.ConvertDto();
                }

                if (Servico != null)
                {
                    dto.ServicoDto = (ServicoDto)Servico.ConvertDto();
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
                var item = (ItemCompraDto)dto;
                Codigo = item.Codigo;
                Observacao = item.Observacao;
                UnidadeId = item.UnidadeId;
                Quantidade = item.Quantidade;
                ValorUnitario = item.ValorUnitario;
                ServicoId = item.ServicoId;
                CompraManualId = item.CompraManualId;

                if (item.ValorTotal <= 0)
                {
                    ValorTotal = Quantidade * ValorUnitario;
                }
                else
                {
                    ValorTotal = item.ValorTotal;
                }

                if (item.Unidade != null)
                {
                    Unidade.UpdateEntity(item.Unidade);
                }

                if (item.ServicoDto != null)
                {
                    Servico.UpdateEntity(item.ServicoDto);
                }

            }
            catch (Exception e)
            {
                ConsoleEx.WriteError(e);
            }
        }
    }
}
