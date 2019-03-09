using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using ZZ_ERP.Infra.CrossCutting.Connections.Functions;
using ZZ_ERP.Infra.CrossCutting.DTO.EntitiesDTO;
using ZZ_ERP.Infra.CrossCutting.DTO.Interfaces;

namespace ZZ_ERP.Infra.Data.Identity
{
    public class Authentication : IAuthentication
    {
        private readonly UserManager<UserAccount> _userManager;
        private readonly SignInManager<UserAccount> _signInManager;
        private readonly TokenConfigurations _tokenConfigurations;
        private readonly SigningConfigurations _signingConfigurations;

        public Authentication(UserManager<UserAccount> userManager, SignInManager<UserAccount> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenConfigurations = TokenConfigurations.Instance;
            _signingConfigurations = SigningConfigurations.Instance;
        }

        public async Task<LoginResultDto> Authenticate(string username, string password)
        {
            LoginResultDto loginResult = new LoginResultDto();
            loginResult.Authenticated = false;
            loginResult.Message = "Falha ao autenticar";
            try
            {     
                if (!string.IsNullOrWhiteSpace(username))
                {
                    var userIdentity = await _userManager.FindByNameAsync(username);
                    if (userIdentity != null)
                    {
                        
                        var result = await _signInManager.CheckPasswordSignInAsync(userIdentity, password, false);
                        if (result.Succeeded)
                        {
                            loginResult = CreateToken(username);
                            loginResult.UserId = userIdentity.Id;
                        }
                    }
                    
                }

                return loginResult;
            }
            catch (Exception e)
            {
                ConsoleEx.WriteError(e);
                return loginResult;
            }
        }

        private  LoginResultDto CreateToken(string username)
        {
            try
            {
                LoginResultDto loginResult = new LoginResultDto();
                loginResult.Authenticated = true;
                ClaimsIdentity identity = new ClaimsIdentity(
                    new GenericIdentity(username, "Login"),
                    new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Jti,        Guid.NewGuid().ToString("N")),
                        new Claim(JwtRegisteredClaimNames.UniqueName, username),
                    });

                loginResult.CreatedDate = DateTime.Now;
                loginResult.ExpirationDate =
                    loginResult.CreatedDate + TimeSpan.FromSeconds(_tokenConfigurations.Seconds);

                var handler = new JwtSecurityTokenHandler();
                var securityToken = handler.CreateToken(new SecurityTokenDescriptor
                {
                    Issuer = _tokenConfigurations.Issuer,
                    Audience = _tokenConfigurations.Audience,
                    SigningCredentials = _signingConfigurations.SigningCredentials,
                    Subject = identity,
                    NotBefore = loginResult.CreatedDate,
                    Expires = loginResult.ExpirationDate
                });

                loginResult.AccessToken = handler.WriteToken(securityToken);
                loginResult.Message = "Ok";

                return loginResult;
            }
            catch (Exception e)
            {
                ConsoleEx.WriteError(e);
                throw;
            }
            
        }

        /*public async Task<string> Authenticate(string username, string password)
        {
            try
            {
                var result = await _signInManager.PasswordSignInAsync(username, password, false, false);
                if (result.Succeeded)
                {
                    return _signInManager.UserManager.Users.First(u => u.UserName.Equals(username)).Id;
                }

                return null;
            }
            catch (Exception e)
            {
                ConsoleEx.WriteError(e);
                return null;
            }
        }*/

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
