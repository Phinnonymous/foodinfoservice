using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodInfo.Service.Migrations
{
    public partial class initusersChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "CreatedDate", "Password" },
                values: new object[] { new DateTime(2018, 11, 17, 10, 40, 53, 317, DateTimeKind.Local), "b41af4c157c87c6c8278ec45127c235fb5c991288e6a07da88b87549076acf02" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "ID",
                keyValue: 2,
                columns: new[] { "CreatedDate", "Password" },
                values: new object[] { new DateTime(2018, 11, 17, 10, 40, 53, 325, DateTimeKind.Local), "b41af4c157c87c6c8278ec45127c235fb5c991288e6a07da88b87549076acf02" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "CreatedDate", "Password" },
                values: new object[] { new DateTime(2018, 11, 17, 0, 35, 42, 111, DateTimeKind.Local), "123" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "ID",
                keyValue: 2,
                columns: new[] { "CreatedDate", "Password" },
                values: new object[] { new DateTime(2018, 11, 17, 0, 35, 42, 113, DateTimeKind.Local), "123" });
        }
    }
}
