using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ZZ_ERP.Infra.CrossCutting.DTO.Interfaces
{
    public interface IAuthentication
    {
        Task<bool> Authenticate(string username, string password);
        Task<bool> Logout();
    }
}
