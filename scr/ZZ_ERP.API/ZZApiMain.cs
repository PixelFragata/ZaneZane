using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ZZ_ERP.API.Connections;
using ZZ_ERP.Infra.CrossCutting.Connections.Commons;
using ZZ_ERP.Infra.CrossCutting.Connections.Connections;
using ZZ_ERP.Infra.CrossCutting.Connections.Functions;
using ZZ_ERP.Infra.CrossCutting.DTO.EntitiesDTO;
using ZZ_ERP.Infra.CrossCutting.DTO.Interfaces;
using Timer = System.Timers.Timer;

namespace ZZ_ERP.API
{
    public class ZZApiMain
    {
        private static Thread _dataThread;
        public static ServerConnection Zz;
        public static long Id;
        public static string Error;
        public static Dictionary<string,IConnectionManager> UsersConnections { get; private set; } 

        public static void Main(string[] args)
        {

            try
            {
                UsersConnections = new Dictionary<string, IConnectionManager>();
                Zz = new ServerConnection(AdressPool.ZZ_EF_APK.Ip, AdressPool.ZZ_EF_APK.Port);
                DelegateAction del = new DelegateAction();
                del.act = ReturnServer;

                Zz.PutDelegate(del);
                Thread.Sleep(1000);
                Zz.WriteServer(new Command {Cmd = ServerCommands.IsController});
            }
            catch (Exception e)
            {
                Error = e.Message;
                Console.WriteLine(e);
            }
            finally
            {
                CreateWebHostBuilder(args).Build().Run();
            }

            
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();


        public static void ReturnServer(Object[] server, Object[] local)
        {
            var dataJson = SerializerAsync.DeserializeJson<Command>(server[0].ToString()).Result;
            try
            {
                if (dataJson != null)
                {
                    if (!dataJson.Cmd.Equals(ServerCommands.Exit))
                    {
                        if (dataJson.Cmd.Equals(ServerCommands.LogResultOk))
                        {
                            Id = dataJson.EntityId;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public static bool VerifyUserAuthorize(string username)
        {
            if (UsersConnections.ContainsKey(username))
            {
                return true;
            }

            return false;
        }

        public static async Task AddUserConnection(LoginResultDto loginResult)
        {
            var del = new DelegateAction{act = ReturnLoginRequest};
            var cmd = new Command{Cmd = ServerCommands.AddClientAuthorized, Json = await SerializerAsync.SerializeJson(loginResult.Username)};
            await Zz.WriteServer(del, cmd);

            var conn = new ConnectionManager(loginResult);
            UsersConnections.Add(loginResult.Username, conn);
        }

        public static Timer SetTimer(double interval, ElapsedEventHandler e)
        {
            Timer t = new Timer(interval);
            t.Elapsed += e;
            t.AutoReset = false;
            t.Enabled = true;

            return t;
        }

        public static async Task RemoveUserConnection(string username)
        {
            var del = new DelegateAction { act = ReturnLogoutRequest };
            var cmd = new Command { Cmd = ServerCommands.RemoveClientAuthorized, Json = await SerializerAsync.SerializeJson(username) };
            await Zz.WriteServer(del, cmd);
        }


        public static async void ReturnLoginRequest(Object[] server, Object[] local)
        {
            var dataJson = SerializerAsync.DeserializeJson<Command>(server[0].ToString()).Result;
            try
            {
                if (dataJson != null)
                {
                    if (dataJson.Cmd.Equals(ServerCommands.LogResultOk))
                    {
                        var username = await SerializerAsync.DeserializeJson<string>(dataJson.Json);
                        if (UsersConnections.TryGetValue(username, out var conn))
                        {
                            await conn.Zz.WriteServer(new Command { Cmd = ServerCommands.IsUser, Json = await SerializerAsync.SerializeJson(username)});
                        }
                    }
                    else if (dataJson.Cmd.Equals(ServerCommands.LogResultDeny))
                    {
                        var username = await SerializerAsync.DeserializeJson<string>(dataJson.Json);
                        await RemoveUserConnection(username);
                    }

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public static async void ReturnLogoutRequest(Object[] server, Object[] local)
        {
            var dataJson = SerializerAsync.DeserializeJson<Command>(server[0].ToString()).Result;
            try
            {
                if (dataJson != null)
                {
                    if (dataJson.Cmd.Equals(ServerCommands.LogResultOk))
                    {
                        var username = await SerializerAsync.DeserializeJson<string>(dataJson.Json);
                        UsersConnections.Remove(username);
                        ConsoleEx.WriteLine("Usuario removido da lista de autorizados com sucesso");
                    }
                    else if (dataJson.Cmd.Equals(ServerCommands.LogResultDeny))
                    {
                        var username = await SerializerAsync.DeserializeJson<string>(dataJson.Json);
                        UsersConnections.Remove(username);
                        ConsoleEx.WriteLine("Deu merda ao remover o usuario da lista de autorizados");
                    }

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

    }
}
