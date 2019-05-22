using Microsoft.EntityFrameworkCore.Migrations;

namespace ZZ_ERP.Infra.Data.Migrations
{
    public partial class AddPlantaAtributo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "PlantaId",
                table: "SaldosEstoques",
                nullable: true,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "PlantaId",
                table: "MovimentosEstoque",
                nullable: true,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "PlantaId",
                table: "ItensCompras",
                nullable: true,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "PlantaId",
                table: "Funcionarios",
                nullable: true,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "PlantaId",
                table: "FuncionarioEstoques",
                nullable: true,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "PlantaId",
                table: "Estoques",
                nullable: true,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "PlantaId",
                table: "ComprasManuais",
                nullable: true,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_SaldosEstoques_PlantaId",
                table: "SaldosEstoques",
                column: "PlantaId");

            migrationBuilder.CreateIndex(
                name: "IX_MovimentosEstoque_PlantaId",
                table: "MovimentosEstoque",
                column: "PlantaId");

            migrationBuilder.CreateIndex(
                name: "IX_ItensCompras_PlantaId",
                table: "ItensCompras",
                column: "PlantaId");

            migrationBuilder.CreateIndex(
                name: "IX_Funcionarios_PlantaId",
                table: "Funcionarios",
                column: "PlantaId");

            migrationBuilder.CreateIndex(
                name: "IX_FuncionarioEstoques_PlantaId",
                table: "FuncionarioEstoques",
                column: "PlantaId");

            migrationBuilder.CreateIndex(
                name: "IX_Estoques_PlantaId",
                table: "Estoques",
                column: "PlantaId");

            migrationBuilder.CreateIndex(
                name: "IX_ComprasManuais_PlantaId",
                table: "ComprasManuais",
                column: "PlantaId");

            migrationBuilder.AddForeignKey(
                name: "FK_ComprasManuais_Plantas_PlantaId",
                table: "ComprasManuais",
                column: "PlantaId",
                principalTable: "Plantas",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Estoques_Plantas_PlantaId",
                table: "Estoques",
                column: "PlantaId",
                principalTable: "Plantas",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_FuncionarioEstoques_Plantas_PlantaId",
                table: "FuncionarioEstoques",
                column: "PlantaId",
                principalTable: "Plantas",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Funcionarios_Plantas_PlantaId",
                table: "Funcionarios",
                column: "PlantaId",
                principalTable: "Plantas",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_ItensCompras_Plantas_PlantaId",
                table: "ItensCompras",
                column: "PlantaId",
                principalTable: "Plantas",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_MovimentosEstoque_Plantas_PlantaId",
                table: "MovimentosEstoque",
                column: "PlantaId",
                principalTable: "Plantas",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_SaldosEstoques_Plantas_PlantaId",
                table: "SaldosEstoques",
                column: "PlantaId",
                principalTable: "Plantas",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComprasManuais_Plantas_PlantaId",
                table: "ComprasManuais");

            migrationBuilder.DropForeignKey(
                name: "FK_Estoques_Plantas_PlantaId",
                table: "Estoques");

            migrationBuilder.DropForeignKey(
                name: "FK_FuncionarioEstoques_Plantas_PlantaId",
                table: "FuncionarioEstoques");

            migrationBuilder.DropForeignKey(
                name: "FK_Funcionarios_Plantas_PlantaId",
                table: "Funcionarios");

            migrationBuilder.DropForeignKey(
                name: "FK_ItensCompras_Plantas_PlantaId",
                table: "ItensCompras");

            migrationBuilder.DropForeignKey(
                name: "FK_MovimentosEstoque_Plantas_PlantaId",
                table: "MovimentosEstoque");

            migrationBuilder.DropForeignKey(
                name: "FK_SaldosEstoques_Plantas_PlantaId",
                table: "SaldosEstoques");

            migrationBuilder.DropIndex(
                name: "IX_SaldosEstoques_PlantaId",
                table: "SaldosEstoques");

            migrationBuilder.DropIndex(
                name: "IX_MovimentosEstoque_PlantaId",
                table: "MovimentosEstoque");

            migrationBuilder.DropIndex(
                name: "IX_ItensCompras_PlantaId",
                table: "ItensCompras");

            migrationBuilder.DropIndex(
                name: "IX_Funcionarios_PlantaId",
                table: "Funcionarios");

            migrationBuilder.DropIndex(
                name: "IX_FuncionarioEstoques_PlantaId",
                table: "FuncionarioEstoques");

            migrationBuilder.DropIndex(
                name: "IX_Estoques_PlantaId",
                table: "Estoques");

            migrationBuilder.DropIndex(
                name: "IX_ComprasManuais_PlantaId",
                table: "ComprasManuais");

            migrationBuilder.DropColumn(
                name: "PlantaId",
                table: "SaldosEstoques");

            migrationBuilder.DropColumn(
                name: "PlantaId",
                table: "MovimentosEstoque");

            migrationBuilder.DropColumn(
                name: "PlantaId",
                table: "ItensCompras");

            migrationBuilder.DropColumn(
                name: "PlantaId",
                table: "Funcionarios");

            migrationBuilder.DropColumn(
                name: "PlantaId",
                table: "FuncionarioEstoques");

            migrationBuilder.DropColumn(
                name: "PlantaId",
                table: "Estoques");

            migrationBuilder.DropColumn(
                name: "PlantaId",
                table: "ComprasManuais");
        }
    }
}
