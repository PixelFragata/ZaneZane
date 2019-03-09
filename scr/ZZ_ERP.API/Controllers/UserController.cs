using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
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
    public class UserController : ControllerBase
    {
        private readonly IAccountManager _manager;

        public UserController(IAccountManager manager)
        {
            _manager = manager;
        }

        // GET api/values
        //[Authorize("Bearer")]
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult<IEnumerable<UserDto>> GetAllUsers()
        {
            var users = _manager.ListAll();
            var dtos = users.Select(u => new UserDto {Id = u.Id, Email = u.Email, Username = u.UserName});
            return new ActionResult<IEnumerable<UserDto>>(dtos);
        }

        // GET api/values/5
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<bool>> Create(UserDto dto)
        {
            if (await _manager.CreateAsync(dto.Username, dto.Email, dto.Password, dto.Role))
            {
                return true;
            }

            return false;
        }

        [HttpPost]
        public async Task<ActionResult<bool>> CreateClient(UserDto dto)
        {
            dto.Role =Roles.RoleCliente;

            if (await _manager.CreateAsync(dto.Username, dto.Email, dto.Password, dto.Role))
            {
                return true;
            }

            return false;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{username}")]
        public ActionResult<IEnumerable<UserDto>> GetUserByUsername(string username)
        {
            var users = _manager.GetUserByUsername(username);
            var dtos = users.Select(u => new UserDto { Id = u.Id, Email = u.Email, Username = u.UserName });
            return new ActionResult<IEnumerable<UserDto>>(dtos);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult<IEnumerable<UserDto>> GetUserByEmail(string email)
        {
            var users = _manager.GetUserByEmail(email);
            var dtos = users.Select(u => new UserDto { Id = u.Id, Email = u.Email, Username = u.UserName });
            return new ActionResult<IEnumerable<UserDto>>(dtos);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            _manager.DeleteUser(id);
        }
    }
}
 