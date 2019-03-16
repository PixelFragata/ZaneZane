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
    public class UnidadeMedidaController : ControllerBase
    {
        public static string Tela = ServerCommands.UnidadeMedida;

        [Authorize(Policy = "UnidadeMedidaRead")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UnidadeMedidaDto>>> GetAll()
        {
            var myUsername = User.Identity.Name;
            if (ZZApiMain.VerifyUserAuthorize(myUsername))
            {
                if (ZZApiMain.UsersConnections.TryGetValue(myUsername, out var myConn))
                {
                    var myId = await myConn.Zz.ApiWriteServer(myUsername, new Command{Tela = Tela, Cmd = ServerCommands.GetAll});

                   var responseCommand = await myConn.Zz.GetApiWaitCommand(myId);

                    if (responseCommand != null && responseCommand.Cmd.Equals(ServerCommands.LogResultOk))
                    {
                        return await SerializerAsync.DeserializeJsonList<UnidadeMedidaDto>(responseCommand.Json);
                    }
                }
            }
            return NotFound();
        }

        [Authorize(Policy = "UnidadeMedidaCreate")]
        [HttpPost]
        public async Task<ActionResult<bool>> Create(UnidadeMedidaDto dto)
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

        [Authorize(Policy = "UnidadeMedidaUpdate")]
        [HttpPost]
        public async Task<ActionResult<bool>> Edit(UnidadeMedidaDto dto)
        {
            var myUsername = User.Identity.Name;

            if (ZZApiMain.VerifyUserAuthorize(myUsername))
            {
                if (ZZApiMain.UsersConnections.TryGetValue(myUsername, out var myConn))
                {
                    var myId = await myConn.Zz.ApiWriteServer(myUsername, new Command {Tela = Tela, Cmd = ServerCommands.Edit, EntityId = dto.Id, Json = await SerializerAsync.SerializeJson(dto) });

                    var responseCommand = await myConn.Zz.GetApiWaitCommand(myId);

                    if (responseCommand != null && responseCommand.Cmd.Equals(ServerCommands.LogResultOk))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        [Authorize(Policy = "UnidadeMedidaDelete")]
        [HttpDelete]
        public async Task<ActionResult<bool>> Delete(UnidadeMedidaDto dto)
        {
            var myUsername = User.Identity.Name;

            if (ZZApiMain.VerifyUserAuthorize(myUsername))
            {
                if (ZZApiMain.UsersConnections.TryGetValue(myUsername, out var myConn))
                {
                    var myId = await myConn.Zz.ApiWriteServer(myUsername, new Command {Tela = Tela, Cmd = ServerCommands.Disable, EntityId = dto.Id, Json = await SerializerAsync.SerializeJson(dto) });

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
 