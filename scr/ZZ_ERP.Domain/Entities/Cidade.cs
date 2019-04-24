﻿using System;
using System.Collections.Generic;
using System.Text;
using ZZ_ERP.Infra.CrossCutting.Connections.Functions;
using ZZ_ERP.Infra.CrossCutting.DTO.EntitiesDTO;

namespace ZZ_ERP.Domain.Entities
{
    public class Cidade : Entity
    {
        public string Descricao { get; set; }
        public int Ibge { get; set; }

        public long EstadoId { get; set; }
        public virtual Estado Estado { get; set; }

        public override EntityDto ConvertDto()
        {
            try
            {
                var dto = new TipoSiglaDto {Id = Ibge, Description = Descricao, Codigo = Codigo};

                return dto;
            }
            catch (Exception e)
            {
                ConsoleEx.WriteError(e);
                return null;
            }
        }

        public EntityDto ConvertDto(string uf)
        {
            try
            {
                var dto = (TipoSiglaDto) ConvertDto();
                dto.Sigla = uf;
                dto.Codigo = Codigo;

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
                Ibge = (int) tipoDto.Id;
                Codigo = tipoDto.Codigo;
            }
            catch (Exception e)
            {
                ConsoleEx.WriteError(e);
            }

        }
    }
}