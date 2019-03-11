using System;
using System.Collections.Generic;
using System.Text;

namespace ZZ_ERP.Infra.CrossCutting.DTO.Interfaces
{
    public interface IPolicy
    {
        string PolicyName { get; set; }
        string PermissaoTela { get; set; }
        string TipoPermissao { get; set; }
    }
}
