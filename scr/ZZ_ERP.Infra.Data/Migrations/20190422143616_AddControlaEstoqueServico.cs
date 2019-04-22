using Microsoft.EntityFrameworkCore.Migrations;

namespace ZZ_ERP.Infra.Data.Migrations
{
    public partial class AddControlaEstoqueServico : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ControlaEstoque",
                table: "Servicos",
                nullable: false,
                defaultValue: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ControlaEstoque",
                table: "Servicos");
        }
    }
}
