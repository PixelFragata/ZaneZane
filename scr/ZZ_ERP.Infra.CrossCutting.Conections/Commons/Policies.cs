using System;
using System.Collections.Generic;
using System.Text;

namespace ZZ_ERP.Infra.CrossCutting.Connections.Commons
{
    public static class Policies
    {
        public const string RoleAdmin = "Admin";
        public const string RoleCliente = "Cliente";
    }

    public class Policy
    {
        public string PolicyName { get; set; }
        public string PermissaoTela { get; set; }
        public string TipoPermissao { get; set; }

        public Policy(string permissaoTela, string tipoPermissao)
        {
            PermissaoTela = permissaoTela;
            TipoPermissao = tipoPermissao;
            PolicyName = PermissaoTela + TipoPermissao;
        }
    }
}
