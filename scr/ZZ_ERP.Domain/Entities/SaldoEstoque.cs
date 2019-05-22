using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using ZZ_ERP.Infra.CrossCutting.Connections.Functions;
using ZZ_ERP.Infra.CrossCutting.DTO.EntitiesDTO;

namespace ZZ_ERP.Domain.Entities
{
    public class SaldoEstoque : Entity
    {
        [Required]
        public float Quantidade { get; set; }
        public string Observacao { get; set; }
        [Required]
        public DateTime Data { get; set; }
        [Required]
        public long ServicoId { get; set; }
        public virtual Servico Servico { get; set; }
        [Required]
        public long EstoqueId { get; set; }
        public virtual Estoque Estoque { get; set; }
        [Required]
        public long PlantaId { get; set; }
        public virtual Planta Planta { get; set; }

        public override EntityDto ConvertDto()
        {
            try
            {
                var dto = new MovimentoEstoqueDto
                {
                    Id = Id,
                    Codigo = Codigo,
                    Quantidade = Quantidade,
                    ServicoId = ServicoId,
                    EstoqueId = EstoqueId,
                    Observacao = Observacao
                };
                if (Servico != null)
                {
                    dto.Servico = (ServicoDto) Servico.ConvertDto();
                }
                if (Estoque != null)
                {
                    dto.Estoque = (TipoDto)Estoque.ConvertDto();
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
                var movimentoEstoqueDto = (MovimentoEstoqueDto)dto;
                Codigo = movimentoEstoqueDto.Codigo;
                Quantidade = movimentoEstoqueDto.Quantidade;
                Observacao = movimentoEstoqueDto.Observacao;
                ServicoId = movimentoEstoqueDto.ServicoId;
                EstoqueId = movimentoEstoqueDto.EstoqueId;

                if (movimentoEstoqueDto.Servico != null)
                {
                    Servico.UpdateEntity(movimentoEstoqueDto.Servico);
                }
                if (movimentoEstoqueDto.Estoque != null)
                {
                    Estoque.UpdateEntity(movimentoEstoqueDto.Estoque);
                }

            }
            catch (Exception e)
            {
                ConsoleEx.WriteError(e);
            }
        }
    }
}
