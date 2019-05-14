using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ZZ_ERP.Infra.Data.Migrations
{
    public partial class AddPlanta : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Plantas",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Codigo = table.Column<string>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    Documento = table.Column<string>(nullable: false),
                    InscricaoEstadual = table.Column<string>(nullable: true),
                    NomeFantasia = table.Column<string>(nullable: true),
                    RazaoSocial = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    Telefone = table.Column<string>(nullable: true),
                    EnderecoId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plantas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Plantas_Enderecos_EnderecoId",
                        column: x => x.EnderecoId,
                        principalTable: "Enderecos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Plantas_EnderecoId",
                table: "Plantas",
                column: "EnderecoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Plantas");
        }
    }
}
