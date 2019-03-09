using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using ZZ_ERP.Domain.Entities;
using ZZ_ERP.Infra.CrossCutting.Connections.Commons;
using ZZ_ERP.Infra.CrossCutting.Connections.Functions;
using ZZ_ERP.Infra.Data.Contexts;
using ZZ_ERP.Infra.Data.Identity;
using ZZ_ERP.Infra.Data.Repositories;

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

        private static async Task InitializeTipoPermissao(ZZContext context)
        {
            var permissaoRep = new Repository<TipoPermissao>(context);
            var permissaoList = new List<TipoPermissao>();
            permissaoList.Add(new TipoPermissao {Descricao = "Create"});
            permissaoList.Add(new TipoPermissao {Descricao = "Read"});
            permissaoList.Add(new TipoPermissao {Descricao = "Update"});
            permissaoList.Add(new TipoPermissao {Descricao = "Delete"});


            await permissaoRep.InsertList(permissaoList);
            await permissaoRep.Save();

        }

        private static async Task InitializePermissaoTelas(ZZContext context)
        {
            var permissaoRep = new Repository<PermissaoTela>(context);
            var permissaoList = new List<PermissaoTela>();
            permissaoList.Add(new PermissaoTela { NomeTela = "UserManager" });
            permissaoList.Add(new PermissaoTela { NomeTela = "RoleManager" });
            permissaoList.Add(new PermissaoTela { NomeTela = "TipoServico" });


            await permissaoRep.InsertList(permissaoList);
            await permissaoRep.Save();
        }
    }
}
