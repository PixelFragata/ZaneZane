using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ZZ_ERP.Infra.CrossCutting.DTO.Interfaces;
using ZZ_ERP.Infra.Data.Contexts;
using ZZ_ERP.Infra.Data.Identity;

namespace ZZ_ERP.DependencyInjection
{
    public class ZZ_DI
    {
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
        }
         
    }
}
