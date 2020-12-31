using Microsoft.EntityFrameworkCore.Migrations;

namespace FusionWeb.Migrations
{
    public partial class Order : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreditId",
                table: "Order",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Credit",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Number = table.Column<int>(nullable: false),
                    Month = table.Column<int>(nullable: false),
                    Year = table.Column<int>(nullable: false),
                    CVV = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Credit", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Order_CreditId",
                table: "Order",
                column: "CreditId");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Credit_CreditId",
                table: "Order",
                column: "CreditId",
                principalTable: "Credit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Credit_CreditId",
                table: "Order");

            migrationBuilder.DropTable(
                name: "Credit");

            migrationBuilder.DropIndex(
                name: "IX_Order_CreditId",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "CreditId",
                table: "Order");
        }
    }
}
