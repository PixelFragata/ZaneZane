using System;
using System.Collections.Generic;
using System.Text;

namespace ZZ_ERP.Infra.CrossCutting.DTO.EntitiesDTO
{
    public class TabelaDto : EntityDto
    {
        public string Description { get; set; }
        public float Price { get; set; }
        public DateTime TableDate { get; set; }
        public long ServicoId { get; set; }
    }
}
