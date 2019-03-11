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
using ZZ_ERP.Infra.CrossCutting.DTO.EntitiesDTO;
using ZZ_ERP.Infra.CrossCutting.DTO.Interfaces;

namespace ZZ_ERP.API.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    [Authorize("Bearer")]
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
        public ActionResult<IEnumerable<string>> GetAllUsers()
        {
            var users = _roleManager.ListAll();
            var dtos = users.Select(u => u.Name);
            return new ActionResult<IEnumerable<string>>(dtos);
        }

        // GET api/values/5
        [Authorize(Policy = "RoleManagerCreate")]
        [HttpPost("{roleName}")]
        public async Task<ActionResult<bool>> Create(string roleName)
        {
            return await _roleManager.CreateAsync(roleName);
        }

        [Authorize(Policy = "RoleManagerCreate")]
        [HttpPost]
        public async Task<ActionResult<bool>> CreateClaim(string roleName, string nomeTela, string tipoPermissao)
        {
            return await _roleManager.AddRoleClaim(roleName, nomeTela, tipoPermissao);
        }

        [Authorize(Policy = "RoleManagerRead")]
        [HttpGet]
        public async Task<ActionResult<MatrizRolePermission>> GetRoleClaims(string roleName)
        {
            return await _roleManager.GetRolePermissions(roleName);
        }
    }
}
 