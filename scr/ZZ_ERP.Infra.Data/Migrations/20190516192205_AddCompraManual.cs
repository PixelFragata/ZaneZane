using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ZZ_ERP.Infra.Data.Migrations
{
    public partial class AddCompraManual : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NomeTabela",
                table: "TipoEntradas",
                newName: "NomeEntity");

            migrationBuilder.AddColumn<string>(
                name: "InscricaoEstadual",
                table: "Fornecedores",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InscricaoEstadual",
                table: "Clientes",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ComprasManuais",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Codigo = table.Column<string>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    DataEmissao = table.Column<DateTime>(nullable: false),
                    Observacao = table.Column<string>(nullable: true),
                    ValorTotal = table.Column<float>(nullable: false),
                    ControlaEstoque = table.Column<bool>(nullable: false),
                    MovimentouEstoque = table.Column<bool>(nullable: false),
                    TipoEntradaId = table.Column<long>(nullable: false),
                    FornecedorId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComprasManuais", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComprasManuais_Fornecedores_FornecedorId",
                        column: x => x.FornecedorId,
                        principalTable: "Fornecedores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComprasManuais_TipoEntradas_TipoEntradaId",
                        column: x => x.TipoEntradaId,
                        principalTable: "TipoEntradas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItensCompras",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Codigo = table.Column<string>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    Observacao = table.Column<string>(nullable: true),
                    Quantidade = table.Column<float>(nullable: false),
                    ValorUnitario = table.Column<float>(nullable: false),
                    ValorTotal = table.Column<float>(nullable: false),
                    ServicoId = table.Column<long>(nullable: false),
                    UnidadeId = table.Column<long>(nullable: false),
                    CompraManualId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItensCompras", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItensCompras_ComprasManuais_CompraManualId",
                        column: x => x.CompraManualId,
                        principalTable: "ComprasManuais",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItensCompras_Servicos_ServicoId",
                        column: x => x.ServicoId,
                        principalTable: "Servicos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItensCompras_UnidadeMedidas_UnidadeId",
                        column: x => x.UnidadeId,
                        principalTable: "UnidadeMedidas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ComprasManuais_FornecedorId",
                table: "ComprasManuais",
                column: "FornecedorId");

            migrationBuilder.CreateIndex(
                name: "IX_ComprasManuais_TipoEntradaId",
                table: "ComprasManuais",
                column: "TipoEntradaId");

            migrationBuilder.CreateIndex(
                name: "IX_ItensCompras_CompraManualId",
                table: "ItensCompras",
                column: "CompraManualId");

            migrationBuilder.CreateIndex(
                name: "IX_ItensCompras_ServicoId",
                table: "ItensCompras",
                column: "ServicoId");

            migrationBuilder.CreateIndex(
                name: "IX_ItensCompras_UnidadeId",
                table: "ItensCompras",
                column: "UnidadeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItensCompras");

            migrationBuilder.DropTable(
                name: "ComprasManuais");

            migrationBuilder.DropColumn(
                name: "InscricaoEstadual",
                table: "Fornecedores");

            migrationBuilder.DropColumn(
                name: "InscricaoEstadual",
                table: "Clientes");

            migrationBuilder.RenameColumn(
                name: "NomeEntity",
                table: "TipoEntradas",
                newName: "NomeTabela");
        }
    }
}
