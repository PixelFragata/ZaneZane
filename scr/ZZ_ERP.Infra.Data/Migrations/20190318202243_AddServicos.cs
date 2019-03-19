using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ZZ_ERP.Infra.Data.Migrations
{
    public partial class AddServicos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ServicoId",
                table: "TabelasCusto",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "Servicos",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsActive = table.Column<bool>(nullable: false),
                    Codigo = table.Column<string>(maxLength: 20, nullable: false),
                    DescricaoCompleta = table.Column<string>(maxLength: 2147483647, nullable: true),
                    DescricaoResumida = table.Column<string>(maxLength: 200, nullable: false),
                    Observacoes = table.Column<string>(maxLength: 2147483647, nullable: true),
                    TipoServicoId = table.Column<long>(nullable: false),
                    UnidadeMedidaId = table.Column<long>(nullable: false),
                    CentroCustoId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servicos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Servicos_CentrosCustoSintetico_CentroCustoId",
                        column: x => x.CentroCustoId,
                        principalTable: "CentrosCustoSintetico",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Servicos_TipoServicos_TipoServicoId",
                        column: x => x.TipoServicoId,
                        principalTable: "TipoServicos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Servicos_UnidadeMedidas_UnidadeMedidaId",
                        column: x => x.UnidadeMedidaId,
                        principalTable: "UnidadeMedidas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TabelasCusto_ServicoId",
                table: "TabelasCusto",
                column: "ServicoId");

            migrationBuilder.CreateIndex(
                name: "IX_Servicos_CentroCustoId",
                table: "Servicos",
                column: "CentroCustoId");

            migrationBuilder.CreateIndex(
                name: "IX_Servicos_TipoServicoId",
                table: "Servicos",
                column: "TipoServicoId");

            migrationBuilder.CreateIndex(
                name: "IX_Servicos_UnidadeMedidaId",
                table: "Servicos",
                column: "UnidadeMedidaId");

            migrationBuilder.AddForeignKey(
                name: "FK_TabelasCusto_Servicos_ServicoId",
                table: "TabelasCusto",
                column: "ServicoId",
                principalTable: "Servicos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TabelasCusto_Servicos_ServicoId",
                table: "TabelasCusto");

            migrationBuilder.DropTable(
                name: "Servicos");

            migrationBuilder.DropIndex(
                name: "IX_TabelasCusto_ServicoId",
                table: "TabelasCusto");

            migrationBuilder.DropColumn(
                name: "ServicoId",
                table: "TabelasCusto");
        }
    }
}
