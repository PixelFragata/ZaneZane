using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using ZZ_ERP.Infra.CrossCutting.Connections.Functions;
using ZZ_ERP.Infra.CrossCutting.DTO.EntitiesDTO;

namespace ZZ_ERP.Domain.Entities
{
    public class Servico : Entity
    {
        public string DescricaoCompleta { get; set; }
        public string DescricaoResumida { get; set; }
        public string Observacoes { get; set; }
        public bool ControlaEstoque { get; set; }

        public long TipoServicoId { get; set; }
        public virtual TipoServico TipoServico { get; set; }

        public long UnidadeMedidaId { get; set; }
        public virtual UnidadeMedida UnidadeMedida { get; set; }
         
        public long CentroCustoId { get; set; }
        public virtual CentroCustoSintetico CentroCusto { get; set; }

        public virtual ICollection<TabelaCusto> TabelasCusto { get; set; }


        public Servico()
        {
            TabelasCusto = new List<TabelaCusto>();
        }

        public override EntityDto ConvertDto()
        { 
            try
            {
                var dto = new ServicoDto
                {
                    Id = Id, DescricaoCompleta = DescricaoCompleta, Codigo = Codigo, DescricaoResumida = DescricaoResumida, Observacoes = Observacoes,
                    TipoServicoId = TipoServicoId, UnidadeMedidaId = UnidadeMedidaId, CentroCustoId = CentroCustoId, ControlaEstoque = ControlaEstoque
                };

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
                var tipoDto = (ServicoDto)dto;
                Codigo = tipoDto.Codigo;
                DescricaoResumida = tipoDto.DescricaoResumida;
                DescricaoCompleta = tipoDto.DescricaoCompleta;
                Observacoes = tipoDto.Observacoes;
                TipoServicoId = tipoDto.TipoServicoId;
                UnidadeMedidaId = tipoDto.UnidadeMedidaId;
                CentroCustoId = tipoDto.CentroCustoId;
                ControlaEstoque = tipoDto.ControlaEstoque;
            }
            catch (Exception e)
            {
                ConsoleEx.WriteError(e);
            } 

        }
    }
}
