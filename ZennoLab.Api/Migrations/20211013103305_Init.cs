using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ZennoLab.Api.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsCyrillic = table.Column<bool>(type: "bit", nullable: false),
                    IsLatin = table.Column<bool>(type: "bit", nullable: false),
                    IsNumbers = table.Column<bool>(type: "bit", nullable: false),
                    IsSpecialChar = table.Column<bool>(type: "bit", nullable: false),
                    CaseSensitivity = table.Column<bool>(type: "bit", nullable: false),
                    LocationAnswer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ArchivePath = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contacts");
        }
    }
}
