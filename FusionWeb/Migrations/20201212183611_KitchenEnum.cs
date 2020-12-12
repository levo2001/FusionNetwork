using Microsoft.EntityFrameworkCore.Migrations;

namespace FusionWeb.Migrations
{
    public partial class KitchenEnum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservasion_Kitchen_KitchenId",
                table: "Reservasion");

            migrationBuilder.DropTable(
                name: "Kitchen");

            migrationBuilder.DropIndex(
                name: "IX_Reservasion_KitchenId",
                table: "Reservasion");

            migrationBuilder.DropColumn(
                name: "KitchenId",
                table: "Reservasion");

            migrationBuilder.AddColumn<int>(
                name: "Kitchen",
                table: "Reservasion",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Kitchen",
                table: "Reservasion");

            migrationBuilder.AddColumn<int>(
                name: "KitchenId",
                table: "Reservasion",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Kitchen",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AmericanId = table.Column<int>(type: "int", nullable: false),
                    AsianId = table.Column<int>(type: "int", nullable: false),
                    IsraelId = table.Column<int>(type: "int", nullable: false),
                    ItalianId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kitchen", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reservasion_KitchenId",
                table: "Reservasion",
                column: "KitchenId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservasion_Kitchen_KitchenId",
                table: "Reservasion",
                column: "KitchenId",
                principalTable: "Kitchen",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
