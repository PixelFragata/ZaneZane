﻿using System;
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
        private List<string> _authorizedUserList;
        private Dictionary<string, ZZClientManager> _authorizedClients;

        public ZZServer(string ip, int port)
        {
            _ip = ip;
            _port = port;
            _authorizedUserList = new List<string>();
            _authorizedClients = new Dictionary<string, ZZClientManager>();

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

            //using (var context = new ZZContext())
            //{
            //    //var permissaoRep = new Repository<PermissaoTela>(context);

            //    //await permissaoRep.Insert(new PermissaoTela { NomeTela = ServerCommands.TipoEntrada, Codigo = "TE" });
            //    //await permissaoRep.Save();
            //    await InitializeTipoPermissao(context);
            //    await InitializePermissaoTelas(context);
            //}

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

        public bool AddAuthorizedUser(string username)
        {
            ConsoleEx.WriteLine("Adicionando usuario autorizado na lista");
            if (_authorizedUserList.Contains(username))
            {
                return false;
            }
            _authorizedUserList.Add(username);
            return true;
        }

        public bool VerifyUserAuthorization(string username, ZZClientManager client)
        {
            if (_authorizedUserList.Contains(username))
            {
                ConsoleEx.WriteLine("Um usuario autorizado se conectou");
                _authorizedClients.Add(username,client);
                return true;
            }
            return false;
        }

        public bool RemoveAuthorizedUser(string username)
        {
            if (_authorizedUserList.Contains(username))
            { 
                if (_authorizedClients.TryGetValue(username, out var client))
                {
                    ConsoleEx.WriteLine("Usuario dando logout");
                    client.Dispose();
                    _authorizedUserList.Remove(username);
                    _authorizedClients.Remove(username);
                    return true;
                }
            }
            return false;
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
            permissaoList.Add(new PermissaoTela { NomeTela = "UserManager", Codigo = "UserM" });
            permissaoList.Add(new PermissaoTela { NomeTela = "RoleManager", Codigo = "RoleM" });
            permissaoList.Add(new PermissaoTela { NomeTela = ServerCommands.TipoServico, Codigo = "TS" });
            permissaoList.Add(new PermissaoTela { NomeTela = ServerCommands.UnidadeMedida, Codigo = "Un" });
            permissaoList.Add(new PermissaoTela { NomeTela = ServerCommands.TipoOS, Codigo = "TOS" });
            permissaoList.Add(new PermissaoTela { NomeTela = ServerCommands.CondicaoPagamento, Codigo = "CP" });
            permissaoList.Add(new PermissaoTela { NomeTela = ServerCommands.CentroCustoSintetico, Codigo = "CCS" });
            permissaoList.Add(new PermissaoTela { NomeTela = ServerCommands.TabelaCusto, Codigo = "TC" });
            permissaoList.Add(new PermissaoTela { NomeTela = ServerCommands.Servico, Codigo = "S" });
            permissaoList.Add(new PermissaoTela { NomeTela = ServerCommands.Localization, Codigo = "L" });
            permissaoList.Add(new PermissaoTela { NomeTela = ServerCommands.Funcionario, Codigo = "Fun" });
            permissaoList.Add(new PermissaoTela { NomeTela = ServerCommands.Fornecedor, Codigo = "For" });
            permissaoList.Add(new PermissaoTela { NomeTela = ServerCommands.Cliente, Codigo = "Cli" });
            permissaoList.Add(new PermissaoTela { NomeTela = ServerCommands.Estoque, Codigo = "E" });
            permissaoList.Add(new PermissaoTela { NomeTela = ServerCommands.TipoEntrada, Codigo = "TE" });

            await permissaoRep.InsertList(permissaoList);
            await permissaoRep.Save();
        }
    }
}
