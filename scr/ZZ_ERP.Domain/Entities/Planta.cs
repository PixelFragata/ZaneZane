using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using ZZ_ERP.Infra.CrossCutting.Connections.Functions;
using ZZ_ERP.Infra.CrossCutting.DTO.EntitiesDTO;

namespace ZZ_ERP.Domain.Entities
{
    public class Planta : Entity
    {
        [Required]
        public string Documento { get; set; }
        public string InscricaoEstadual { get; set; }
        public string NomeFantasia { get; set; }
        [Required]
        public string RazaoSocial { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }

        public long EnderecoId { get; set; }
        public virtual Endereco Endereco { get; set; }

        public Planta()
        {
            Endereco = new Endereco();
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
                    Telefone = Telefone,
                    IE = InscricaoEstadual
                };

                dto.ValidatorDocument(Documento);
                dto.Endereco = (EnderecoDto)Endereco.ConvertDto();

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
                var dadosDto = (UserDadosDto)dto;
                Codigo = dadosDto.Codigo;
                if (!string.IsNullOrWhiteSpace(dadosDto.CPF))
                {
                    Documento = dadosDto.CPF;
                }
                else if (!string.IsNullOrWhiteSpace(dadosDto.CNPJ))
                {
                    Documento = dadosDto.CNPJ;
                }
                NomeFantasia = dadosDto.NomeFantasia;
                RazaoSocial = dadosDto.RazaoSocial;
                Email = dadosDto.Email;
                Telefone = dadosDto.Telefone;
                EnderecoId = dadosDto.Endereco.Id;
                Endereco.UpdateEntity(dadosDto.Endereco);
                InscricaoEstadual = dadosDto.IE;
            }
            catch (Exception e)
            {
                ConsoleEx.WriteError(e);
            }
        }
    }
}
