using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
using Microsoft.AspNetCore.Mvc;
using ZZ_ERP.Infra.CrossCutting.Connections.Commons;
using ZZ_ERP.Infra.CrossCutting.Connections.Functions;
using ZZ_ERP.Infra.CrossCutting.DTO.IdentityDTO;
using ZZ_ERP.Infra.CrossCutting.DTO.Interfaces;

namespace ZZ_ERP.API.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    //[Authorize("Bearer")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleManager _roleManager;

        public RoleController(IRoleManager roleManager)
        {
            _roleManager = roleManager;
        }

        // GET api/values
        [Authorize(Policy = "RoleManagerRead")]
        [HttpGet]
        public ActionResult<IEnumerable<string>> GetAll()
        {
            try
            {
                if (ZZApiMain.VerifyUserAuthorize(User.Identity.Name))
                {
                    var users = _roleManager.ListAll();
                    var dtos = users.Select(u => u.Name);
                    return new ActionResult<IEnumerable<string>>(dtos);
                }

                return null;
            }
            catch (Exception e)
            {
                ConsoleEx.WriteError(e);
                return NotFound();
            }
            
        }

        // GET api/values/5
        [Authorize(Policy = "RoleManagerCreate")]
        [HttpPost("{roleName}")]
        public async Task<ActionResult<bool>> Create(string roleName)
        {
            try
            {
                if (ZZApiMain.VerifyUserAuthorize(User.Identity.Name))
                {
                    return await _roleManager.CreateAsync(roleName);
                }

                return false;
            }
            catch (Exception e)
            {
                ConsoleEx.WriteError(e);
                return NotFound();
            }
        }

        [Authorize(Policy = "RoleManagerCreate")]
        [HttpPost]
        public async Task<ActionResult<bool>> CreateClaim(string roleName, string nomeTela, string tipoPermissao)
        {
            try
            {
                if (ZZApiMain.VerifyUserAuthorize(User.Identity.Name))
                {
                    return await _roleManager.AddRoleClaim(roleName, nomeTela, tipoPermissao);
                }

                return false;
            }
            catch (Exception e)
            {
                ConsoleEx.WriteError(e);
                return NotFound();
            }
            
        }

        [Authorize(Policy = "RoleManagerCreate")]
        [HttpPost]
        public async Task<ActionResult<bool>> CreateScreenClaims(string roleName, string nomeTela)
        {
            try
            {
                if (ZZApiMain.VerifyUserAuthorize(User.Identity.Name))
                {
                    var success = await _roleManager.AddRoleClaim(roleName, nomeTela, ServerCommands.Create);
                    success = success && await _roleManager.AddRoleClaim(roleName, nomeTela, ServerCommands.Read);
                    success = success && await _roleManager.AddRoleClaim(roleName, nomeTela, ServerCommands.Update);
                    success = success && await _roleManager.AddRoleClaim(roleName, nomeTela, ServerCommands.Delete);
                    return success;
                }

                return false;
            }
            catch (Exception e)
            {
                ConsoleEx.WriteError(e);
                return NotFound();
            }
            
        }

        [Authorize(Policy = "RoleManagerRead")]
        [HttpGet]
        public async Task<ActionResult<MatrizRolePermission>> GetRoleClaims(string roleName)
        {
            try
            {
                if (ZZApiMain.VerifyUserAuthorize(User.Identity.Name))
                {
                    return await _roleManager.GetRolePermissions(roleName);
                }

                return null;
            }
            catch (Exception e)
            {
                ConsoleEx.WriteError(e);
                return NotFound();
            } 
            
        }

        [HttpGet]
        public async Task<ActionResult<bool>> CreateAdminRole()
        {
            try
            {
                await _roleManager.CreateAsync("Admin");
                await _roleManager.InitializeAdminRole();
                return true;

            }
            catch (Exception e)
            {
                ConsoleEx.WriteError(e);
                return NotFound();
            }
        }

    }
}
 