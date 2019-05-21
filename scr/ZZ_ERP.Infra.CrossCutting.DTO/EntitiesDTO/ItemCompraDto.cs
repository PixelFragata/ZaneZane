using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.RegularExpressions;

namespace ZZ_ERP.Infra.CrossCutting.DTO.EntitiesDTO
{
    public class ItemCompraDto : EntityDto
    {
        public string Observacao { get; set; }
        public float Quantidade { get; set; }
        public float ValorUnitario { get; set; }
        public float ValorTotal { get; set; }
        public long CompraManualId { get; set; }

        public long UnidadeId { get; set; }
        public TipoSiglaDto Unidade { get; set; }

        public long ServicoId { get; set; }
        public ServicoDto ServicoDto { get; set; }



    }
}
