using Microsoft.EntityFrameworkCore.Migrations;

namespace CustomerPurchases.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Account",
                columns: new[] { "Id", "IsStaff" },
                values: new object[,]
                {
                    { 1, false },
                    { 2, true },
                    { 3, false }
                });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "Id", "Name", "StockLevel" },
                values: new object[,]
                {
                    { 1, "Hydrogen", 1 },
                    { 2, "Helium", 0 },
                    { 3, "Lithium", 300 }
                });

            migrationBuilder.InsertData(
                table: "CustomerAddress",
                columns: new[] { "Id", "AccountId", "Address" },
                values: new object[,]
                {
                    { 1, 1, "This is an address" },
                    { 3, 1, "This is also an address" },
                    { 2, 2, "This is an address" }
                });

            migrationBuilder.InsertData(
                table: "CustomerTel",
                columns: new[] { "Id", "AccountId", "TelNo" },
                values: new object[,]
                {
                    { 1, 1, "This is a number" },
                    { 3, 1, "This is also a number" },
                    { 2, 3, "This is a number" }
                });

            migrationBuilder.InsertData(
                table: "Purchase",
                columns: new[] { "Id", "AccountId", "AddressId", "OrderStatus", "ProductId", "Qty" },
                values: new object[,]
                {
                    { 1, 1, 1, "Placed", 1, 1 },
                    { 2, 1, 3, "Complete", 1, 4 },
                    { 3, 3, 2, "In Progress", 3, 2 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CustomerAddress",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "CustomerAddress",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "CustomerAddress",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "CustomerTel",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "CustomerTel",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "CustomerTel",
                keyColumn: "Id",
                keyValue: 3);

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
                table: "Account",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Account",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Account",
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
    }
}
