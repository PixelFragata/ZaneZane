using Microsoft.EntityFrameworkCore.Migrations;

namespace ZZ_ERP.Infra.Data.Migrations
{
    public partial class AlterandoNomeColuna : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DescricaoOS",
                table: "TiposOS",
                newName: "Descricao");

            migrationBuilder.RenameColumn(
                name: "DescricaoServico",
                table: "TipoServicos",
                newName: "Descricao");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Descricao",
                table: "TiposOS",
                newName: "DescricaoOS");

            migrationBuilder.RenameColumn(
                name: "Descricao",
                table: "TipoServicos",
                newName: "DescricaoServico");
        }
    }
}
