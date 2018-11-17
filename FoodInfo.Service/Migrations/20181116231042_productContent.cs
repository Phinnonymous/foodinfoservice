using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodInfo.Service.Migrations
{
    public partial class productContent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NutritionFacts_ProductContents_ProductContentID",
                table: "NutritionFacts");

            migrationBuilder.DropIndex(
                name: "IX_NutritionFacts_ProductContentID",
                table: "NutritionFacts");

            migrationBuilder.DropColumn(
                name: "ProductContentID",
                table: "NutritionFacts");

            migrationBuilder.AddColumn<bool>(
                name: "IsAdmin",
                table: "User",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsModerator",
                table: "User",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "NutritionFactID",
                table: "ProductContents",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "ID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2018, 11, 17, 0, 10, 42, 159, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "ID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2018, 11, 17, 0, 10, 42, 161, DateTimeKind.Local));

            migrationBuilder.CreateIndex(
                name: "IX_ProductContents_NutritionFactID",
                table: "ProductContents",
                column: "NutritionFactID");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductContents_NutritionFacts_NutritionFactID",
                table: "ProductContents",
                column: "NutritionFactID",
                principalTable: "NutritionFacts",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductContents_NutritionFacts_NutritionFactID",
                table: "ProductContents");

            migrationBuilder.DropIndex(
                name: "IX_ProductContents_NutritionFactID",
                table: "ProductContents");

            migrationBuilder.DropColumn(
                name: "IsAdmin",
                table: "User");

            migrationBuilder.DropColumn(
                name: "IsModerator",
                table: "User");

            migrationBuilder.DropColumn(
                name: "NutritionFactID",
                table: "ProductContents");

            migrationBuilder.AddColumn<int>(
                name: "ProductContentID",
                table: "NutritionFacts",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "ID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2018, 11, 16, 18, 2, 46, 280, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "ID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2018, 11, 16, 18, 2, 46, 282, DateTimeKind.Local));

            migrationBuilder.CreateIndex(
                name: "IX_NutritionFacts_ProductContentID",
                table: "NutritionFacts",
                column: "ProductContentID");

            migrationBuilder.AddForeignKey(
                name: "FK_NutritionFacts_ProductContents_ProductContentID",
                table: "NutritionFacts",
                column: "ProductContentID",
                principalTable: "ProductContents",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
