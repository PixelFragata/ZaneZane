using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ZZ_ERP.Infra.CrossCutting.DTO.EntitiesDTO
{
    public class EnderecoDto : EntityDto
    {
        [RegularExpression("^[0-9]{8}$", ErrorMessage = "Informe um CEP valido")]
        public string Cep { get; set; }
        public int Numero { get; set; }
        [MinLength(3)]
        public string Logradouro { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public int Ibge { get; set; }
        public int GIACode { get; set; }
        [MinLength(3)]
        public string Cidade { get; set; } 
        public long CidadeId { get; set; }
        [MinLength(2)]
        [MaxLength(2)]
        public string Estado { get; set; }
    }
}
