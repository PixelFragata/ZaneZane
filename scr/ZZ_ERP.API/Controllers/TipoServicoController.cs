using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
using Microsoft.AspNetCore.Mvc;
using ZZ_ERP.Infra.CrossCutting.Connections.Commons;
using ZZ_ERP.Infra.CrossCutting.Connections.Functions;
using ZZ_ERP.Infra.CrossCutting.DTO.EntitiesDTO;
using ZZ_ERP.Infra.CrossCutting.DTO.Interfaces;

namespace ZZ_ERP.API.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    [Authorize("Bearer")]
    public class TipoServicoController : ControllerBase
    {

        // GET api/values
        [Authorize(Policy = "TipoServicoRead")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> GetAllTiposServicos()
        {
            var myUsername = User.Identity.Name;
            Command responseCommand = null;
            int timeCount = 0;
            if (ZZApiMain.VerifyUserAuthorize(myUsername))
            {
                if (ZZApiMain.UsersConnections.TryGetValue(myUsername, out var myConn))
                {
                    var myId = await myConn.Zz.ApiWriteServer(myUsername, new Command{Cmd = ServerCommands.GetAllTiposServiço});

                   responseCommand = await myConn.Zz.GetApiWaitCommand(myId);


                    if (responseCommand != null && responseCommand.Cmd.Equals(ServerCommands.LogResultOk))
                    {
                        return await SerializerAsync.DeserializeJsonList<string>(responseCommand.Json);
                    }
                }
                
            }

            return NotFound();
        }

    }
}
 