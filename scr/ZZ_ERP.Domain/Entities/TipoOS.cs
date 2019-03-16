﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using ZZ_ERP.Infra.CrossCutting.Connections.Functions;
using ZZ_ERP.Infra.CrossCutting.DTO.EntitiesDTO;

namespace ZZ_ERP.Domain.Entities
{
    public class TipoOS : Entity
    {
        [Required]
        public string Descricao { get; set; }

        public override EntityDto ConvertDto()
        { 
            try
            {
                var dto = new TipoDto {Id = Id, Description = Descricao };

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