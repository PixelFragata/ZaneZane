﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using ZZ_ERP.Infra.CrossCutting.Connections.Functions;
using ZZ_ERP.Infra.CrossCutting.DTO.EntitiesDTO;

namespace ZZ_ERP.Domain.Entities
{
    public class Estoque : Entity
    {
        [Required]
        public string Descricao { get; set; }
        [Required]
        public long PlantaId { get; set; }
        public virtual Planta Planta { get; set; }

        public override EntityDto ConvertDto()
        {
            try
            {
                var dto = new TipoDto {Id = Id, Description = Descricao, Codigo = Codigo};

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
                var tipoDto = (TipoDto)dto;
                Descricao = tipoDto.Description;
                Codigo = tipoDto.Codigo;
            }
            catch (Exception e)
            {
                ConsoleEx.WriteError(e);
            }

        }
    }
}
