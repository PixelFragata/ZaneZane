using System;
using System.Collections.Generic;
using System.Text;
using ZZ_ERP.Infra.CrossCutting.DTO.Interfaces;

namespace ZZ_ERP.Domain.Entities
{
    public class Policy : IPolicy
    {
        public string PolicyName { get; set; }
        public string PermissaoTela { get; set; }
        public string TipoPermissao { get; set; }

        public Policy(PermissaoTela permissaoTela, TipoPermissao tipoPermissao)
        {
            PermissaoTela = permissaoTela.NomeTela;
            TipoPermissao = tipoPermissao.Descricao;
            PolicyName = PermissaoTela + TipoPermissao;
        }
    }
}
