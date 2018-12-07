using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodInfo.Service.Migrations
{
    public partial class nutritionFact : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "NutritionFacts",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CreatedUserId",
                table: "NutritionFacts",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "NutritionFacts",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "NutritionFacts",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ModifiedUserId",
                table: "NutritionFacts",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "ID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2018, 12, 7, 1, 13, 28, 944, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "ID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2018, 12, 7, 1, 13, 28, 946, DateTimeKind.Local));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "NutritionFacts");

            migrationBuilder.DropColumn(
                name: "CreatedUserId",
                table: "NutritionFacts");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "NutritionFacts");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "NutritionFacts");

            migrationBuilder.DropColumn(
                name: "ModifiedUserId",
                table: "NutritionFacts");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "ID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2018, 12, 6, 1, 4, 57, 69, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "ID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2018, 12, 6, 1, 4, 57, 71, DateTimeKind.Local));
        }
    }
}
