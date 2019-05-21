using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.RegularExpressions;

namespace ZZ_ERP.Infra.CrossCutting.DTO.EntitiesDTO
{
    public class ItemNfDto : EntityDto
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
    }
}
