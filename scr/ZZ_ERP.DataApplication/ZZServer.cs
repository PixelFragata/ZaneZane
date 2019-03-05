using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using ZZ_ERP.Infra.CrossCutting.Connections.Commons;
using ZZ_ERP.Infra.CrossCutting.Connections.Functions;
using ZZ_ERP.Infra.Data.Contexts;
using ZZ_ERP.Infra.Data.Identity;

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

            /*using (var context = new ZZContext())
            {
                //var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context), null, null, null, null);
                var manager = new AccountManager();
                //var role = new IdentityRole(Enum.GetName(typeof(Roles), Roles.Admin));
               // await roleManager.CreateAsync(role);
               await manager.CreateAsync("admin@admin.com","admin", new List<string>(){"Admin"});
               await context.SaveChangesAsync();
            }*/

            ServerConn = new TcpListener(IPAddress.Parse(AdressPool.ZZ_EF_APK.Ip), AdressPool.ZZ_EF_APK.Port);
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
