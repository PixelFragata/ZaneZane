using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ZZ_ERP.Infra.CrossCutting.DTO.Interfaces;
using ZZ_ERP.Infra.Data.Contexts;
using ZZ_ERP.Infra.Data.Identity;

namespace ZZ_ERP.DependencyInjection
{
    public class ZZ_DI
    {
        private static IConfiguration _configuration { get; set; }

        public static void Configure(IServiceCollection services)
        {
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

            services.AddAuthentication(authOptions =>
            {
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(bearerOptions =>
            {
                var paramsValidation = bearerOptions.TokenValidationParameters;
                paramsValidation.IssuerSigningKey = signingConfigurations.Key;
                paramsValidation.ValidAudience = tokenConfigurations.Audience;
                paramsValidation.ValidIssuer = tokenConfigurations.Issuer;

                // Valida a assinatura de um token recebido
                paramsValidation.ValidateIssuerSigningKey = true;

                // Verifica se um token recebido ainda é válido
                paramsValidation.ValidateLifetime = true;

                // Tempo de tolerância para a expiração de um token (utilizado
                // caso haja problemas de sincronismo de horário entre diferentes
                // computadores envolvidos no processo de comunicação)
                paramsValidation.ClockSkew = TimeSpan.Zero;
            });

            
        }
         
    }
}
