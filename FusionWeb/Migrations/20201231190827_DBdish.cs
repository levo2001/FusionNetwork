using Microsoft.EntityFrameworkCore.Migrations;

namespace FusionWeb.Migrations
{
    public partial class DBdish : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "KitchenDish",
                table: "Dish",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "KitchenDish",
                table: "Dish");
        }
    }
}
