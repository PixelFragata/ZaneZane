using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ZZ_ERP.Domain.Entities;
using ZZ_ERP.Infra.Data.Config;
using ZZ_ERP.Infra.Data.Identity;

namespace ZZ_ERP.Infra.Data.Contexts
{
    public class ZZContext : IdentityDbContext<UserAccount>
    {

        public DbSet<TipoServico> TipoServicos { get; set; }
        public DbSet<PermissaoTela> PermissaoTelas { get; set; }
        public DbSet<TipoPermissao> TipoPermissoes { get; set; }
        public DbSet<UnidadeMedida> UnidadeMedidas { get; set; }
        public DbSet<TipoOS> TiposOS { get; set; }
        public DbSet<CondicaoPagamento> CondicaoPagamentos { get; set; }
        public DbSet<CentroCustoSintetico> CentrosCustoSintetico { get; set; }
        public DbSet<TabelaCusto> TabelasCusto { get; set; }
        public DbSet<Servico> Servicos { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<Fornecedor> Fornecedores { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Estoque> Estoques { get; set; }
        public DbSet<TipoEntrada> TipoEntradas { get; set; }
        public DbSet<FuncionarioEstoque> FuncionarioEstoques { get; set; }
        public DbSet<Planta> Plantas { get; set; }
        public DbSet<ItemCompra> ItensCompras { get; set; }
        public DbSet<CompraManual> ComprasManuais { get; set; }
        public DbSet<MovimentoEstoque> MovimentosEstoque { get; set; }
        public DbSet<SaldoEstoque> SaldosEstoques { get; set; }
        public DbSet<UserPlanta> UserPlantas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            const string connection =
                "Server=zanedb.database.windows.net;Database=ZaneERP;User Id=zanesa;Password=zane$server1406";
            optionsBuilder.UseSqlServer(connection);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PermissaoTelaConfig());
            modelBuilder.ApplyConfiguration(new TipoPermissaoConfig());
            modelBuilder.ApplyConfiguration(new UnidadeMedidaConfig());
            modelBuilder.ApplyConfiguration(new EstadoConfig());
            modelBuilder.ApplyConfiguration(new TabelaCustoConfig());
            modelBuilder.ApplyConfiguration(new ServicoConfig());
            modelBuilder.ApplyConfiguration(new EnderecoConfig());
            modelBuilder.ApplyConfiguration(new FuncionarioConfig());
            modelBuilder.ApplyConfiguration(new CompraManualConfig());
            modelBuilder.ApplyConfiguration(new TipoEntradaConfig());
            modelBuilder.ApplyConfiguration(new MovimentoEstoqueConfig());

            base.OnModelCreating(modelBuilder);
        }
    }
}
