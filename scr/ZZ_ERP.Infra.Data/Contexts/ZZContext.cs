using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ZZ_ERP.Domain.Entities;
using ZZ_ERP.Infra.Data.Identity;

namespace ZZ_ERP.Infra.Data.Contexts
{
    public class ZZContext : IdentityDbContext<UserAccount>
    {

        public DbSet<TipoServico> TipoServicos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            Console.WriteLine("Configurando Conexao 332");

            const string connection =
                "Server=dbfragata.cmcait8irl6a.us-east-2.rds.amazonaws.com,1433;Database=ZZ_ERP_Test;User Id=melchior;Password=Otiagoehfoda";
            optionsBuilder.UseSqlServer(connection);
            base.OnConfiguring(optionsBuilder);
        }
    }
}
