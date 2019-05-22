using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.RegularExpressions;

namespace ZZ_ERP.Infra.CrossCutting.DTO.EntitiesDTO
{
    public class MovimentoEstoqueDto : EntityDto
    {
        [Required]
        public bool IsEntrada { get; set; }
        [Required]
        public float Quantidade { get; set; }
        public string Observacao { get; set; }
        [Required]
        public long ServicoId { get; set; }
        public virtual ServicoDto Servico { get; set; }
        [Required]
        public long EstoqueId { get; set; }
        public virtual TipoDto Estoque { get; set; }
        [Required]
        public long TipoEntradaId { get; set; }
        public virtual TipoDto TipoEntrada { get; set; }
        public DateTime DataMovimento { get; set; }
        public long DocumentoId { get; set; }
    }
}
