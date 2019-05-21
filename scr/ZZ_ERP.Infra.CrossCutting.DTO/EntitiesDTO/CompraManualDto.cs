using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.RegularExpressions;

namespace ZZ_ERP.Infra.CrossCutting.DTO.EntitiesDTO
{
    public class CompraManualDto : EntityDto
    {
        public DateTime DataEmissao { get; set; }
        public string Observacao { get; set; }
        public float ValorTotal { get; set; }
        public bool ControlaEstoque { get; set; }
        public bool MovimentouEstoque { get; set; }

        public long TipoEntradaId { get; set; }
        public TipoEntradaDto TipoEntrada { get; set; }

        public long FornecedorId { get; set; }
        public UserDadosDto Fornecedor { get; set; }

        public List<ItemCompraDto> Itens { get; set; }

        public CompraManualDto()
        {
            Itens = new List<ItemCompraDto>();
        }

    }
}
