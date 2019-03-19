using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using ZZ_ERP.Infra.CrossCutting.Connections.Functions;
using ZZ_ERP.Infra.CrossCutting.DTO.EntitiesDTO;

namespace ZZ_ERP.Domain.Entities
{
    public class TabelaCusto : Entity
    {
        public string Descricao { get; set; }
        public float Preco { get; set; }
        public DateTime DataTabela { get; set; }

        public long ServicoId { get; set; }
        public virtual Servico Servico { get; set; }

        public override EntityDto ConvertDto()
        { 
            try
            {
                var dto = new TabelaDto {Id = Id, Description = Descricao, Price = Preco, TableDate = DataTabela};

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
                var tipoDto = (TabelaDto)dto;
                Descricao = tipoDto.Description;
                Preco = tipoDto.Price;
                DataTabela = tipoDto.TableDate;
            }
            catch (Exception e)
            {
                ConsoleEx.WriteError(e);
            }

        }
    }
}
