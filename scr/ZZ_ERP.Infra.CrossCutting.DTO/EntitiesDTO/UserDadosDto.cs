using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.RegularExpressions;

namespace ZZ_ERP.Infra.CrossCutting.DTO.EntitiesDTO
{
    public class UserDadosDto : EntityDto
    {
        [RegularExpression("^\\d{3}\\.\\d{3}\\.\\d{3}-\\d{2}$", ErrorMessage = "Informe um CPF valido")]
        public string CPF { get; set; }
        [RegularExpression("^\\d{2}\\.\\d{3}\\.\\d{3}\\/\\d{4}-\\d{2}$", ErrorMessage = "Informe um CNPJ valido")]
        public string CNPJ { get; set; }
        [RegularExpression("^\\d{3}\\.\\d{3}\\.\\d{3}\\.\\d{3}$", ErrorMessage = "Informe uma Inscrição Estadual valida")]
        public string IE { get; set; }
        public string Codigo { get; set; }
        public string NomeFantasia { get; set; }
        public string RazaoSocial { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }

        public EnderecoDto Endereco { get; set; }

        public void ValidatorDocument(string document)
        {
            var cpfValidator = new Regex("^\\d{3}\\.\\d{3}\\.\\d{3}-\\d{2}$");
            var cnpjValidator = new Regex("^\\d{2}\\.\\d{3}\\.\\d{3}\\/\\d{4}-\\d{2}$");

            if (cpfValidator.IsMatch(document))
            {
                CPF = document;
            }
            else if (cnpjValidator.IsMatch(document))
            {
                CNPJ = document;
            }
        }
    }
}
