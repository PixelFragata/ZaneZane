using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ZZ_ERP.Infra.Data.Migrations
{
    public partial class AddUserPlanta : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "PlantaId",
                table: "Plantas",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UserPlantas",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Codigo = table.Column<string>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    UserId = table.Column<string>(maxLength: 450, nullable: false),
                    PlantaId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPlantas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserPlantas_Plantas_PlantaId",
                        column: x => x.PlantaId,
                        principalTable: "Plantas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_UserPlantas_Users_UseraId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Plantas_PlantaId",
                table: "Plantas",
                column: "PlantaId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPlantas_UserId",
                table: "UserPlantas",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPlantas_PlantaId",
                table: "UserPlantas",
                column: "PlantaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Plantas_Plantas_PlantaId",
                table: "UserPlantas",
                column: "PlantaId",
                principalTable: "Plantas",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Users_PlantaId",
                table: "UserPlantas",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Plantas_Plantas_PlantaId",
                table: "Plantas");

            migrationBuilder.DropTable(
                name: "UserPlantas");

            migrationBuilder.DropIndex(
                name: "IX_Plantas_PlantaId",
                table: "Plantas");

            migrationBuilder.DropColumn(
                name: "PlantaId",
                table: "Plantas");
        }
    }
}
