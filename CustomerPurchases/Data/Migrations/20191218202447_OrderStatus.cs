using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CustomerPurchases.Migrations
{
    public partial class OrderStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Purchase",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "OrderStatus", "TimeStamp" },
                values: new object[] { "Created", new DateTime(2019, 12, 18, 20, 24, 47, 563, DateTimeKind.Local).AddTicks(2965) });

            migrationBuilder.UpdateData(
                table: "Purchase",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "OrderStatus", "TimeStamp" },
                values: new object[] { "Completed", new DateTime(2019, 12, 18, 20, 24, 47, 566, DateTimeKind.Local).AddTicks(9532) });

            migrationBuilder.UpdateData(
                table: "Purchase",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "OrderStatus", "TimeStamp" },
                values: new object[] { "Shipped", new DateTime(2019, 12, 18, 20, 24, 47, 566, DateTimeKind.Local).AddTicks(9549) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Purchase",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "OrderStatus", "TimeStamp" },
                values: new object[] { "Placed", new DateTime(2019, 12, 18, 20, 23, 20, 955, DateTimeKind.Local).AddTicks(7761) });

            migrationBuilder.UpdateData(
                table: "Purchase",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "OrderStatus", "TimeStamp" },
                values: new object[] { "Complete", new DateTime(2019, 12, 18, 20, 23, 20, 959, DateTimeKind.Local).AddTicks(6240) });

            migrationBuilder.UpdateData(
                table: "Purchase",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "OrderStatus", "TimeStamp" },
                values: new object[] { "In Progress", new DateTime(2019, 12, 18, 20, 23, 20, 959, DateTimeKind.Local).AddTicks(6257) });
        }
    }
}
