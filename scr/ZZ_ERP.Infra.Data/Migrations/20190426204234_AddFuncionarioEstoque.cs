using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ZZ_ERP.Infra.Data.Migrations
{
    public partial class AddFuncionarioEstoque : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ControlaEstoque",
                table: "TipoEntradas",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "FuncionarioEstoques",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Codigo = table.Column<string>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    FuncionarioId = table.Column<long>(nullable: false),
                    EstoqueId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FuncionarioEstoques", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FuncionarioEstoques_Estoques_EstoqueId",
                        column: x => x.EstoqueId,
                        principalTable: "Estoques",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FuncionarioEstoques_Funcionarios_FuncionarioId",
                        column: x => x.FuncionarioId,
                        principalTable: "Funcionarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FuncionarioEstoques_EstoqueId",
                table: "FuncionarioEstoques",
                column: "EstoqueId");

            migrationBuilder.CreateIndex(
                name: "IX_FuncionarioEstoques_FuncionarioId",
                table: "FuncionarioEstoques",
                column: "FuncionarioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FuncionarioEstoques");

            migrationBuilder.DropColumn(
                name: "ControlaEstoque",
                table: "TipoEntradas");
        }
    }
}
