using System;
using System.Collections.Generic;
using System.Text;
using ZZ_ERP.Infra.CrossCutting.DTO.EntitiesDTO;

namespace ZZ_ERP.Domain.Entities
{
    public class TipoPermissao : Entity
    {
        public string Descricao { get; set; }
        public override EntityDto ConvertDto()
        {
            throw new NotImplementedException();
        }
    }
}
