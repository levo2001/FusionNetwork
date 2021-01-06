using Microsoft.EntityFrameworkCore.Migrations;

namespace FusionWeb.Migrations
{
    public partial class fixDishOrders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DishOrder_Client_ClientId",
                table: "DishOrder");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DishOrder",
                table: "DishOrder");

            migrationBuilder.DropIndex(
                name: "IX_DishOrder_ClientId",
                table: "DishOrder");

            migrationBuilder.DropIndex(
                name: "IX_DishOrder_DishId",
                table: "DishOrder");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "DishOrder");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "DishOrder");

            migrationBuilder.DropColumn(
                name: "Comment",
                table: "DishOrder");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DishOrder",
                table: "DishOrder",
                columns: new[] { "DishId", "OrderId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_DishOrder",
                table: "DishOrder");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "DishOrder",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "ClientId",
                table: "DishOrder",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Comment",
                table: "DishOrder",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_DishOrder",
                table: "DishOrder",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_DishOrder_ClientId",
                table: "DishOrder",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_DishOrder_DishId",
                table: "DishOrder",
                column: "DishId");

            migrationBuilder.AddForeignKey(
                name: "FK_DishOrder_Client_ClientId",
                table: "DishOrder",
                column: "ClientId",
                principalTable: "Client",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
