using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using ZZ_ERP.Infra.CrossCutting.Connections.Functions;
using ZZ_ERP.Infra.CrossCutting.DTO.EntitiesDTO;

namespace ZZ_ERP.Domain.Entities
{
    public class MovimentoEstoque : Entity
    {
        [Required]
        public bool IsEntrada { get; set; }
        [Required]
        public int Quantidade { get; set; }
        public string Observacao { get; set; }
        [Required]
        public DateTime Data { get; set; }
        public DateTime DataMovimento { get; set; }
        [Required]
        public long ServicoId { get; set; }
        public virtual Servico Servico { get; set; }
        [Required]
        public long EstoqueId { get; set; }
        public virtual Estoque Estoque { get; set; }
        [Required]
        public long TipoEntradaId { get; set; }
        public virtual TipoEntrada TipoEntrada { get; set; }
        public long DocumentoId { get; set; }


        public MovimentoEstoque()
        {
            Data = DateTime.Now;
        }
         
        public override EntityDto ConvertDto()
        {
            try
            {
                var dto = new MovimentoEstoqueDto
                {
                    Id = Id,
                    Codigo = Codigo,
                    IsEntrada = IsEntrada,
                    Quantidade = Quantidade,
                    ServicoId = ServicoId,
                    EstoqueId = EstoqueId,
                    TipoEntradaId = TipoEntradaId,
                    DataMovimento = DataMovimento,
                    DocumentoId = DocumentoId,
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
                if (TipoEntrada != null)
                {
                    dto.TipoEntrada = (TipoDto)TipoEntrada.ConvertDto();
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
                IsEntrada = movimentoEstoqueDto.IsEntrada;
                Quantidade = movimentoEstoqueDto.Quantidade;
                Observacao = movimentoEstoqueDto.Observacao;
                ServicoId = movimentoEstoqueDto.ServicoId;
                EstoqueId = movimentoEstoqueDto.EstoqueId;
                TipoEntradaId = movimentoEstoqueDto.TipoEntradaId;
                DataMovimento = movimentoEstoqueDto.DataMovimento;
                DocumentoId = movimentoEstoqueDto.DocumentoId;

                if (movimentoEstoqueDto.Servico != null)
                {
                    Servico.UpdateEntity(movimentoEstoqueDto.Servico);
                }
                if (movimentoEstoqueDto.Estoque != null)
                {
                    Estoque.UpdateEntity(movimentoEstoqueDto.Estoque);
                }
                if (movimentoEstoqueDto.TipoEntrada != null)
                {
                    Servico.UpdateEntity(movimentoEstoqueDto.TipoEntrada);
                }

            }
            catch (Exception e)
            {
                ConsoleEx.WriteError(e);
            }
        }
    }
}
