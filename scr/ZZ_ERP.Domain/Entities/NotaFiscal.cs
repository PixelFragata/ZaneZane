using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using ZZ_ERP.Infra.CrossCutting.Connections.Functions;
using ZZ_ERP.Infra.CrossCutting.DTO.EntitiesDTO;

namespace ZZ_ERP.Domain.Entities
{
    public class NotaFiscal : Entity
    {
        public string NF { get; set; }
        public string Serie { get; set; }
        public string NaturezaOperacao { get; set; }
        public string ProtocoloAutorizacao { get; set; }
        public DateTime DataEmissao { get; set; }
        public float Quantidade { get; set; }
        public float ValorTotalProdutos { get; set; }
        public float ValorTotalNota { get; set; }
        public float BaseICMS { get; set; }
        public float ValorICMS { get; set; }
        public float BaseICMSSubstituicao { get; set; }
        public float ValorICMSSubstituicao { get; set; }
        public float ValorIPI { get; set; }
        public float ICMS { get; set; }
        public float IPI { get; set; }

        public override EntityDto ConvertDto()
        {
            try
            {
                var dto = new ItemNfDto
                {
                    Id = Id,
                    Codigo = Codigo,
                    BaseICMS = BaseICMS,
                    ValorICMS = ValorICMS,
                    ValorIPI = ValorIPI,
                    ICMS = ICMS,
                    IPI = IPI
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
                var item = (ItemNfDto)dto;
                Codigo = item.Codigo;
                Quantidade = item.Quantidade;
                BaseICMS = item.BaseICMS;
                ValorICMS = item.ValorICMS;
                ValorIPI = item.ValorIPI;
                ICMS = item.ICMS;
                IPI = item.IPI;
            }
            catch (Exception e)
            {
                ConsoleEx.WriteError(e);
            }
        }
    }
}
