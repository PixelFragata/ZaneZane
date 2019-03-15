using System;
using System.Collections.Generic;
using System.Text;
using ZZ_ERP.Infra.CrossCutting.Connections.Functions;
using ZZ_ERP.Infra.CrossCutting.DTO.EntitiesDTO;

namespace ZZ_ERP.Domain.Entities
{
    public class TipoServico : Entity
    {
        public string DescricaoServico { get; set; }

        public override EntityDto ConvertDto()
        {
            
            try
            {
                var dto = new TipoServicoDto {Id = Id, Description = DescricaoServico};

                return dto;
            }
            catch (Exception e)
            {
                ConsoleEx.WriteError(e);
                return null;
            }
        }
    }
}
