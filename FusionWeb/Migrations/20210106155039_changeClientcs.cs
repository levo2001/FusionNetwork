using Microsoft.EntityFrameworkCore.Migrations;

namespace FusionWeb.Migrations
{
    public partial class changeClientcs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "CreditOwnerName",
                table: "Order",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "cardNumber",
                table: "Order",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "cvv",
                table: "Order",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "expiryMonth",
                table: "Order",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "expiryYear",
                table: "Order",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Client",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<int>(
                name: "CreditId",
                table: "Order",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Client",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Credit",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CVV = table.Column<int>(type: "int", nullable: false),
                    Month = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Number = table.Column<int>(type: "int", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false)
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
    }
}
