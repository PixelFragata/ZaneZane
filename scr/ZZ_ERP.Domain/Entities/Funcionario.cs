using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using ZZ_ERP.Infra.CrossCutting.Connections.Functions;
using ZZ_ERP.Infra.CrossCutting.DTO.EntitiesDTO;

namespace ZZ_ERP.Domain.Entities
{
    public class Funcionario : Entity
    {
        [Required]
        public string Documento { get; set; }
        public string NomeFantasia { get; set; }
        [Required]
        public string RazaoSocial { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }

        public long EnderecoId { get; set; }
        public virtual Endereco Endereco { get; set; }
        [Required]
        public long PlantaId { get; set; }
        public virtual Planta Planta { get; set; }

        public virtual List<FuncionarioEstoque> FuncionarioEstoques { get; set; }
        [NotMapped]
        public virtual List<Estoque> Estoques { get; set; }

        public Funcionario()
        {
            Endereco = new Endereco();
            FuncionarioEstoques = new List<FuncionarioEstoque>();
            Estoques = new List<Estoque>();
            
        }
         
        public override EntityDto ConvertDto()
        {
            try
            {
                var dto = new UserDadosDto
                {
                    Id = Id,
                    Codigo = Codigo,
                    NomeFantasia = NomeFantasia,
                    RazaoSocial = RazaoSocial,
                    Email = Email,
                    Telefone = Telefone
                };

                dto.ValidatorDocument(Documento);
                dto.Endereco = (EnderecoDto) Endereco.ConvertDto();

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
                var tipoDto = (UserDadosDto)dto;
                Codigo = tipoDto.Codigo;
                if (!string.IsNullOrWhiteSpace(tipoDto.CPF))
                {
                    Documento = tipoDto.CPF;
                }
                else if (!string.IsNullOrWhiteSpace(tipoDto.CNPJ))
                {
                    Documento = tipoDto.CNPJ;
                }
                NomeFantasia = tipoDto.NomeFantasia;
                RazaoSocial = tipoDto.RazaoSocial;
                Email = tipoDto.Email;
                Telefone = tipoDto.Telefone;
                EnderecoId = tipoDto.Endereco.Id;
                Endereco.UpdateEntity(tipoDto.Endereco);

            }
            catch (Exception e)
            {
                ConsoleEx.WriteError(e);
            }
        }
    }
}
