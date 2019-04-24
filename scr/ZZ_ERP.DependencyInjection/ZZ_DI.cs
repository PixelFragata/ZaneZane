using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using ZZ_ERP.Domain.Entities;
using ZZ_ERP.Infra.CrossCutting.Connections.Commons;
using ZZ_ERP.Infra.CrossCutting.DTO.Interfaces;
using ZZ_ERP.Infra.Data.Contexts;
using ZZ_ERP.Infra.Data.Identity;
using ZZ_ERP.Infra.Data.Repositories;

namespace ZZ_ERP.DependencyInjection
{
    public class ZZ_DI
    {
        private static IConfiguration _configuration { get; set; }

        public static void Configure(IServiceCollection services)
        {
            //var context = new ZZContext();

            //InitializeTipoPermissao(context);
            //InitializePermissaoTelas(context);

            services.AddDbContext<ZZContext>();
            services.AddIdentity<UserAccount, IdentityRole>(config =>
                {
                    config.Password.RequireDigit = false;
                    config.Password.RequiredLength = 3;
                    config.Password.RequireLowercase = false;
                    config.Password.RequireNonAlphanumeric = false;
                    config.Password.RequireUppercase = false;
                }).AddEntityFrameworkStores<ZZContext>()
                .AddDefaultTokenProviders();
            

            services.AddScoped(typeof(IAuthentication), typeof(Authentication));
            services.AddScoped(typeof(IAccountManager), typeof(AccountManager));
            services.AddScoped(typeof(IRoleManager), typeof(RoleManager));
            
            var signingConfigurations = SigningConfigurations.Instance;
            
            var tokenConfigurations = TokenConfigurations.Instance;

            /*services.AddAuthentication(authOptions =>
            {
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }*/
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(bearerOptions =>
                {
                    bearerOptions.TokenValidationParameters = new TokenValidationParameters
                    {

                        ValidateIssuer = true,
                        ValidateActor = true,
                        ValidateAudience = true,
                        IssuerSigningKey = signingConfigurations.Key,
                        ValidAudience = tokenConfigurations.Audience,
                        ValidIssuer = tokenConfigurations.Issuer,

                        // Valida a assinatura de um token recebido
                        ValidateIssuerSigningKey = true,

                        // Verifica se um token recebido ainda é válido
                        ValidateLifetime = true,

                        // Tempo de tolerância para a expiração de um token (utilizado
                        // caso haja problemas de sincronismo de horário entre diferentes
                        // computadores envolvidos no processo de comunicação)
                        ClockSkew = TimeSpan.Zero
                    };
                });

            var myPolicies = new MyPolicesRepository();
            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser().Build());
                foreach (var policy in myPolicies.MyPolicies)
                {
                    auth.AddPolicy(policy.PolicyName, p => p.RequireClaim(policy.PermissaoTela, policy.TipoPermissao).AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme));
                }
                //auth.AddPolicy("RoleManagerCreate",p => p.RequireClaim("RoleManager", "Create").AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme));
                //auth.AddPolicy("UserManagerRead", p => p.RequireClaim("UserManager", "Read").AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme));
            });
        }


        private static void InitializeTipoPermissao(ZZContext context)
        {
            var permissaoRep = new Repository<TipoPermissao>(context);
            var permissaoList = new List<TipoPermissao>();
            permissaoList.Add(new TipoPermissao { Descricao = "Create", Codigo = "Create"});
            permissaoList.Add(new TipoPermissao { Descricao = "Read", Codigo = "Read" });
            permissaoList.Add(new TipoPermissao { Descricao = "Update", Codigo = "Update" });
            permissaoList.Add(new TipoPermissao { Descricao = "Delete", Codigo = "Delete" });


            permissaoRep.InsertList(permissaoList);
            permissaoRep.Save();

        }

        private static void InitializePermissaoTelas(ZZContext context)
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

            permissaoRep.InsertList(permissaoList);
            permissaoRep.Save();
        }

        private static void InitializeAdminUser(RoleManager roleManager)
        {
            var context = new ZZContext();

            var permissaoRep = new Repository<TipoPermissao>(context);
            var telasRep = new Repository<PermissaoTela>(context);

            var permissaoList = permissaoRep.Get().Result;
            var telasList = telasRep.Get().Result;
            var tipoPermissaos = permissaoList.ToList();

            if (roleManager.CreateAsync("Admin").Result)
            {
                
                foreach (var tela in telasList)
                {
                    foreach (var permissao in tipoPermissaos)
                    {
                        roleManager.AddRoleClaim("Admin", tela.NomeTela, permissao.Descricao);
                    }
                }
            }
        }
    }
}
