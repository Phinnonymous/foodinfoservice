using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodInfo.Service.Migrations
{
    public partial class for_database : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "ID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2018, 10, 23, 13, 43, 40, 431, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "ID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2018, 10, 23, 13, 43, 40, 432, DateTimeKind.Local));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "ID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2018, 10, 22, 11, 9, 7, 843, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "ID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2018, 10, 22, 11, 9, 7, 844, DateTimeKind.Local));
        }
    }
}
