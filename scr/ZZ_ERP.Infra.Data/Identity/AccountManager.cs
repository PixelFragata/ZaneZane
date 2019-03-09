using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ZZ_ERP.Infra.CrossCutting.Connections.Commons;
using ZZ_ERP.Infra.CrossCutting.DTO.Interfaces;
using ZZ_ERP.Infra.Data.Contexts;

namespace ZZ_ERP.Infra.Data.Identity
{
    public class AccountManager : IAccountManager
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


        public async Task<bool> CreateAsync(string username, string email, string password, string role = Roles.RoleCliente)
        {
                if (string.IsNullOrEmpty(username))
            {
                username = email;
            }
            var user = new UserAccount { UserName = username, Email = email };
            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, role);
                await _dbContext.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public List<IAccount> GetUserByUsername(string username)
        {
            var users = _dbContext.Users;
            var userList = new List<IAccount>(users.Where(u => u.UserName.Contains(username)).ToList());
            return userList;
        }

        public List<IAccount> GetUserByEmail(string email)
        {
            var users = _dbContext.Users;
            var userList = new List<IAccount>(users.Where(u => u.Email.Equals(email)).ToList());
            return userList;
        }

        public List<IAccount> ListAll()
        {
            var users = _dbContext.Users;
            return users.Any() ? users.Select(u => (IAccount)u).ToList() : new List<IAccount>();
        }

        public bool DeleteUser(string id)
        {
            var users = _dbContext.Users;
            var user = users.FirstOrDefault(u => u.Id.Equals(id));
            return true;
        }


    }
}
