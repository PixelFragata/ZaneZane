using System;
using System.Collections.Generic;
using System.Text;

namespace ZZ_ERP.Infra.CrossCutting.DTO.EntitiesDTO
{
    public class ServicoDto : EntityDto
    {
        public string Codigo { get; set; }
        public string DescricaoCompleta { get; set; }
        public string DescricaoResumida { get; set; }
        public string Observacoes { get; set; }
        public long TipoServicoId { get; set; }
        public long UnidadeMedidaId { get; set; }
        public long CentroCustoId { get; set; }
    }
}
