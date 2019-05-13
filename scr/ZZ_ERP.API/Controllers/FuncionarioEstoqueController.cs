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
    public class FuncionarioEstoqueController : ControllerBase
    {
        public static string Tela = ServerCommands.FuncionarioEstoque;

        [Authorize(Policy = "FuncionarioEstoqueRead")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DtoLigacao>>> GetAll()
        {
            try
            {
                var myUsername = User.Identity.Name;
                if (ZZApiMain.VerifyUserAuthorize(myUsername))
                {
                    if (ZZApiMain.UsersConnections.TryGetValue(myUsername, out var myConn))
                    {
                        var myId = await myConn.Zz.ApiWriteServer(myUsername, new Command { Cmd = ServerCommands.GetAll, Tela = Tela });

                        var responseCommand = await myConn.Zz.GetApiWaitCommand(myId);

                        if (responseCommand != null && responseCommand.Cmd.Equals(ServerCommands.LogResultOk))
                        {
                            return await SerializerAsync.DeserializeJsonList<DtoLigacao>(responseCommand.Json);
                        }
                    }
                }
                return NotFound();
            }
            catch (Exception e)
            {
                ConsoleEx.WriteError(e);
                return NotFound();
            }
        }

        [Authorize(Policy = "FuncionarioEstoqueRead")]
        [HttpGet]
        public async Task<ActionResult<DtoLigacao>> GetById(DtoLigacao dto)
        {
            try
            {
                var myUsername = User.Identity.Name;
                if (ZZApiMain.VerifyUserAuthorize(myUsername))
                {
                    if (ZZApiMain.UsersConnections.TryGetValue(myUsername, out var myConn))
                    {
                        var myId = await myConn.Zz.ApiWriteServer(myUsername, new Command { Cmd = ServerCommands.GetById, EntityId = dto.Id, Tela = Tela, Json = await SerializerAsync.SerializeJson(dto) });

                        var responseCommand = await myConn.Zz.GetApiWaitCommand(myId);

                        if (responseCommand != null && responseCommand.Cmd.Equals(ServerCommands.LogResultOk))
                        {
                            return await SerializerAsync.DeserializeJson<DtoLigacao>(responseCommand.Json);
                        }
                    }
                }
                return NotFound();
            }
            catch (Exception e)
            {
                ConsoleEx.WriteError(e);
                return NotFound();
            }
        }

        [Authorize(Policy = "FuncionarioEstoqueRead")]
        [HttpGet]
        public async Task<ActionResult<DtoLigacao>> GetByHumanCode(DtoLigacao dto)
        {
            try
            {
                var myUsername = User.Identity.Name;
                if (ZZApiMain.VerifyUserAuthorize(myUsername))
                {
                    if (ZZApiMain.UsersConnections.TryGetValue(myUsername, out var myConn))
                    {
                        var myId = await myConn.Zz.ApiWriteServer(myUsername, new Command { Cmd = ServerCommands.GetById, EntityId = dto.Id, Tela = Tela, Json = await SerializerAsync.SerializeJson(dto) });

                        var responseCommand = await myConn.Zz.GetApiWaitCommand(myId);

                        if (responseCommand != null && responseCommand.Cmd.Equals(ServerCommands.LogResultOk))
                        {
                            return await SerializerAsync.DeserializeJson<DtoLigacao>(responseCommand.Json);
                        }
                    }
                }
                return NotFound();
            }
            catch (Exception e)
            {
                ConsoleEx.WriteError(e);
                return NotFound();
            }

        }

        [Authorize(Policy = "FuncionarioEstoqueCreate")]
        [HttpPost]
        public async Task<ActionResult<bool>> Create(DtoLigacao dto)
        {
            try
            {
                var myUsername = User.Identity.Name;

                if (ZZApiMain.VerifyUserAuthorize(myUsername))
                {
                    if (ZZApiMain.UsersConnections.TryGetValue(myUsername, out var myConn))
                    {
                        var myId = await myConn.Zz.ApiWriteServer(myUsername, new Command { Tela = Tela, Cmd = ServerCommands.Add, Json = await SerializerAsync.SerializeJson(dto) });

                        var responseCommand = await myConn.Zz.GetApiWaitCommand(myId);

                        if (responseCommand != null && responseCommand.Cmd.Equals(ServerCommands.LogResultOk))
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
            catch (Exception e)
            {
                ConsoleEx.WriteError(e);
                return NotFound();
            }
            
        }

        [Authorize(Policy = "FuncionarioEstoqueUpdate")]
        [HttpPost]
        public async Task<ActionResult<bool>> Edit(DtoLigacao dto)
        {
            try
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
            catch (Exception e)
            {
                ConsoleEx.WriteError(e);
                return NotFound();
            }
            
        }

        [Authorize(Policy = "FuncionarioEstoqueDelete")]
        [HttpDelete]
        public async Task<ActionResult<bool>> Delete(DtoLigacao dto)
        {
            try
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
            catch (Exception e)
            {
                ConsoleEx.WriteError(e);
                return NotFound();
            }
            
        }

    }
}
 