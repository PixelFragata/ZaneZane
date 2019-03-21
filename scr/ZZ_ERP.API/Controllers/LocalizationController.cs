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
    public class LocalizationController : ControllerBase
    {
        public static string Tela = ServerCommands.Localization;

        [Authorize(Policy = "LocalizationUpdate")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoSiglaDto>>> UpdateEstados()
        {
            var myUsername = User.Identity.Name;
            if (ZZApiMain.VerifyUserAuthorize(myUsername))
            {
                if (ZZApiMain.UsersConnections.TryGetValue(myUsername, out var myConn))
                {
                    var myId = await myConn.Zz.ApiWriteServer(myUsername, new Command{Cmd = ServerCommands.UpdateEstados});

                    var responseCommand = await myConn.Zz.GetApiWaitCommand(myId);

                    if (responseCommand != null && responseCommand.Cmd.Equals(ServerCommands.LogResultOk))
                    {
                        return await SerializerAsync.DeserializeJsonList<TipoSiglaDto>(responseCommand.Json);
                    }
                }
            }
            return NotFound();
        }

        [Authorize(Policy = "LocalizationUpdate")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoSiglaDto>>> UpdateCidades()
        {
            var myUsername = User.Identity.Name;
            if (ZZApiMain.VerifyUserAuthorize(myUsername))
            {
                if (ZZApiMain.UsersConnections.TryGetValue(myUsername, out var myConn))
                {
                    var myId = await myConn.Zz.ApiWriteServer(myUsername, new Command { Cmd = ServerCommands.UpdateCidades });

                    var responseCommand = await myConn.Zz.GetApiWaitCommand(myId);

                    if (responseCommand != null && responseCommand.Cmd.Equals(ServerCommands.LogResultOk))
                    {
                        return await SerializerAsync.DeserializeJsonList<TipoSiglaDto>(responseCommand.Json);
                    }
                }
            }
            return NotFound();
        }

        [Authorize(Policy = "LocalizationCreate")]
        [HttpPost]
        public async Task<ActionResult<bool>> Create(ServicoDto dto)
        {
            var myUsername = User.Identity.Name;

            if (ZZApiMain.VerifyUserAuthorize(myUsername))
            {
                if (ZZApiMain.UsersConnections.TryGetValue(myUsername, out var myConn))
                {
                    var myId = await myConn.Zz.ApiWriteServer(myUsername, new Command {Tela = Tela, Cmd = ServerCommands.Add, Json = await SerializerAsync.SerializeJson(dto)});

                    var responseCommand = await myConn.Zz.GetApiWaitCommand(myId);

                    if (responseCommand != null && responseCommand.Cmd.Equals(ServerCommands.LogResultOk))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        [Authorize(Policy = "LocalizationUpdate")]
        [HttpPost]
        public async Task<ActionResult<bool>> Edit(ServicoDto dto)
        {
            var myUsername = User.Identity.Name;

            if (ZZApiMain.VerifyUserAuthorize(myUsername))
            {
                if (ZZApiMain.UsersConnections.TryGetValue(myUsername, out var myConn))
                {
                    var myId = await myConn.Zz.ApiWriteServer(myUsername, new Command { Tela = Tela, Cmd = ServerCommands.Edit, EntityId = dto.Id, Json = await SerializerAsync.SerializeJson(dto) });

                    var responseCommand = await myConn.Zz.GetApiWaitCommand(myId);

                    if (responseCommand != null && responseCommand.Cmd.Equals(ServerCommands.LogResultOk))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        [Authorize(Policy = "LocalizationDelete")]
        [HttpDelete]
        public async Task<ActionResult<bool>> Delete(ServicoDto dto)
        {
            var myUsername = User.Identity.Name;

            if (ZZApiMain.VerifyUserAuthorize(myUsername))
            {
                if (ZZApiMain.UsersConnections.TryGetValue(myUsername, out var myConn))
                {
                    var myId = await myConn.Zz.ApiWriteServer(myUsername, new Command { Tela = Tela, Cmd = ServerCommands.Disable, EntityId = dto.Id, Json = await SerializerAsync.SerializeJson(dto) });

                    var responseCommand = await myConn.Zz.GetApiWaitCommand(myId);

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
 