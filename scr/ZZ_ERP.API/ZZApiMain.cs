using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ZZ_ERP.API.Connections;
using ZZ_ERP.Infra.CrossCutting.Connections.Commons;
using ZZ_ERP.Infra.CrossCutting.Connections.Connections;
using ZZ_ERP.Infra.CrossCutting.Connections.Functions;
using ZZ_ERP.Infra.CrossCutting.DTO.Interfaces;

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

        public static async Task AddUserConnection(string userId)
        {
            var conn = new ConnectionManager(userId);
            UsersConnections.Add(userId,conn);
        }
    }
}
