using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Timers;
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
    public class AccountController : ControllerBase
    {
        private readonly IAuthentication _authentication;
        private Timer _userTimer;

        public AccountController(IAuthentication authentication)
        {
            _authentication = authentication;

        }


        [HttpPost]
        public async Task<ActionResult<LoginResultDto>> Login(LoginDto dto)
        {
            var resultDto = await _authentication.Authenticate(dto.Username, dto.Password);

            await ZZApiMain.AddUserConnection(resultDto.UserId);
            return resultDto;
        }

        public async Task<ActionResult<bool>> Logout()
        {
            if (await _authentication.Logout())
            {
                return true;
            }

            return false;
        }

        public async Task<ActionResult<LoginResultDto>> LoginTeste()
        {
            var dto = new LoginDto{Password = "admin", Username = "admin"};
            var resultDto = await _authentication.Authenticate(dto.Username, dto.Password);

            await ZZApiMain.AddUserConnection(resultDto.UserId);
            return resultDto;
        }  
    }
}
 