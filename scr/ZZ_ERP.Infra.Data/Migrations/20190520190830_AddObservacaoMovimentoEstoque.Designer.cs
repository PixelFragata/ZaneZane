﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ZZ_ERP.Infra.Data.Contexts;

namespace ZZ_ERP.Infra.Data.Migrations
{
    [DbContext(typeof(ZZContext))]
    [Migration("20190520190830_AddObservacaoMovimentoEstoque")]
    partial class AddObservacaoMovimentoEstoque
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.2-servicing-10034")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("ZZ_ERP.Domain.Entities.CentroCustoSintetico", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Codigo")
                        .IsRequired();

                    b.Property<string>("Descricao")
                        .IsRequired();

                    b.Property<bool>("IsActive");

                    b.HasKey("Id");

                    b.ToTable("CentrosCustoSintetico");
                });

            modelBuilder.Entity("ZZ_ERP.Domain.Entities.Cliente", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Codigo")
                        .IsRequired();

                    b.Property<string>("Documento")
                        .IsRequired();

                    b.Property<string>("Email");

                    b.Property<long>("EnderecoId");

                    b.Property<string>("InscricaoEstadual");

                    b.Property<bool>("IsActive");

                    b.Property<string>("NomeFantasia");

                    b.Property<string>("RazaoSocial")
                        .IsRequired();

                    b.Property<string>("Telefone");

                    b.HasKey("Id");

                    b.HasIndex("EnderecoId");

                    b.ToTable("Clientes");
                });

            modelBuilder.Entity("ZZ_ERP.Domain.Entities.CompraManual", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Codigo")
                        .IsRequired();

                    b.Property<bool>("ControlaEstoque");

                    b.Property<DateTime>("DataEmissao");

                    b.Property<long>("FornecedorId");

                    b.Property<bool>("IsActive");

                    b.Property<bool>("MovimentouEstoque");

                    b.Property<string>("Observacao");

                    b.Property<long>("TipoEntradaId");

                    b.Property<float>("ValorTotal");

                    b.HasKey("Id");

                    b.HasIndex("FornecedorId");

                    b.HasIndex("TipoEntradaId");

                    b.ToTable("ComprasManuais");
                });

            modelBuilder.Entity("ZZ_ERP.Domain.Entities.CondicaoPagamento", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Codigo")
                        .IsRequired();

                    b.Property<string>("Descricao")
                        .IsRequired();

                    b.Property<bool>("IsActive");

                    b.HasKey("Id");

                    b.ToTable("CondicaoPagamentos");
                });

            modelBuilder.Entity("ZZ_ERP.Domain.Entities.Endereco", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Bairro")
                        .IsRequired();

                    b.Property<string>("Cep")
                        .IsRequired()
                        .HasMaxLength(8);

                    b.Property<string>("CidadeDescricao");

                    b.Property<string>("Codigo")
                        .IsRequired();

                    b.Property<string>("Complemento");

                    b.Property<int>("GIACode");

                    b.Property<int>("Ibge");

                    b.Property<bool>("IsActive");

                    b.Property<string>("Logradouro")
                        .IsRequired();

                    b.Property<int>("Numero");

                    b.Property<string>("Uf");

                    b.HasKey("Id");

                    b.ToTable("Enderecos");
                });

            modelBuilder.Entity("ZZ_ERP.Domain.Entities.Estado", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Codigo")
                        .IsRequired();

                    b.Property<string>("Descricao")
                        .HasMaxLength(2147483647);

                    b.Property<int>("Ibge");

                    b.Property<bool>("IsActive");

                    b.Property<string>("Sigla")
                        .IsRequired()
                        .HasMaxLength(2);

                    b.HasKey("Id");

                    b.ToTable("Estado");
                });

            modelBuilder.Entity("ZZ_ERP.Domain.Entities.Estoque", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Codigo")
                        .IsRequired();

                    b.Property<string>("Descricao")
                        .IsRequired();

                    b.Property<bool>("IsActive");

                    b.HasKey("Id");

                    b.ToTable("Estoques");
                });

            modelBuilder.Entity("ZZ_ERP.Domain.Entities.Fornecedor", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Codigo")
                        .IsRequired();

                    b.Property<string>("Documento")
                        .IsRequired();

                    b.Property<string>("Email");

                    b.Property<long>("EnderecoId");

                    b.Property<string>("InscricaoEstadual");

                    b.Property<bool>("IsActive");

                    b.Property<string>("NomeFantasia");

                    b.Property<string>("RazaoSocial")
                        .IsRequired();

                    b.Property<string>("Telefone");

                    b.HasKey("Id");

                    b.HasIndex("EnderecoId");

                    b.ToTable("Fornecedores");
                });

            modelBuilder.Entity("ZZ_ERP.Domain.Entities.Funcionario", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Codigo")
                        .IsRequired();

                    b.Property<string>("Documento")
                        .IsRequired();

                    b.Property<string>("Email");

                    b.Property<long>("EnderecoId");

                    b.Property<bool>("IsActive");

                    b.Property<string>("NomeFantasia");

                    b.Property<string>("RazaoSocial")
                        .IsRequired();

                    b.Property<string>("Telefone");

                    b.HasKey("Id");

                    b.HasIndex("EnderecoId");

                    b.ToTable("Funcionarios");
                });

            modelBuilder.Entity("ZZ_ERP.Domain.Entities.FuncionarioEstoque", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Codigo")
                        .IsRequired();

                    b.Property<long>("EstoqueId");

                    b.Property<long>("FuncionarioId");

                    b.Property<bool>("IsActive");

                    b.HasKey("Id");

                    b.HasIndex("EstoqueId");

                    b.HasIndex("FuncionarioId");

                    b.ToTable("FuncionarioEstoques");
                });

            modelBuilder.Entity("ZZ_ERP.Domain.Entities.ItemCompra", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Codigo")
                        .IsRequired();

                    b.Property<long>("CompraManualId");

                    b.Property<bool>("IsActive");

                    b.Property<string>("Observacao");

                    b.Property<float>("Quantidade");

                    b.Property<long>("ServicoId");

                    b.Property<long>("UnidadeId");

                    b.Property<float>("ValorTotal");

                    b.Property<float>("ValorUnitario");

                    b.HasKey("Id");

                    b.HasIndex("CompraManualId");

                    b.HasIndex("ServicoId");

                    b.HasIndex("UnidadeId");

                    b.ToTable("ItensCompras");
                });

            modelBuilder.Entity("ZZ_ERP.Domain.Entities.MovimentoEstoque", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Codigo")
                        .IsRequired();

                    b.Property<DateTime>("Data");

                    b.Property<DateTime>("DataMovimento");

                    b.Property<long>("DocumentoId");

                    b.Property<long>("EstoqueId");

                    b.Property<bool>("IsActive");

                    b.Property<bool>("IsEntrada");

                    b.Property<string>("Observacao");

                    b.Property<int>("Quantidade");

                    b.Property<long>("ServicoId");

                    b.Property<long>("TipoEntradaId");

                    b.HasKey("Id");

                    b.HasIndex("EstoqueId");

                    b.HasIndex("ServicoId");

                    b.HasIndex("TipoEntradaId");

                    b.ToTable("MovimentosEstoque");
                });

            modelBuilder.Entity("ZZ_ERP.Domain.Entities.PermissaoTela", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Codigo")
                        .IsRequired();

                    b.Property<bool>("IsActive");

                    b.Property<string>("NomeTela")
                        .IsRequired()
                        .HasMaxLength(2147483647);

                    b.HasKey("Id");

                    b.ToTable("PermissaoTelas");
                });

            modelBuilder.Entity("ZZ_ERP.Domain.Entities.Planta", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Codigo")
                        .IsRequired();

                    b.Property<string>("Documento")
                        .IsRequired();

                    b.Property<string>("Email");

                    b.Property<long>("EnderecoId");

                    b.Property<string>("InscricaoEstadual");

                    b.Property<bool>("IsActive");

                    b.Property<string>("NomeFantasia");

                    b.Property<string>("RazaoSocial")
                        .IsRequired();

                    b.Property<string>("Telefone");

                    b.HasKey("Id");

                    b.HasIndex("EnderecoId");

                    b.ToTable("Plantas");
                });

            modelBuilder.Entity("ZZ_ERP.Domain.Entities.Servico", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("CentroCustoId");

                    b.Property<string>("Codigo")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<bool>("ControlaEstoque");

                    b.Property<string>("DescricaoCompleta");

                    b.Property<string>("DescricaoResumida")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<bool>("IsActive");

                    b.Property<string>("Observacoes");

                    b.Property<long>("TipoServicoId");

                    b.Property<long>("UnidadeMedidaId");

                    b.HasKey("Id");

                    b.HasIndex("CentroCustoId");

                    b.HasIndex("TipoServicoId");

                    b.HasIndex("UnidadeMedidaId");

                    b.ToTable("Servicos");
                });

            modelBuilder.Entity("ZZ_ERP.Domain.Entities.TabelaCusto", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Codigo")
                        .IsRequired();

                    b.Property<DateTime>("DataTabela");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(2147483647);

                    b.Property<bool>("IsActive");

                    b.Property<float>("Preco");

                    b.Property<long>("ServicoId");

                    b.HasKey("Id");

                    b.HasIndex("ServicoId");

                    b.ToTable("TabelasCusto");
                });

            modelBuilder.Entity("ZZ_ERP.Domain.Entities.TipoEntrada", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Codigo")
                        .IsRequired();

                    b.Property<bool>("ControlaEstoque");

                    b.Property<string>("Descricao")
                        .IsRequired();

                    b.Property<bool>("IsActive");

                    b.Property<string>("NomeEntity");

                    b.HasKey("Id");

                    b.ToTable("TipoEntradas");
                });

            modelBuilder.Entity("ZZ_ERP.Domain.Entities.TipoOS", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Codigo")
                        .IsRequired();

                    b.Property<string>("Descricao")
                        .IsRequired();

                    b.Property<bool>("IsActive");

                    b.HasKey("Id");

                    b.ToTable("TiposOS");
                });

            modelBuilder.Entity("ZZ_ERP.Domain.Entities.TipoPermissao", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Codigo")
                        .IsRequired();

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(2147483647);

                    b.Property<bool>("IsActive");

                    b.HasKey("Id");

                    b.ToTable("TipoPermissoes");
                });

            modelBuilder.Entity("ZZ_ERP.Domain.Entities.TipoServico", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Codigo")
                        .IsRequired();

                    b.Property<string>("Descricao")
                        .IsRequired();

                    b.Property<bool>("IsActive");

                    b.HasKey("Id");

                    b.ToTable("TipoServicos");
                });

            modelBuilder.Entity("ZZ_ERP.Domain.Entities.UnidadeMedida", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Codigo")
                        .IsRequired();

                    b.Property<string>("Descricao")
                        .HasMaxLength(2147483647);

                    b.Property<bool>("IsActive");

                    b.Property<string>("Sigla")
                        .IsRequired()
                        .HasMaxLength(5);

                    b.HasKey("Id");

                    b.ToTable("UnidadeMedidas");
                });

            modelBuilder.Entity("ZZ_ERP.Infra.Data.Identity.UserAccount", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<DateTime>("CreateDate");

                    b.Property<DateTime>("DeactivationDate");

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("IsActive");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("ZZ_ERP.Infra.Data.Identity.UserAccount")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("ZZ_ERP.Infra.Data.Identity.UserAccount")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ZZ_ERP.Infra.Data.Identity.UserAccount")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("ZZ_ERP.Infra.Data.Identity.UserAccount")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ZZ_ERP.Domain.Entities.Cliente", b =>
                {
                    b.HasOne("ZZ_ERP.Domain.Entities.Endereco", "Endereco")
                        .WithMany()
                        .HasForeignKey("EnderecoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ZZ_ERP.Domain.Entities.CompraManual", b =>
                {
                    b.HasOne("ZZ_ERP.Domain.Entities.Fornecedor", "Fornecedor")
                        .WithMany()
                        .HasForeignKey("FornecedorId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ZZ_ERP.Domain.Entities.TipoEntrada", "TipoEntrada")
                        .WithMany()
                        .HasForeignKey("TipoEntradaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ZZ_ERP.Domain.Entities.Fornecedor", b =>
                {
                    b.HasOne("ZZ_ERP.Domain.Entities.Endereco", "Endereco")
                        .WithMany()
                        .HasForeignKey("EnderecoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ZZ_ERP.Domain.Entities.Funcionario", b =>
                {
                    b.HasOne("ZZ_ERP.Domain.Entities.Endereco", "Endereco")
                        .WithMany()
                        .HasForeignKey("EnderecoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ZZ_ERP.Domain.Entities.FuncionarioEstoque", b =>
                {
                    b.HasOne("ZZ_ERP.Domain.Entities.Estoque", "Estoque")
                        .WithMany()
                        .HasForeignKey("EstoqueId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ZZ_ERP.Domain.Entities.Funcionario", "Funcionario")
                        .WithMany("FuncionarioEstoques")
                        .HasForeignKey("FuncionarioId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ZZ_ERP.Domain.Entities.ItemCompra", b =>
                {
                    b.HasOne("ZZ_ERP.Domain.Entities.CompraManual", "CompraManual")
                        .WithMany("Itens")
                        .HasForeignKey("CompraManualId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ZZ_ERP.Domain.Entities.Servico", "Servico")
                        .WithMany()
                        .HasForeignKey("ServicoId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ZZ_ERP.Domain.Entities.UnidadeMedida", "Unidade")
                        .WithMany()
                        .HasForeignKey("UnidadeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ZZ_ERP.Domain.Entities.MovimentoEstoque", b =>
                {
                    b.HasOne("ZZ_ERP.Domain.Entities.Estoque", "Estoque")
                        .WithMany()
                        .HasForeignKey("EstoqueId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ZZ_ERP.Domain.Entities.Servico", "Servico")
                        .WithMany()
                        .HasForeignKey("ServicoId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ZZ_ERP.Domain.Entities.TipoEntrada", "TipoEntrada")
                        .WithMany()
                        .HasForeignKey("TipoEntradaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ZZ_ERP.Domain.Entities.Planta", b =>
                {
                    b.HasOne("ZZ_ERP.Domain.Entities.Endereco", "Endereco")
                        .WithMany()
                        .HasForeignKey("EnderecoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ZZ_ERP.Domain.Entities.Servico", b =>
                {
                    b.HasOne("ZZ_ERP.Domain.Entities.CentroCustoSintetico", "CentroCusto")
                        .WithMany()
                        .HasForeignKey("CentroCustoId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ZZ_ERP.Domain.Entities.TipoServico", "TipoServico")
                        .WithMany()
                        .HasForeignKey("TipoServicoId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ZZ_ERP.Domain.Entities.UnidadeMedida", "UnidadeMedida")
                        .WithMany()
                        .HasForeignKey("UnidadeMedidaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ZZ_ERP.Domain.Entities.TabelaCusto", b =>
                {
                    b.HasOne("ZZ_ERP.Domain.Entities.Servico", "Servico")
                        .WithMany("TabelasCusto")
                        .HasForeignKey("ServicoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}