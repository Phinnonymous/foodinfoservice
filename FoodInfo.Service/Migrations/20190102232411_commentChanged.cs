using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodInfo.Service.Migrations
{
    public partial class commentChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Products_ProductID",
                table: "Comments");

            migrationBuilder.RenameColumn(
                name: "ProductID",
                table: "Comments",
                newName: "ProductContentID");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_ProductID",
                table: "Comments",
                newName: "IX_Comments_ProductContentID");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_ProductContents_ProductContentID",
                table: "Comments",
                column: "ProductContentID",
                principalTable: "ProductContents",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_ProductContents_ProductContentID",
                table: "Comments");

            migrationBuilder.RenameColumn(
                name: "ProductContentID",
                table: "Comments",
                newName: "ProductID");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_ProductContentID",
                table: "Comments",
                newName: "IX_Comments_ProductID");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "ID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2018, 12, 7, 14, 14, 7, 690, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "ID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2018, 12, 7, 14, 14, 7, 692, DateTimeKind.Local));

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Products_ProductID",
                table: "Comments",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
