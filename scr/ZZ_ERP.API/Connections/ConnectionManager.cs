using System;
using System.Threading;
using System.Timers;
using ZZ_ERP.Infra.CrossCutting.Connections.Commons;
using ZZ_ERP.Infra.CrossCutting.Connections.Connections;
using ZZ_ERP.Infra.CrossCutting.Connections.Functions;
using ZZ_ERP.Infra.CrossCutting.DTO.EntitiesDTO;
using ZZ_ERP.Infra.CrossCutting.DTO.Interfaces;
using Timer = System.Timers.Timer;

namespace ZZ_ERP.API.Connections
{
    public class ConnectionManager : IConnectionManager
    {
        public ServerConnection Zz { get; }
        public LoginResultDto LoginResultDto { get; set; }
        public Command Command { get; set; }
        private Timer _expirationTimer;

        public ConnectionManager(LoginResultDto loginResultDto)
        {
            LoginResultDto = loginResultDto;
            Zz = new ServerConnection(AdressPool.ZZ_EF_APK.Ip, AdressPool.ZZ_EF_APK.Port);
            DelegateAction del = new DelegateAction();
            del.act = ReturnServer;

            Zz.PutDelegate(del);
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
                            _expirationTimer =
                                ZZApiMain.SetTimer(
                                    (LoginResultDto.ExpirationDate - LoginResultDto.CreatedDate).TotalMilliseconds,
                                    ExpirationTokenTimer);
                        }
                        else if(dataJson.Cmd.Equals(ServerCommands.LogResultDeny))
                        {
                            ConsoleEx.WriteLine("Algo deu errado");
                            ZZApiMain.RemoveUserConnection(LoginResultDto.Username);
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

        private void ExpirationTokenTimer(Object source, ElapsedEventArgs e)
        {
            Zz.WriteServer(new Command {Cmd = ServerCommands.Logout, Json = SerializerAsync.SerializeJson("Token expired").Result});
            ZZApiMain.RemoveUserConnection(LoginResultDto.Username);
        }
    }
}
