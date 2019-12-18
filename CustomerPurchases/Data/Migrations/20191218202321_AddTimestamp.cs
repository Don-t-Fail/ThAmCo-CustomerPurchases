using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CustomerPurchases.Migrations
{
    public partial class AddTimestamp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "TimeStamp",
                table: "Purchase",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Purchase",
                keyColumn: "Id",
                keyValue: 1,
                column: "TimeStamp",
                value: new DateTime(2019, 12, 18, 20, 23, 20, 955, DateTimeKind.Local).AddTicks(7761));

            migrationBuilder.UpdateData(
                table: "Purchase",
                keyColumn: "Id",
                keyValue: 2,
                column: "TimeStamp",
                value: new DateTime(2019, 12, 18, 20, 23, 20, 959, DateTimeKind.Local).AddTicks(6240));

            migrationBuilder.UpdateData(
                table: "Purchase",
                keyColumn: "Id",
                keyValue: 3,
                column: "TimeStamp",
                value: new DateTime(2019, 12, 18, 20, 23, 20, 959, DateTimeKind.Local).AddTicks(6257));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeStamp",
                table: "Purchase");
        }
    }
}
