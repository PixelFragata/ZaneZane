using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using ZZ_ERP.Infra.CrossCutting.Connections.Functions;
using ZZ_ERP.Infra.CrossCutting.DTO.Interfaces;

namespace ZZ_ERP.Infra.Data.Identity
{
    public class Authentication : IAuthentication
    {
        private readonly SignInManager<UserAccount> _signInManager;

        public Authentication(SignInManager<UserAccount> signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task<bool> Authenticate(string username, string password)
        {
            try
            {
                var result = await _signInManager.PasswordSignInAsync(username, password, false, false);
                return result.Succeeded;
            }
            catch (Exception e)
            {
                ConsoleEx.WriteError(e);
                return false;
            }
        }

        public async Task<bool> Logout()
        {
            try
            {
                await _signInManager.SignOutAsync();
                return true;
            }
            catch (Exception e)
            {
                ConsoleEx.WriteError(e);
                return false;
            }
        }
    }
}
