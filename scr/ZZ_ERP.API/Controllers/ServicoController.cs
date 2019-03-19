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
    public class ServicoController : ControllerBase
    {
        public static string Tela = ServerCommands.Servico;

        [Authorize(Policy = "ServicoRead")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServicoDto>>> GetAll()
        {
            var myUsername = User.Identity.Name;
            if (ZZApiMain.VerifyUserAuthorize(myUsername))
            {
                if (ZZApiMain.UsersConnections.TryGetValue(myUsername, out var myConn))
                {
                    var myId = await myConn.Zz.ApiWriteServer(myUsername, new Command{Cmd = ServerCommands.GetAll, Tela = Tela});

                    var responseCommand = await myConn.Zz.GetApiWaitCommand(myId);

                    if (responseCommand != null && responseCommand.Cmd.Equals(ServerCommands.LogResultOk))
                    {
                        return await SerializerAsync.DeserializeJsonList<ServicoDto>(responseCommand.Json);
                    }
                }
            }
            return NotFound();
        }

        [Authorize(Policy = "ServicoCreate")]
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

        [Authorize(Policy = "ServicoUpdate")]
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

        [Authorize(Policy = "ServicoDelete")]
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
 