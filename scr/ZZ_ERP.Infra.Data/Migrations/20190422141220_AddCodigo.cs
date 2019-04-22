using Microsoft.EntityFrameworkCore.Migrations;

namespace ZZ_ERP.Infra.Data.Migrations
{
    public partial class AddCodigo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Codigo",
                table: "UnidadeMedidas",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Codigo",
                table: "TiposOS",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Codigo",
                table: "TipoServicos",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Codigo",
                table: "TipoPermissoes",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Codigo",
                table: "TabelasCusto",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Codigo",
                table: "PermissaoTelas",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Codigo",
                table: "Estoques",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Codigo",
                table: "Estados",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Codigo",
                table: "Enderecos",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Codigo",
                table: "CondicaoPagamentos",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Codigo",
                table: "Cidades",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Codigo",
                table: "CentrosCustoSintetico",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Codigo",
                table: "UnidadeMedidas");

            migrationBuilder.DropColumn(
                name: "Codigo",
                table: "TiposOS");

            migrationBuilder.DropColumn(
                name: "Codigo",
                table: "TipoServicos");

            migrationBuilder.DropColumn(
                name: "Codigo",
                table: "TipoPermissoes");

            migrationBuilder.DropColumn(
                name: "Codigo",
                table: "TabelasCusto");

            migrationBuilder.DropColumn(
                name: "Codigo",
                table: "PermissaoTelas");

            migrationBuilder.DropColumn(
                name: "Codigo",
                table: "Estoques");

            migrationBuilder.DropColumn(
                name: "Codigo",
                table: "Estados");

            migrationBuilder.DropColumn(
                name: "Codigo",
                table: "Enderecos");

            migrationBuilder.DropColumn(
                name: "Codigo",
                table: "CondicaoPagamentos");

            migrationBuilder.DropColumn(
                name: "Codigo",
                table: "Cidades");

            migrationBuilder.DropColumn(
                name: "Codigo",
                table: "CentrosCustoSintetico");
        }
    }
}
