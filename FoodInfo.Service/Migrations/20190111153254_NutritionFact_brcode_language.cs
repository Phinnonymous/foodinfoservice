using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodInfo.Service.Migrations
{
    public partial class NutritionFact_brcode_language : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BarcodeId",
                table: "NutritionFacts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LanguageCode",
                table: "NutritionFacts",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "ID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2019, 1, 11, 16, 32, 54, 41, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "ID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2019, 1, 11, 16, 32, 54, 44, DateTimeKind.Local));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BarcodeId",
                table: "NutritionFacts");

            migrationBuilder.DropColumn(
                name: "LanguageCode",
                table: "NutritionFacts");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "ID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2019, 1, 3, 0, 24, 11, 620, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "ID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2019, 1, 3, 0, 24, 11, 623, DateTimeKind.Local));
        }
    }
}
