using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodInfo.Service.Migrations
{
    public partial class Initial_third : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductCategories_Products_ProductID",
                table: "ProductCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductLanguages_ProductLanguageID",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_ProductCategories_ProductID",
                table: "ProductCategories");

            migrationBuilder.DropColumn(
                name: "ProductID",
                table: "ProductCategories");

            migrationBuilder.RenameColumn(
                name: "ProductLanguageID",
                table: "Products",
                newName: "ProductCategoryID");

            migrationBuilder.RenameIndex(
                name: "IX_Products_ProductLanguageID",
                table: "Products",
                newName: "IX_Products_ProductCategoryID");

            migrationBuilder.AddColumn<int>(
                name: "ProductID",
                table: "ProductLanguages",
                nullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_ProductLanguages_ProductID",
                table: "ProductLanguages",
                column: "ProductID");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductLanguages_Products_ProductID",
                table: "ProductLanguages",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductCategories_ProductCategoryID",
                table: "Products",
                column: "ProductCategoryID",
                principalTable: "ProductCategories",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductLanguages_Products_ProductID",
                table: "ProductLanguages");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductCategories_ProductCategoryID",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_ProductLanguages_ProductID",
                table: "ProductLanguages");

            migrationBuilder.DropColumn(
                name: "ProductID",
                table: "ProductLanguages");

            migrationBuilder.RenameColumn(
                name: "ProductCategoryID",
                table: "Products",
                newName: "ProductLanguageID");

            migrationBuilder.RenameIndex(
                name: "IX_Products_ProductCategoryID",
                table: "Products",
                newName: "IX_Products_ProductLanguageID");

            migrationBuilder.AddColumn<int>(
                name: "ProductID",
                table: "ProductCategories",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "ID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2018, 10, 22, 10, 55, 28, 756, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "ID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2018, 10, 22, 10, 55, 28, 757, DateTimeKind.Local));

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategories_ProductID",
                table: "ProductCategories",
                column: "ProductID");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCategories_Products_ProductID",
                table: "ProductCategories",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductLanguages_ProductLanguageID",
                table: "Products",
                column: "ProductLanguageID",
                principalTable: "ProductLanguages",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
