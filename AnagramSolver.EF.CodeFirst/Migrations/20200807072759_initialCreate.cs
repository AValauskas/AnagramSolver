using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AnagramSolver.EF.CodeFirst.Migrations
{
    public partial class initialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CachedWords",
                columns: table => new
                {
                    CachedWordId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Word = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CachedWords", x => x.CachedWordId);
                });

            migrationBuilder.CreateTable(
                name: "UserLogs",
                columns: table => new
                {
                    UserLogId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserIp = table.Column<string>(nullable: true),
                    Word = table.Column<string>(nullable: true),
                    Time = table.Column<DateTime>(nullable: false),
                    Anagrams = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogs", x => x.UserLogId);
                });

            migrationBuilder.CreateTable(
                name: "Words",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Word = table.Column<string>(nullable: true),
                    LanguagePart = table.Column<string>(nullable: true),
                    SortedWord = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Words", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CachedWord_WordEntityes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WordId = table.Column<int>(nullable: true),
                    CachedWordId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CachedWord_WordEntityes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CachedWord_WordEntityes_CachedWords_CachedWordId",
                        column: x => x.CachedWordId,
                        principalTable: "CachedWords",
                        principalColumn: "CachedWordId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CachedWord_WordEntityes_Words_WordId",
                        column: x => x.WordId,
                        principalTable: "Words",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CachedWord_WordEntityes_CachedWordId",
                table: "CachedWord_WordEntityes",
                column: "CachedWordId");

            migrationBuilder.CreateIndex(
                name: "IX_CachedWord_WordEntityes_WordId",
                table: "CachedWord_WordEntityes",
                column: "WordId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CachedWord_WordEntityes");

            migrationBuilder.DropTable(
                name: "UserLogs");

            migrationBuilder.DropTable(
                name: "CachedWords");

            migrationBuilder.DropTable(
                name: "Words");
        }
    }
}
