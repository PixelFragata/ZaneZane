using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using ZZ_ERP.Infra.CrossCutting.Connections.Functions;
using ZZ_ERP.Infra.CrossCutting.DTO.EntitiesDTO;

namespace ZZ_ERP.Domain.Entities
{
    public class TipoEntrada : Entity
    {
        [Required]
        public string Descricao { get; set; }
        public bool ControlaEstoque { get; set; }
        public string NomeEntity { get; set; }

        public override EntityDto ConvertDto()
        {
            try
            {
                var dto = new TipoEntradaDto { Id = Id, Description = Descricao, Codigo = Codigo,
                    NomeEntity = NomeEntity, ControlaEstoque = ControlaEstoque};

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
                var tipoEntradaDto = (TipoEntradaDto)dto;
                Descricao = tipoEntradaDto.Description;
                NomeEntity = tipoEntradaDto.NomeEntity;
                ControlaEstoque = tipoEntradaDto.ControlaEstoque;
                Codigo = tipoEntradaDto.Codigo;
            }
            catch (Exception e)
            {
                ConsoleEx.WriteError(e);
            }

        }
    }
}
