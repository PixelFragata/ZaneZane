using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using ZZ_ERP.DataApplication.EntitiesManager;
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
        public const DayOfWeek DayUpdateSaldo = DayOfWeek.Sunday;

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
            //    var permissaoRep = new Repository<PermissaoTela>(context);

            //    await permissaoRep.Insert(new PermissaoTela { NomeTela = ServerCommands.UserPlanta, Codigo = "UP" });
            //    await permissaoRep.Save();
            //}

            ServerConn = new TcpListener(IPAddress.Parse(AdressPool.ZZ_EF_APK.Ip), AdressPool.ZZ_EF_APK.Port);
            ServerConn.Start();

            //await InitializeData();

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

        private static async Task InitializeData()
        {
            var context = new ZZContext();



            var unidadeRep = new Repository<UnidadeMedida>(context);
            var unidadeList = new List<UnidadeMedida>();
            unidadeList.Add(new UnidadeMedida { Descricao = "UNIDADE", Codigo = "UN", Sigla = "UN" });
            unidadeList.Add(new UnidadeMedida { Descricao = "METRO", Codigo = "M", Sigla = "M"});
            unidadeList.Add(new UnidadeMedida { Descricao = "METRO LINEAR", Codigo = "ML", Sigla = "ML" });
            unidadeList.Add(new UnidadeMedida { Descricao = "METRO QUADRADO", Codigo = "M2", Sigla = "M2" });
            unidadeList.Add(new UnidadeMedida { Descricao = "LITRO", Codigo = "L", Sigla = "L" });
            unidadeList.Add(new UnidadeMedida { Descricao = "CONJUNTO", Codigo = "CJ", Sigla = "CJ" });
            unidadeList.Add(new UnidadeMedida { Descricao = "HORAS", Codigo = "H", Sigla = "H" });

            await unidadeRep.InsertList(unidadeList);
            await unidadeRep.Save();

            var tipoServicoRep = new Repository<TipoServico>(context);
            var tipoServicoList = new List<TipoServico>();
            tipoServicoList.Add(new TipoServico { Descricao = "MATERIAL", Codigo = "MAT" });
            tipoServicoList.Add(new TipoServico { Descricao = "TROCA PECAS", Codigo = "TRC" });
            tipoServicoList.Add(new TipoServico { Descricao = "INSUMOS", Codigo = "INS" });
            tipoServicoList.Add(new TipoServico { Descricao = "CLIMATIZADOR", Codigo = "CLIMAT" });
            tipoServicoList.Add(new TipoServico { Descricao = "MANUTENCAO", Codigo = "MANUT" });
            tipoServicoList.Add(new TipoServico { Descricao = "INSTALACAO", Codigo = "INST" });
            tipoServicoList.Add(new TipoServico { Descricao = "SERVICOS GERAIS", Codigo = "SERVG" });

            await tipoServicoRep.InsertList(tipoServicoList);
            await tipoServicoRep.Save();

            var tipoOSRep = new Repository<TipoOS>(context);
            var tipoOSList = new List<TipoOS>();
            tipoOSList.Add(new TipoOS { Descricao = "CONSTRUCAO", Codigo = "CTOR" });
            tipoOSList.Add(new TipoOS { Descricao = "REFORMA", Codigo = "REF" });
            tipoOSList.Add(new TipoOS { Descricao = "CLIMATIZADOR", Codigo = "CLIMAT" });
            tipoOSList.Add(new TipoOS { Descricao = "MANUTENCAO", Codigo = "MANUT" });

            await tipoOSRep.InsertList(tipoOSList);
            await tipoOSRep.Save();

            var tipoEntradaRep = new Repository<TipoEntrada>(context);
            var tipoEntradaList = new List<TipoEntrada>();
            tipoEntradaList.Add(new TipoEntrada { Descricao = "NOTA FISCAL", Codigo = "NF" });
            tipoEntradaList.Add(new TipoEntrada { Descricao = "NOTINHA", Codigo = "NT", ControlaEstoque = true});
            tipoEntradaList.Add(new TipoEntrada { Descricao = "REEMBOLSO", Codigo = "RB", ControlaEstoque = true});

            await tipoEntradaRep.InsertList(tipoEntradaList);
            await tipoEntradaRep.Save();
             
            var estoqueRep = new Repository<Estoque>(context);
            var estoqueList = new List<Estoque>();
            estoqueList.Add(new Estoque { Descricao = "MATRIZ", Codigo = "MT" });
            estoqueList.Add(new Estoque { Descricao = "BERGUE", Codigo = "B" });
            estoqueList.Add(new Estoque { Descricao = "CESAR", Codigo = "C" }); 

            await estoqueRep.InsertList(estoqueList);
            await estoqueRep.Save();

            var pagamentoRep = new Repository<CondicaoPagamento>(context);
            var pagamentoList = new List<CondicaoPagamento>();
            pagamentoList.Add(new CondicaoPagamento { Descricao = "A VISTA", Codigo = "V" });
            pagamentoList.Add(new CondicaoPagamento { Descricao = "5 DIAS", Codigo = "5D" });
            pagamentoList.Add(new CondicaoPagamento { Descricao = "28 DIAS", Codigo = "28D" });

            await pagamentoRep.InsertList(pagamentoList);
            await pagamentoRep.Save();

            var centroRep = new Repository<CentroCustoSintetico>(context);
            var centroList = new List<CentroCustoSintetico>();
            centroList.Add(new CentroCustoSintetico { Descricao = "MATERIAL", Codigo = "MAT" });
            centroList.Add(new CentroCustoSintetico { Descricao = "TROCA PECAS", Codigo = "TRC" });
            centroList.Add(new CentroCustoSintetico { Descricao = "INSUMOS", Codigo = "INS" });
            centroList.Add(new CentroCustoSintetico { Descricao = "MANUTENCAO", Codigo = "MANUT" });
            centroList.Add(new CentroCustoSintetico { Descricao = "CLIMATIZADOR", Codigo = "CLIMAT" });
            centroList.Add(new CentroCustoSintetico { Descricao = "INSTALACAO", Codigo = "INST" });
            centroList.Add(new CentroCustoSintetico { Descricao = "SERVICOS GERAIS", Codigo = "SERVG" });

            await centroRep.InsertList(centroList);
            await centroRep.Save();

            var plantaRep = new Repository<Planta>(context);
            var planta = new Planta
            {
                Codigo = "ZANE",
                Documento = "97.547.310/0001-62",
                Email = "patricia@zanezane.com",
                InscricaoEstadual = "587.151.010.112",
                Telefone = "30233580",
                RazaoSocial = "ZANE & ZANE INSTALACAO DE CALHAS LTDA",
                NomeFantasia = "ZANE & ZANE",
            };


            await plantaRep.Insert(planta);
            await plantaRep.Save();

            await InitilizeServicos(context, tipoServicoList, unidadeList, centroList); 
        }

        private static async Task InitilizeServicos(ZZContext context, List<TipoServico> tipoServicoList, List<UnidadeMedida> unidadeList, List<CentroCustoSintetico> centroList)
        {
            var servicoRep = new Repository<Servico>(context);
            var servicoList = new List<Servico>();

            servicoList.Add(new Servico
            {
                DescricaoResumida = "CLIMATIZADOR", Codigo = "CLIMAT", ControlaEstoque = false,
                TipoServico = tipoServicoList.Find(t => t.Codigo.Equals("CLIMAT")),
                UnidadeMedida = unidadeList.Find(t => t.Codigo.Equals("UN")),
                CentroCusto = centroList.Find(t => t.Codigo.Equals("CLIMAT"))
            });
            servicoList.Add(new Servico
            {
                DescricaoResumida = "LIMPEZA CAIXA DE AGUA", Codigo = "CXAGUA", ControlaEstoque = false,
                TipoServico = tipoServicoList.Find(t => t.Codigo.Equals("MANUT")),
                UnidadeMedida = unidadeList.Find(t => t.Codigo.Equals("UN")),
                CentroCusto = centroList.Find(t => t.Codigo.Equals("MANUT"))
            });
            servicoList.Add(new Servico
            {
                DescricaoResumida = "BOMBA",
                Codigo = "BOM",
                ControlaEstoque = true,
                TipoServico = tipoServicoList.Find(t => t.Codigo.Equals("MAT")),
                UnidadeMedida = unidadeList.Find(t => t.Codigo.Equals("UN")),
                CentroCusto = centroList.Find(t => t.Codigo.Equals("MAT"))
            });
            servicoList.Add(new Servico
            {
                DescricaoResumida = "TROCA BOMBA",
                Codigo = "TRBOM",
                ControlaEstoque = false,
                TipoServico = tipoServicoList.Find(t => t.Codigo.Equals("TRC")),
                UnidadeMedida = unidadeList.Find(t => t.Codigo.Equals("UN")),
                CentroCusto = centroList.Find(t => t.Codigo.Equals("TRC"))
            });
            servicoList.Add(new Servico
            {
                DescricaoResumida = "VALVULA",
                Codigo = "VAL",
                ControlaEstoque = true,
                TipoServico = tipoServicoList.Find(t => t.Codigo.Equals("MAT")),
                UnidadeMedida = unidadeList.Find(t => t.Codigo.Equals("UN")),
                CentroCusto = centroList.Find(t => t.Codigo.Equals("MAT"))
            });
            servicoList.Add(new Servico
            {
                DescricaoResumida = "TROCA VALVULA",
                Codigo = "TRVAL",
                ControlaEstoque = false,
                TipoServico = tipoServicoList.Find(t => t.Codigo.Equals("TRC")),
                UnidadeMedida = unidadeList.Find(t => t.Codigo.Equals("UN")),
                CentroCusto = centroList.Find(t => t.Codigo.Equals("TRC"))
            });
            servicoList.Add(new Servico
            {
                DescricaoResumida = "MANTA ACRILICA",
                Codigo = "MAN",
                ControlaEstoque = true,
                TipoServico = tipoServicoList.Find(t => t.Codigo.Equals("MAT")),
                UnidadeMedida = unidadeList.Find(t => t.Codigo.Equals("M")),
                CentroCusto = centroList.Find(t => t.Codigo.Equals("MAT"))
            });
            servicoList.Add(new Servico
            {
                DescricaoResumida = "TROCA MANTA ACRILICA",
                Codigo = "TRMAN",
                ControlaEstoque = false,
                TipoServico = tipoServicoList.Find(t => t.Codigo.Equals("TRC")),
                UnidadeMedida = unidadeList.Find(t => t.Codigo.Equals("M")),
                CentroCusto = centroList.Find(t => t.Codigo.Equals("TRC"))
            });
            servicoList.Add(new Servico
            {
                DescricaoResumida = "SUPORTE DE BARRA",
                Codigo = "SUPBAR",
                ControlaEstoque = true,
                TipoServico = tipoServicoList.Find(t => t.Codigo.Equals("MAT")),
                UnidadeMedida = unidadeList.Find(t => t.Codigo.Equals("UN")),
                CentroCusto = centroList.Find(t => t.Codigo.Equals("MAT"))
            });
            servicoList.Add(new Servico
            {
                DescricaoResumida = "INSTALACAO DE CONJUNTO SUPORTE DE BARRA",
                Codigo = "INSTCONJSUPBAR",
                ControlaEstoque = false,
                TipoServico = tipoServicoList.Find(t => t.Codigo.Equals("SERVG")),
                UnidadeMedida = unidadeList.Find(t => t.Codigo.Equals("UN")),
                CentroCusto = centroList.Find(t => t.Codigo.Equals("SERVG"))
            });
            servicoList.Add(new Servico
            {
                DescricaoResumida = "INSTALACAO DE SUPORTE DE BARRA ADICIONAL",
                Codigo = "INSTSUPBAR",
                ControlaEstoque = false,
                TipoServico = tipoServicoList.Find(t => t.Codigo.Equals("SERVG")),
                UnidadeMedida = unidadeList.Find(t => t.Codigo.Equals("UN")),
                CentroCusto = centroList.Find(t => t.Codigo.Equals("SERVG"))
            });
            servicoList.Add(new Servico
            {
                DescricaoResumida = "DESENTUPIMENTO DE MICTORIO",
                Codigo = "MIC",
                ControlaEstoque = false,
                TipoServico = tipoServicoList.Find(t => t.Codigo.Equals("SERVG")),
                UnidadeMedida = unidadeList.Find(t => t.Codigo.Equals("UN")),
                CentroCusto = centroList.Find(t => t.Codigo.Equals("SERVG"))
            });
            servicoList.Add(new Servico
            {
                DescricaoResumida = "BOIA",
                Codigo = "BOIA",
                ControlaEstoque = true,
                TipoServico = tipoServicoList.Find(t => t.Codigo.Equals("MAT")),
                UnidadeMedida = unidadeList.Find(t => t.Codigo.Equals("UN")),
                CentroCusto = centroList.Find(t => t.Codigo.Equals("MAT"))
            });
            servicoList.Add(new Servico
            {
                DescricaoResumida = "TROCA BOIA",
                Codigo = "TRBOIA",
                ControlaEstoque = false,
                TipoServico = tipoServicoList.Find(t => t.Codigo.Equals("TRC")),
                UnidadeMedida = unidadeList.Find(t => t.Codigo.Equals("UN")),
                CentroCusto = centroList.Find(t => t.Codigo.Equals("TRC"))
            });
            servicoList.Add(new Servico
            {
                DescricaoResumida = "NUCLEO",
                Codigo = "NC",
                ControlaEstoque = true,
                TipoServico = tipoServicoList.Find(t => t.Codigo.Equals("MAT")),
                UnidadeMedida = unidadeList.Find(t => t.Codigo.Equals("UN")),
                CentroCusto = centroList.Find(t => t.Codigo.Equals("MAT"))
            });
            servicoList.Add(new Servico
            {
                DescricaoResumida = "TROCA NUCLEO",
                Codigo = "TRNC",
                ControlaEstoque = false,
                TipoServico = tipoServicoList.Find(t => t.Codigo.Equals("TRC")),
                UnidadeMedida = unidadeList.Find(t => t.Codigo.Equals("UN")),
                CentroCusto = centroList.Find(t => t.Codigo.Equals("TRC"))
            });
            servicoList.Add(new Servico
            {
                DescricaoResumida = "SUPORTE MOTOR",
                Codigo = "SUPM",
                ControlaEstoque = true,
                TipoServico = tipoServicoList.Find(t => t.Codigo.Equals("MAT")),
                UnidadeMedida = unidadeList.Find(t => t.Codigo.Equals("CJ")),
                CentroCusto = centroList.Find(t => t.Codigo.Equals("MAT"))
            });
            servicoList.Add(new Servico
            {
                DescricaoResumida = "TROCA SUPORTE MOTOR",
                Codigo = "TRSUPM",
                ControlaEstoque = false,
                TipoServico = tipoServicoList.Find(t => t.Codigo.Equals("TRC")),
                UnidadeMedida = unidadeList.Find(t => t.Codigo.Equals("UN")),
                CentroCusto = centroList.Find(t => t.Codigo.Equals("TRC"))
            });
            servicoList.Add(new Servico
            {
                DescricaoResumida = "EIXO MOTOR",
                Codigo = "EIX",
                ControlaEstoque = true,
                TipoServico = tipoServicoList.Find(t => t.Codigo.Equals("MAT")),
                UnidadeMedida = unidadeList.Find(t => t.Codigo.Equals("UN")),
                CentroCusto = centroList.Find(t => t.Codigo.Equals("MAT"))
            });
            servicoList.Add(new Servico
            {
                DescricaoResumida = "TROCA EIXO MOTOR",
                Codigo = "TREIX",
                ControlaEstoque = false,
                TipoServico = tipoServicoList.Find(t => t.Codigo.Equals("TRC")),
                UnidadeMedida = unidadeList.Find(t => t.Codigo.Equals("UN")),
                CentroCusto = centroList.Find(t => t.Codigo.Equals("TRC"))
            });
            servicoList.Add(new Servico
            {
                DescricaoResumida = "TINTA CAIXA PRETA",
                Codigo = "TCXP",
                ControlaEstoque = true,
                TipoServico = tipoServicoList.Find(t => t.Codigo.Equals("MAT")),
                UnidadeMedida = unidadeList.Find(t => t.Codigo.Equals("L")),
                CentroCusto = centroList.Find(t => t.Codigo.Equals("MAT"))
            });
            servicoList.Add(new Servico
            {
                DescricaoResumida = "PINTURA CAIXA PRETA",
                Codigo = "PINCXP",
                ControlaEstoque = false,
                TipoServico = tipoServicoList.Find(t => t.Codigo.Equals("SERVG")),
                UnidadeMedida = unidadeList.Find(t => t.Codigo.Equals("L")),
                CentroCusto = centroList.Find(t => t.Codigo.Equals("SERVG"))
            });
            servicoList.Add(new Servico
            {
                DescricaoResumida = "MOTOR",
                Codigo = "MTR",
                ControlaEstoque = true,
                TipoServico = tipoServicoList.Find(t => t.Codigo.Equals("MAT")),
                UnidadeMedida = unidadeList.Find(t => t.Codigo.Equals("UN")),
                CentroCusto = centroList.Find(t => t.Codigo.Equals("MAT"))
            });
            servicoList.Add(new Servico
            {
                DescricaoResumida = "TROCA MOTOR",
                Codigo = "TRMTR",
                ControlaEstoque = false,
                TipoServico = tipoServicoList.Find(t => t.Codigo.Equals("TRC")),
                UnidadeMedida = unidadeList.Find(t => t.Codigo.Equals("UN")),
                CentroCusto = centroList.Find(t => t.Codigo.Equals("TRC"))
            });
            servicoList.Add(new Servico
            {
                DescricaoResumida = "PINTURA MARQUISE",
                Codigo = "PMARQ",
                ControlaEstoque = false,
                TipoServico = tipoServicoList.Find(t => t.Codigo.Equals("SERVG")),
                UnidadeMedida = unidadeList.Find(t => t.Codigo.Equals("UN")),
                CentroCusto = centroList.Find(t => t.Codigo.Equals("SERVG"))
            });

            await servicoRep.InsertList(servicoList);
            await servicoRep.Save();
        }
    }
}
