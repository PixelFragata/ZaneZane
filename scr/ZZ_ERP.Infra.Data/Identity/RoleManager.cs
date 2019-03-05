using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Internal;
using ZZ_ERP.Infra.CrossCutting.Connections.Functions;
using ZZ_ERP.Infra.CrossCutting.DTO.Interfaces;
using ZZ_ERP.Infra.Data.Contexts;
using System.Linq;

namespace ZZ_ERP.Infra.Data.Identity
{
    public class RoleManager : IRoleManager
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ZZContext _dbContext;

        public RoleManager(RoleManager<IdentityRole> roleManager, ZZContext dbContext)
        {
            _roleManager = roleManager;
            _dbContext = dbContext;
        }

        public async Task<bool> CreateAsync(string roleName)
        {
            try
            {
                if (!await _roleManager.RoleExistsAsync(roleName))
                {
                    var role = new IdentityRole(roleName);
                    await _roleManager.CreateAsync(role);
                    await _dbContext.SaveChangesAsync();
                }

                return true;
            }
            catch (Exception e)
            {
                ConsoleEx.WriteError(e);
                return false;
            }
        }

        public List<IdentityRole> ListAll()
        {
            var roles = _dbContext.Roles;
            return roles.Any() ? roles.ToList() : new List<IdentityRole>();
        }
    }
}
