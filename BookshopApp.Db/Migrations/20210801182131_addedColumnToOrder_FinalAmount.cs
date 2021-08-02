using Microsoft.EntityFrameworkCore.Migrations;

namespace BookshopApp.Migrations
{
    public partial class addedColumnToOrder_FinalAmount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "FinalAmount",
                table: "Orders",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.UpdateData(
                table: "OrderStates",
                keyColumn: "Id",
                keyValue: 1,
                column: "NameOfState",
                value: "IsCart");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FinalAmount",
                table: "Orders");

            migrationBuilder.UpdateData(
                table: "OrderStates",
                keyColumn: "Id",
                keyValue: 1,
                column: "NameOfState",
                value: "IsBasket");
        }
    }
}
