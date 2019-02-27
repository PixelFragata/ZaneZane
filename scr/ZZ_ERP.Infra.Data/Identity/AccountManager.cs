using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using ZZ_ERP.Domain.Account;
using ZZ_ERP.Infra.Data.Contexts;

namespace ZZ_ERP.Infra.Data.Identity
{
    public class AccountManager
    {
        private readonly UserManager<UserAccount> _userManager;
        private readonly SignInManager<UserAccount> _signInManager;
        private readonly ZZContext _dbContext;

        public AccountManager(UserManager<UserAccount> userManager, SignInManager<UserAccount> signInManager, ZZContext dbContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _dbContext = dbContext;
        }


        public async Task<bool> CreateAsync(string email, string password, List<string> roles)
        {
            var user = new UserAccount { UserName = email, Email = email };
            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                foreach (var role in roles)
                {
                    await _userManager.AddToRoleAsync(user, role);
                }
                
                return true;
            }

            return false;
        }

        public List<IAccount> ListAll()
        {
            var users = _dbContext.Users;
            return users.Any() ? users.Select(u => (IAccount)u).ToList() : new List<IAccount>();
        }
    }
}
