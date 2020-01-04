using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace CustomerPurchases.Migrations
{
    public partial class TrimmedModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Purchase_Account_AccountId",
                table: "Purchase");

            migrationBuilder.DropTable(
                name: "CustomerAddress");

            migrationBuilder.DropTable(
                name: "CustomerTel");

            migrationBuilder.DropTable(
                name: "Account");

            migrationBuilder.DropIndex(
                name: "IX_Purchase_AccountId",
                table: "Purchase");

            migrationBuilder.UpdateData(
                table: "Purchase",
                keyColumn: "Id",
                keyValue: 1,
                column: "TimeStamp",
                value: new DateTime(2020, 1, 2, 15, 31, 36, 413, DateTimeKind.Local).AddTicks(886));

            migrationBuilder.UpdateData(
                table: "Purchase",
                keyColumn: "Id",
                keyValue: 2,
                column: "TimeStamp",
                value: new DateTime(2020, 1, 2, 15, 31, 36, 415, DateTimeKind.Local).AddTicks(8955));

            migrationBuilder.UpdateData(
                table: "Purchase",
                keyColumn: "Id",
                keyValue: 3,
                column: "TimeStamp",
                value: new DateTime(2020, 1, 2, 15, 31, 36, 415, DateTimeKind.Local).AddTicks(8970));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsStaff = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CustomerAddress",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AccountId = table.Column<int>(nullable: false),
                    Address = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerAddress", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerAddress_Account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomerTel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AccountId = table.Column<int>(nullable: false),
                    TelNo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerTel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerTel_Account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Account",
                columns: new[] { "Id", "IsStaff" },
                values: new object[] { 1, false });

            migrationBuilder.InsertData(
                table: "Account",
                columns: new[] { "Id", "IsStaff" },
                values: new object[] { 2, true });

            migrationBuilder.InsertData(
                table: "Account",
                columns: new[] { "Id", "IsStaff" },
                values: new object[] { 3, false });

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

            migrationBuilder.UpdateData(
                table: "Purchase",
                keyColumn: "Id",
                keyValue: 1,
                column: "TimeStamp",
                value: new DateTime(2019, 12, 18, 20, 24, 47, 563, DateTimeKind.Local).AddTicks(2965));

            migrationBuilder.UpdateData(
                table: "Purchase",
                keyColumn: "Id",
                keyValue: 2,
                column: "TimeStamp",
                value: new DateTime(2019, 12, 18, 20, 24, 47, 566, DateTimeKind.Local).AddTicks(9532));

            migrationBuilder.UpdateData(
                table: "Purchase",
                keyColumn: "Id",
                keyValue: 3,
                column: "TimeStamp",
                value: new DateTime(2019, 12, 18, 20, 24, 47, 566, DateTimeKind.Local).AddTicks(9549));

            migrationBuilder.CreateIndex(
                name: "IX_Purchase_AccountId",
                table: "Purchase",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerAddress_AccountId",
                table: "CustomerAddress",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerTel_AccountId",
                table: "CustomerTel",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Purchase_Account_AccountId",
                table: "Purchase",
                column: "AccountId",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}