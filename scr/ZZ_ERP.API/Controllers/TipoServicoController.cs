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


        [Authorize(Policy = "TipoServicoRead")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> GetAll()
        {
            var myUsername = User.Identity.Name;
            if (ZZApiMain.VerifyUserAuthorize(myUsername))
            {
                if (ZZApiMain.UsersConnections.TryGetValue(myUsername, out var myConn))
                {
                    var myId = await myConn.Zz.ApiWriteServer(myUsername, new Command{Cmd = ServerCommands.GetAllTiposServico});

                   var responseCommand = await myConn.Zz.GetApiWaitCommand(myId);


                    if (responseCommand != null && responseCommand.Cmd.Equals(ServerCommands.LogResultOk))
                    {
                        return await SerializerAsync.DeserializeJsonList<string>(responseCommand.Json);
                    }
                }
            }
            return NotFound();
        }

        [Authorize(Policy = "TipoServicoCreate")]
        [HttpPost]
        public async Task<ActionResult<bool>> Create(string tipoServicoDescricao)
        {
            var myUsername = User.Identity.Name;
            Command responseCommand = null;

            if (ZZApiMain.VerifyUserAuthorize(myUsername))
            {
                if (ZZApiMain.UsersConnections.TryGetValue(myUsername, out var myConn))
                {
                    var myId = await myConn.Zz.ApiWriteServer(myUsername, new Command { Cmd = ServerCommands.AddTipoServico, Json = await SerializerAsync.SerializeJson(tipoServicoDescricao)});

                    responseCommand = await myConn.Zz.GetApiWaitCommand(myId);

                    if (responseCommand != null && responseCommand.Cmd.Equals(ServerCommands.LogResultOk))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        [Authorize(Policy = "TipoServicoUpdate")]
        [HttpPost]
        public async Task<ActionResult<bool>> Edit(TipoServicoDto dto)
        {
            var myUsername = User.Identity.Name;
            Command responseCommand = null;

            if (ZZApiMain.VerifyUserAuthorize(myUsername))
            {
                if (ZZApiMain.UsersConnections.TryGetValue(myUsername, out var myConn))
                {
                    var myId = await myConn.Zz.ApiWriteServer(myUsername, new Command { Cmd = ServerCommands.EditTipoServico, Json = await SerializerAsync.SerializeJson(dto) });

                    responseCommand = await myConn.Zz.GetApiWaitCommand(myId);

                    if (responseCommand != null && responseCommand.Cmd.Equals(ServerCommands.LogResultOk))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        [Authorize(Policy = "TipoServicoDelete")]
        [HttpDelete]
        public async Task<ActionResult<bool>> Delete(string tipoServicoDescricao)
        {
            var myUsername = User.Identity.Name;
            Command responseCommand = null;

            if (ZZApiMain.VerifyUserAuthorize(myUsername))
            {
                if (ZZApiMain.UsersConnections.TryGetValue(myUsername, out var myConn))
                {
                    var myId = await myConn.Zz.ApiWriteServer(myUsername, new Command { Cmd = ServerCommands.DeleteTipoServico, Json = await SerializerAsync.SerializeJson(tipoServicoDescricao) });

                    responseCommand = await myConn.Zz.GetApiWaitCommand(myId);

                    if (responseCommand != null && responseCommand.Cmd.Equals(ServerCommands.LogResultOk))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

    }
}
 