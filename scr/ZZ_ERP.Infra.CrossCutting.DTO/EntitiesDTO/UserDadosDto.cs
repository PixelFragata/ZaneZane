using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ZZ_ERP.Infra.CrossCutting.DTO.EntitiesDTO
{
    public class UserDadosDto : EntityDto
    {
        [RegularExpression("^\\d{3}\\.\\d{3}\\.\\d{3}-\\d{2}$", ErrorMessage = "Informe um CPF valido")]
        public string CPF { get; set; }
        public string CNPJ { get; set; }
        public string IE { get; set; }
        public string Codigo { get; set; }
        public string NomeFantasia { get; set; }
        public string RazaoSocial { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }

        public EnderecoDto Endereco { get; set; }
    }
}
