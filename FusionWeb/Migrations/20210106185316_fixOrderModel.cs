using Microsoft.EntityFrameworkCore.Migrations;

namespace FusionWeb.Migrations
{
    public partial class fixOrderModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreditOwnerName",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "cardNumber",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "cvv",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "expiryMonth",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "expiryYear",
                table: "Order");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreditOwnerName",
                table: "Order",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "cardNumber",
                table: "Order",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "cvv",
                table: "Order",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "expiryMonth",
                table: "Order",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "expiryYear",
                table: "Order",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
