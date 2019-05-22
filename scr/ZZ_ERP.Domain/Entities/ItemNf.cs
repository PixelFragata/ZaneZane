using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using ZZ_ERP.Infra.CrossCutting.Connections.Functions;
using ZZ_ERP.Infra.CrossCutting.DTO.EntitiesDTO;

namespace ZZ_ERP.Domain.Entities
{
    public class ItemNf : Entity
    {
        public string Descricao { get; set; }
        public string NCM { get; set; }
        public string CST { get; set; }
        public string CFOP { get; set; }
        public string Unidade { get; set; }
        public float Quantidade { get; set; }
        public float ValorUnitario { get; set; }
        public float ValorTotal { get; set; }
        public float BaseICMS { get; set; }
        public float ValorICMS { get; set; }
        public float ValorIPI { get; set; }
        public float ICMS { get; set; }
        public float IPI { get; set; }
        [Required]
        public long PlantaId { get; set; }
        public virtual Planta Planta { get; set; }

        public override EntityDto ConvertDto()
        {
            try
            {
                var dto = new ItemNfDto
                {
                    Id = Id,
                    Codigo = Codigo,
                    CST = CST,
                    CFOP = CFOP,
                    Descricao = Descricao,
                    Unidade = Unidade,
                    Quantidade = Quantidade,
                    NCM = NCM,
                    ValorUnitario = ValorUnitario,
                    ValorTotal = ValorTotal,
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
                Descricao = item.Descricao;
                CST = item.CST;
                CFOP = item.CFOP;
                Unidade = item.Unidade;
                Quantidade = item.Quantidade;
                NCM = item.NCM;
                ValorUnitario = item.ValorUnitario;
                ValorTotal = item.ValorTotal;
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
