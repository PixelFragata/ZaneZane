using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using ZZ_ERP.DataApplication.Clients;
using ZZ_ERP.Infra.CrossCutting.Connections.Commons;
using ZZ_ERP.Infra.CrossCutting.Connections.Connections;
using ZZ_ERP.Infra.CrossCutting.Connections.Functions;

namespace ZZ_ERP.DataApplication
{
    public class ZZClientManager
    {
        public ZZServer Server { get; }
        private TcpClient _tcpClient;
        private ServerConnection connection;
        private readonly bool _ownsClient;
        public   string MyId { get; private set; }
        private IClient _client;

        public ZZClientManager(TcpClient tcpClient, bool ownsClient, ZZServer server)
        {
            _tcpClient = tcpClient;
            _ownsClient = ownsClient;
            Server = server;
            DelegateAction del = new DelegateAction();
            del.act = ProcessLogin;
            connection = new ServerConnection(_tcpClient, this);
            connection.PutDelegate(del);
            ConsoleEx.WriteLine("Client conectado");
             
        }

        public void ProcessLogin(Object[] server, Object[] local)
        {
            var dataJson = SerializerAsync.DeserializeJson<Command>(server[0].ToString()).Result;
            try
            {
                if (dataJson != null)
                {
                    if (!dataJson.Cmd.Equals(ServerCommands.Exit))
                    {
                        if (dataJson.Cmd.Equals(ServerCommands.IsController))
                        {
                            ConsoleEx.WriteLine("Oi Controller fofo *3* ");
                            _client = new ControllerClient();
                            MyId = ServerCommands.IsController;
                            _client.Login(this, connection);
                        }
                        else if (dataJson.Cmd.Equals(ServerCommands.IsUser))
                        {
                            if (Server.VerifyUserAuthorization(
                                SerializerAsync.DeserializeJson<string>(dataJson.Json).Result, this))
                            {
                                ConsoleEx.WriteLine("Oi Cli fofo *3* ");
                                MyId = SerializerAsync.DeserializeJson<string>(dataJson.Json).Result;
                                _client = new UserClient();
                                _client.Login(this, connection);
                            }
                            else
                            {
                                ConsoleEx.WriteLine("Cliente não autorizado");
                                connection.WriteServer("", 542, ServerCommands.LogResultDeny, "");
                            }
                        }
                        else
                        {
                            _client.Command(dataJson);
                        }

                    }
                }
            }
            catch (Exception e)
            {
                ConsoleEx.WriteLine("Erro na criação do client : " + " | " + _client + " | " +
                                    dataJson + " | " + e.Message);
            }
        }

        public void Dispose()
        {
            _client.Logout();
            //connection.Dispose();
            ConsoleEx.WriteLine("ZZ foi pro ceu");
        }
    }
}
