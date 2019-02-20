using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using ZZ_ERP.Infra.CrossCutting.Connections.Functions;

namespace ZZ_ERP.DataApplication
{
    public class ZZServer
    {
        public TcpListener ServerConn;
        private string _ip;
        private int _port;

        public ZZServer(string ip, int port)
        {
            _ip = ip;
            _port = port;
        }

        public void Start()
        {
            try
            {
                StartServer().GetAwaiter().GetResult();

            }
            catch (Exception e)
            {
                ConsoleEx.WriteLine("Erro ao iniciar o servidor");
                Console.WriteLine("Erro :" + e);
            }
        }

        private async Task StartServer()
        {

            ServerConn = new TcpListener(IPAddress.Parse(_ip), _port);
            ServerConn.Start();

            while (true)
            {
                try
                {
                    var client = await ServerConn.AcceptTcpClientAsync().ConfigureAwait(false);
                    ConsoleEx.WriteLine("Cliente chegando!!!");
                    var cli = new ZZClientManager(client,true, this);
                }
                catch (Exception e)
                {
                    Console.WriteLine("{0} Erro ", e);
                    break;
                }
            }
        }
    }
}
