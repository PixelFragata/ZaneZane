using System;
using System.Collections.Generic;
using System.Text;
using ZZ_ERP.Infra.CrossCutting.Connections.Functions;
using ZZ_ERP.Infra.CrossCutting.DTO.EntitiesDTO;

namespace ZZ_ERP.Domain.Entities
{
    public class UnidadeMedida : Entity
    {
        public string Sigla { get; set; }
        public string Descricao { get; set; }

        public override EntityDto ConvertDto()
        {
            try
            {
                var dto = new UnidadeMedidaDto {Id = Id, Sigla = Sigla, Description = Descricao};

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
