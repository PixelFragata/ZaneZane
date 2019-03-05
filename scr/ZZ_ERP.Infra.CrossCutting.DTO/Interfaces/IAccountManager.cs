using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ZZ_ERP.Infra.CrossCutting.DTO.Interfaces
{
    public interface IAccountManager
    {
        Task<bool> CreateAsync(string username, string email, string password, string roles);
        List<IAccount> ListAll();
        List<IAccount> GetUserByUsername(string username);
        List<IAccount> GetUserByEmail(string email);
        bool DeleteUser(string id);
    }
}
