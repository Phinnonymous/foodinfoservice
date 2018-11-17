using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodInfo.Service.Migrations
{
    public partial class deletedProductLanguages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductLanguages");

            migrationBuilder.AddColumn<int>(
                name: "LanguageID",
                table: "ProductContents",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "ID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2018, 11, 17, 0, 20, 14, 560, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "ID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2018, 11, 17, 0, 20, 14, 561, DateTimeKind.Local));

            migrationBuilder.CreateIndex(
                name: "IX_ProductContents_LanguageID",
                table: "ProductContents",
                column: "LanguageID");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductContents_Languages_LanguageID",
                table: "ProductContents",
                column: "LanguageID",
                principalTable: "Languages",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductContents_Languages_LanguageID",
                table: "ProductContents");

            migrationBuilder.DropIndex(
                name: "IX_ProductContents_LanguageID",
                table: "ProductContents");

            migrationBuilder.DropColumn(
                name: "LanguageID",
                table: "ProductContents");

            migrationBuilder.CreateTable(
                name: "ProductLanguages",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedUserId = table.Column<int>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LanguageID = table.Column<int>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    ModifiedUserId = table.Column<int>(nullable: true),
                    ProductID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductLanguages", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ProductLanguages_Languages_LanguageID",
                        column: x => x.LanguageID,
                        principalTable: "Languages",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductLanguages_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

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
                name: "IX_ProductLanguages_LanguageID",
                table: "ProductLanguages",
                column: "LanguageID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductLanguages_ProductID",
                table: "ProductLanguages",
                column: "ProductID");
        }
    }
}
