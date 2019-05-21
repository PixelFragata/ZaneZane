using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ZZ_ERP.Infra.Data.Migrations
{
    public partial class AddMovimentoEstoque : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "NomeEntity",
                table: "TipoEntradas",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.CreateTable(
                name: "MovimentosEstoque",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Codigo = table.Column<string>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    IsEntrada = table.Column<bool>(nullable: false),
                    Quantidade = table.Column<int>(nullable: false),
                    Data = table.Column<DateTime>(nullable: false),
                    DataMovimento = table.Column<DateTime>(nullable: false),
                    ServicoId = table.Column<long>(nullable: false),
                    EstoqueId = table.Column<long>(nullable: false),
                    TipoEntradaId = table.Column<long>(nullable: false),
                    DocumentoId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovimentosEstoque", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MovimentosEstoque_Estoques_EstoqueId",
                        column: x => x.EstoqueId,
                        principalTable: "Estoques",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovimentosEstoque_Servicos_ServicoId",
                        column: x => x.ServicoId,
                        principalTable: "Servicos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovimentosEstoque_TipoEntradas_TipoEntradaId",
                        column: x => x.TipoEntradaId,
                        principalTable: "TipoEntradas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MovimentosEstoque_EstoqueId",
                table: "MovimentosEstoque",
                column: "EstoqueId");

            migrationBuilder.CreateIndex(
                name: "IX_MovimentosEstoque_ServicoId",
                table: "MovimentosEstoque",
                column: "ServicoId");

            migrationBuilder.CreateIndex(
                name: "IX_MovimentosEstoque_TipoEntradaId",
                table: "MovimentosEstoque",
                column: "TipoEntradaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MovimentosEstoque");

            migrationBuilder.AlterColumn<string>(
                name: "NomeEntity",
                table: "TipoEntradas",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
