using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodInfo.Service.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedUserId = table.Column<int>(nullable: true),
                    ModifiedUserId = table.Column<int>(nullable: true),
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Surname = table.Column<string>(nullable: true),
                    Username = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.ID);
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "ID", "CreatedDate", "CreatedUserId", "IsDeleted", "ModifiedDate", "ModifiedUserId", "Name", "Password", "Surname", "Username" },
                values: new object[] { 1, new DateTime(2018, 10, 21, 20, 41, 19, 136, DateTimeKind.Local), null, false, null, null, "Fatih", null, "Cankurtaran", null });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "ID", "CreatedDate", "CreatedUserId", "IsDeleted", "ModifiedDate", "ModifiedUserId", "Name", "Password", "Surname", "Username" },
                values: new object[] { 2, new DateTime(2018, 10, 21, 20, 41, 19, 137, DateTimeKind.Local), null, false, null, null, "Yusuf", null, "Kocadas", null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
