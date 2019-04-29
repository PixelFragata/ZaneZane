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


        public override EntityDto ConvertDto()
        {
            throw new NotImplementedException();
        }

        public override void UpdateEntity(EntityDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
