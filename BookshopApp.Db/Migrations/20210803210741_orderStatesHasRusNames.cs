using Microsoft.EntityFrameworkCore.Migrations;

namespace BookshopApp.Migrations
{
    public partial class orderStatesHasRusNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "OrderStates",
                keyColumn: "Id",
                keyValue: 1,
                column: "NameOfState",
                value: "Корзина");

            migrationBuilder.UpdateData(
                table: "OrderStates",
                keyColumn: "Id",
                keyValue: 2,
                column: "NameOfState",
                value: "Подтвержден");

            migrationBuilder.UpdateData(
                table: "OrderStates",
                keyColumn: "Id",
                keyValue: 3,
                column: "NameOfState",
                value: "Завершен");

            migrationBuilder.UpdateData(
                table: "OrderStates",
                keyColumn: "Id",
                keyValue: 4,
                column: "NameOfState",
                value: "Отменен");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "OrderStates",
                keyColumn: "Id",
                keyValue: 1,
                column: "NameOfState",
                value: "IsCart");

            migrationBuilder.UpdateData(
                table: "OrderStates",
                keyColumn: "Id",
                keyValue: 2,
                column: "NameOfState",
                value: "Confirmed");

            migrationBuilder.UpdateData(
                table: "OrderStates",
                keyColumn: "Id",
                keyValue: 3,
                column: "NameOfState",
                value: "Completed");

            migrationBuilder.UpdateData(
                table: "OrderStates",
                keyColumn: "Id",
                keyValue: 4,
                column: "NameOfState",
                value: "Cancelled");
        }
    }
}
