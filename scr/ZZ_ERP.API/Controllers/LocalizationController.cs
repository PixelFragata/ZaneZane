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

        [Authorize(Policy = "LocalizationRead")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoSiglaDto>>> GetAllStates()
        {
            try
            {
                var myUsername = User.Identity.Name;
                if (ZZApiMain.VerifyUserAuthorize(myUsername))
                {
                    if (ZZApiMain.UsersConnections.TryGetValue(myUsername, out var myConn))
                    {
                        var myId = await myConn.Zz.ApiWriteServer(myUsername, new Command { Cmd = ServerCommands.GetAllStates });

                        var responseCommand = await myConn.Zz.GetApiWaitCommand(myId);

                        if (responseCommand != null && responseCommand.Cmd.Equals(ServerCommands.LogResultOk))
                        {
                            return await SerializerAsync.DeserializeJsonList<TipoSiglaDto>(responseCommand.Json);
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

        [Authorize(Policy = "LocalizationRead")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoSiglaDto>>> GetCityByUf(TipoSiglaDto dto)
        {
            try
            {
                var myUsername = User.Identity.Name;
                if (ZZApiMain.VerifyUserAuthorize(myUsername))
                {
                    if (ZZApiMain.UsersConnections.TryGetValue(myUsername, out var myConn))
                    {
                        var myId = await myConn.Zz.ApiWriteServer(myUsername, new Command { Cmd = ServerCommands.GetCityByUf, Json = await SerializerAsync.SerializeJson(dto) });

                        var responseCommand = await myConn.Zz.GetApiWaitCommand(myId);

                        if (responseCommand != null && responseCommand.Cmd.Equals(ServerCommands.LogResultOk))
                        {
                            return await SerializerAsync.DeserializeJsonList<TipoSiglaDto>(responseCommand.Json);
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

        [Authorize(Policy = "LocalizationRead")]
        [HttpGet]
        public async Task<ActionResult<EnderecoDto>> GetAddressByZipCode(EnderecoDto dto)
        {
            try
            {
                var myUsername = User.Identity.Name;
                if (ZZApiMain.VerifyUserAuthorize(myUsername))
                {
                    if (ZZApiMain.UsersConnections.TryGetValue(myUsername, out var myConn))
                    {
                        var myId = await myConn.Zz.ApiWriteServer(myUsername, new Command { Cmd = ServerCommands.GetAddressByZipCode, Json = await SerializerAsync.SerializeJson(dto) });

                        var responseCommand = await myConn.Zz.GetApiWaitCommand(myId);

                        if (responseCommand != null && responseCommand.Cmd.Equals(ServerCommands.LogResultOk))
                        {
                            return await SerializerAsync.DeserializeJson<EnderecoDto>(responseCommand.Json);
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

        [Authorize(Policy = "LocalizationRead")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EnderecoDto>>> GetAddress(EnderecoDto dto)
        {
            try
            {
                var myUsername = User.Identity.Name;
                if (ZZApiMain.VerifyUserAuthorize(myUsername))
                {
                    if (ZZApiMain.UsersConnections.TryGetValue(myUsername, out var myConn))
                    {
                        var myId = await myConn.Zz.ApiWriteServer(myUsername, new Command { Cmd = ServerCommands.GetAddress, Json = await SerializerAsync.SerializeJson(dto) });

                        var responseCommand = await myConn.Zz.GetApiWaitCommand(myId);

                        if (responseCommand != null && responseCommand.Cmd.Equals(ServerCommands.LogResultOk))
                        {
                            return await SerializerAsync.DeserializeJsonList<EnderecoDto>(responseCommand.Json);
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

        [Authorize(Policy = "LocalizationUpdate")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoSiglaDto>>> UpdateStates()
        {
            try
            {
                var myUsername = User.Identity.Name;
                if (ZZApiMain.VerifyUserAuthorize(myUsername))
                {
                    if (ZZApiMain.UsersConnections.TryGetValue(myUsername, out var myConn))
                    {
                        var myId = await myConn.Zz.ApiWriteServer(myUsername, new Command { Cmd = ServerCommands.UpdateStates });

                        var responseCommand = await myConn.Zz.GetApiWaitCommand(myId);

                        if (responseCommand != null && responseCommand.Cmd.Equals(ServerCommands.LogResultOk))
                        {
                            return await SerializerAsync.DeserializeJsonList<TipoSiglaDto>(responseCommand.Json);
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

        [Authorize(Policy = "LocalizationUpdate")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoSiglaDto>>> UpdateCities()
        {
            try
            {
                var myUsername = User.Identity.Name;
                if (ZZApiMain.VerifyUserAuthorize(myUsername))
                {
                    if (ZZApiMain.UsersConnections.TryGetValue(myUsername, out var myConn))
                    {
                        var myId = await myConn.Zz.ApiWriteServer(myUsername, new Command { Cmd = ServerCommands.UpdateCities });

                        var responseCommand = await myConn.Zz.GetApiWaitCommand(myId);

                        if (responseCommand != null && responseCommand.Cmd.Equals(ServerCommands.LogResultOk))
                        {
                            return await SerializerAsync.DeserializeJsonList<TipoSiglaDto>(responseCommand.Json);
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

        [Authorize(Policy = "LocalizationCreate")]
        [HttpPost]
        public async Task<ActionResult<long>> SaveAddress(EnderecoDto dto)
        {
            try
            {
                var myUsername = User.Identity.Name;

                if (ZZApiMain.VerifyUserAuthorize(myUsername))
                {
                    if (ZZApiMain.UsersConnections.TryGetValue(myUsername, out var myConn))
                    {
                        var myId = await myConn.Zz.ApiWriteServer(myUsername, new Command {Cmd = ServerCommands.SaveAddress, Json = await SerializerAsync.SerializeJson(dto) });

                        var responseCommand = await myConn.Zz.GetApiWaitCommand(myId);

                        if (responseCommand != null && responseCommand.Cmd.Equals(ServerCommands.LogResultOk))
                        {
                            return responseCommand.EntityId;
                        }
                    }
                }
                return -1;
            }
            catch (Exception e)
            {
                ConsoleEx.WriteError(e);
                return NotFound();
            }
            
        }

        [Authorize(Policy = "LocalizationUpdate")]
        [HttpPost]
        public async Task<ActionResult<bool>> EditAddress(EnderecoDto dto)
        {
            try
            {
                var myUsername = User.Identity.Name;

                if (ZZApiMain.VerifyUserAuthorize(myUsername))
                {
                    if (ZZApiMain.UsersConnections.TryGetValue(myUsername, out var myConn))
                    {
                        var myId = await myConn.Zz.ApiWriteServer(myUsername, new Command {Cmd = ServerCommands.EditAddress, EntityId = dto.Id, Json = await SerializerAsync.SerializeJson(dto) });

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

        [Authorize(Policy = "LocalizationDelete")]
        [HttpDelete]
        public async Task<ActionResult<bool>> Delete(ServicoDto dto)
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
 