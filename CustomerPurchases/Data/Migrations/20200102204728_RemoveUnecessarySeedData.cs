using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace CustomerPurchases.Migrations
{
    public partial class RemoveUnecessarySeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Purchase",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Purchase",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Purchase",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 3);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "Id", "Name", "StockLevel" },
                values: new object[] { 1, "Hydrogen", 1 });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "Id", "Name", "StockLevel" },
                values: new object[] { 2, "Helium", 0 });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "Id", "Name", "StockLevel" },
                values: new object[] { 3, "Lithium", 300 });

            migrationBuilder.InsertData(
                table: "Purchase",
                columns: new[] { "Id", "AccountId", "AddressId", "OrderStatus", "ProductId", "Qty", "TimeStamp" },
                values: new object[] { 1, 1, 1, "Created", 1, 1, new DateTime(2020, 1, 2, 15, 31, 36, 413, DateTimeKind.Local).AddTicks(886) });

            migrationBuilder.InsertData(
                table: "Purchase",
                columns: new[] { "Id", "AccountId", "AddressId", "OrderStatus", "ProductId", "Qty", "TimeStamp" },
                values: new object[] { 2, 1, 3, "Completed", 1, 4, new DateTime(2020, 1, 2, 15, 31, 36, 415, DateTimeKind.Local).AddTicks(8955) });

            migrationBuilder.InsertData(
                table: "Purchase",
                columns: new[] { "Id", "AccountId", "AddressId", "OrderStatus", "ProductId", "Qty", "TimeStamp" },
                values: new object[] { 3, 3, 2, "Shipped", 3, 2, new DateTime(2020, 1, 2, 15, 31, 36, 415, DateTimeKind.Local).AddTicks(8970) });
        }
    }
}