using Microsoft.EntityFrameworkCore.Migrations;

namespace FusionWeb.Migrations
{
    public partial class foo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClientId",
                table: "DishOrder",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DishOrder_ClientId",
                table: "DishOrder",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_DishOrder_Client_ClientId",
                table: "DishOrder",
                column: "ClientId",
                principalTable: "Client",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DishOrder_Client_ClientId",
                table: "DishOrder");

            migrationBuilder.DropIndex(
                name: "IX_DishOrder_ClientId",
                table: "DishOrder");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "DishOrder");
        }
    }
}
