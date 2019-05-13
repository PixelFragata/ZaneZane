using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using ZZ_ERP.Infra.CrossCutting.Connections.Functions;
using ZZ_ERP.Infra.CrossCutting.DTO.EntitiesDTO;

namespace ZZ_ERP.Domain.Entities
{
    public class FuncionarioEstoque : Entity
    {

        [Required]
        public long FuncionarioId { get; set; }
        public virtual Funcionario Funcionario { get; set; }

        [Required]
        public long EstoqueId { get; set; }
        public virtual Estoque Estoque { get; set; }

        public FuncionarioEstoque()
        {
            Funcionario = new Funcionario();
            Estoque = new Estoque();
        }

        public override EntityDto ConvertDto()
        {
            try
            {
                var dto = new DtoLigacao
                {
                    Id = Id, Codigo = Codigo, FirstDtoId = FuncionarioId, FirstDto = Funcionario.ConvertDto(),
                    SecondDtoId = EstoqueId, SecondDto = Estoque.ConvertDto()
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
                var dtoLigacao = (DtoLigacao)dto;
                Codigo = dtoLigacao.Codigo;
                FuncionarioId = dtoLigacao.FirstDtoId;
                if (dtoLigacao.FirstDto != null)
                {
                    Funcionario.UpdateEntity(dtoLigacao.FirstDto);
                }
                EstoqueId = dtoLigacao.SecondDtoId;
                if (dtoLigacao.SecondDto != null)
                {
                    Estoque.UpdateEntity(dtoLigacao.SecondDto);
                }
            }
            catch (Exception e)
            {
                ConsoleEx.WriteError(e);
            }

        }
    }
}
