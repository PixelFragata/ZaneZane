using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
using Microsoft.AspNetCore.Mvc;
using ZZ_ERP.Infra.CrossCutting.Connections.Commons;
using ZZ_ERP.Infra.CrossCutting.DTO.IdentityDTO;
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
        //[Authorize(Roles = "Admin")]
        [Authorize(Policy = "UserManagerRead")]
        [HttpGet]
        public ActionResult<List<UserDto>> GetAll()
        {
            if (ZZApiMain.VerifyUserAuthorize(User.Identity.Name))
            {
                var users = _manager.ListAll();
                var dtos = users.Select(u => new UserDto {Id = u.Id, Email = u.Email, Username = u.UserName}).ToList();
                return new List<UserDto>(dtos);
            }

            return null;
        }

        // GET api/values/5
        [Authorize(Policy = "UserManagerCreate")]
        [HttpPost]
        public async Task<ActionResult<bool>> Create(UserDto dto)
        {
            if (ZZApiMain.VerifyUserAuthorize(User.Identity.Name))
            {
                if (await _manager.CreateAsync(dto.Username, dto.Email, dto.Password, dto.Role))
                {
                    return true;
                }
            }

            return false;
        }

        [HttpPost]
        public async Task<ActionResult<bool>> CreateClient(UserDto dto)
        {
            dto.Role = "Cliente";

            if (await _manager.CreateAsync(dto.Username, dto.Email, dto.Password, dto.Role))
            {
                return true;
            }

            return false;
        }

        [Authorize(Policy = "UserManagerRead")]
        [HttpGet]
        public ActionResult<IEnumerable<UserDto>> GetByUsername(string username)
        {
            if (ZZApiMain.VerifyUserAuthorize(User.Identity.Name))
            {
                var users = _manager.GetUserByUsername(username);
                var dtos = users.Select(u => new UserDto {Id = u.Id, Email = u.Email, Username = u.UserName});
                return new ActionResult<IEnumerable<UserDto>>(dtos);
            }

            return null;
        }

        [Authorize(Policy = "UserManagerRead")]
        [HttpGet]
        public ActionResult<IEnumerable<UserDto>> GetByEmail(string email)
        {
            if (ZZApiMain.VerifyUserAuthorize(User.Identity.Name))
            {
                var users = _manager.GetUserByEmail(email);
                var dtos = users.Select(u => new UserDto {Id = u.Id, Email = u.Email, Username = u.UserName});
                return new ActionResult<IEnumerable<UserDto>>(dtos);
            }

            return null;
        }

        [Authorize(Policy = "UserManagerDelete")]
        [HttpDelete("{id}")]
        public void Delete(string username)
        {
            if (ZZApiMain.VerifyUserAuthorize(User.Identity.Name))
            {
                _manager.DeleteUser(username);
            }
        }
    }
}
 