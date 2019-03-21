using System;
using System.Collections.Generic;
using System.Text;
using ZZ_ERP.Infra.CrossCutting.Connections.Functions;
using ZZ_ERP.Infra.CrossCutting.DTO.EntitiesDTO;

namespace ZZ_ERP.Domain.Entities
{
    public class Estado : Entity
    {
        public string Sigla { get; set; }
        public string Descricao { get; set; }
        public int Ibge { get; set; }

        public override EntityDto ConvertDto()
        {
            try
            {
                var dto = new TipoSiglaDto {Id = Ibge, Sigla = Sigla, Description = Descricao};

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
                var tipoDto = (TipoSiglaDto)dto;
                Descricao = tipoDto.Description;
                Sigla = tipoDto.Sigla;
                Ibge = (int)tipoDto.Id;
            }
            catch (Exception e)
            {
                ConsoleEx.WriteError(e);
            }

        }
    }
}
