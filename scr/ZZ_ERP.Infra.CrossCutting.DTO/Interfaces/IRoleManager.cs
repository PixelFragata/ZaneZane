using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace ZZ_ERP.Infra.CrossCutting.DTO.Interfaces
{
    public interface IRoleManager
    {
        Task<bool> CreateAsync(string roleName);
        List<IdentityRole> ListAll();
        Task<bool> AddRoleClaim(string roleName, string nomeTela, string tipoPermissao);
    }
}
