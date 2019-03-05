using System;
using System.Threading;
using ZZ_ERP.Infra.CrossCutting.Connections.Commons;
using ZZ_ERP.Infra.CrossCutting.Connections.Connections;
using ZZ_ERP.Infra.CrossCutting.Connections.Functions;
using ZZ_ERP.Infra.CrossCutting.DTO.Interfaces;

namespace ZZ_ERP.API.Connections
{
    public class ConnectionManager : IConnectionManager
    {
        public ServerConnection Zz { get; }
        public Command Command { get; set; }

        public ConnectionManager(string userId)
        {
            Zz = new ServerConnection(AdressPool.ZZ_EF_APK.Ip, AdressPool.ZZ_EF_APK.Port);
            DelegateAction del = new DelegateAction();
            del.act = ReturnServer;

            Zz.PutDelegate(del);
            Thread.Sleep(1000);
            Zz.WriteServer(new Command { Cmd = ServerCommands.IsUser, Json = SerializerAsync.SerializeJson(userId).Result});
        }

        public void ReturnServer(Object[] server, Object[] local)
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
                            ConsoleEx.WriteLine("Uhulll, consegui logar");
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
    }
}
