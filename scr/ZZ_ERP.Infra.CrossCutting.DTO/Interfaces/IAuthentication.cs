using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZZ_ERP.Infra.CrossCutting.DTO.IdentityDTO;

namespace ZZ_ERP.Infra.CrossCutting.DTO.Interfaces
{
    public interface IAuthentication
    {
        Task<LoginResultDto> Authenticate(string username, string password);
        Task<bool> Logout();
    }
}
