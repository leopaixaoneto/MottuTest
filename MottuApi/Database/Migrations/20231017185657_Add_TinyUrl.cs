using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MottuTest.Database.Migrations
{
    /// <inheritdoc />
    public partial class Add_TinyUrl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tinyurl",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Original = table.Column<string>(type: "text", nullable: false),
                    Shortened = table.Column<string>(type: "text", nullable: false),
                    ShortenedCode = table.Column<string>(type: "text", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tinyurl", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tinyurl");
        }
    }
}
