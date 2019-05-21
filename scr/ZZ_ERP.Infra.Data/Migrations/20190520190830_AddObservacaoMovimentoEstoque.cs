using Microsoft.EntityFrameworkCore.Migrations;

namespace ZZ_ERP.Infra.Data.Migrations
{
    public partial class AddObservacaoMovimentoEstoque : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Observacao",
                table: "MovimentosEstoque",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Observacao",
                table: "MovimentosEstoque");
        }
    }
}
