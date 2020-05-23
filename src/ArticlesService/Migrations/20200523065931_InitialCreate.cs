using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ArticlesService.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "articles",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    slug = table.Column<string>(nullable: true),
                    title = table.Column<string>(nullable: false),
                    description = table.Column<string>(nullable: true),
                    Body = table.Column<string>(nullable: true),
                    created_at_utc = table.Column<DateTimeOffset>(nullable: false),
                    updated_at_utc = table.Column<DateTimeOffset>(nullable: true),
                    author_id = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_articles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "article_favorites",
                columns: table => new
                {
                    article_id = table.Column<Guid>(nullable: false),
                    user_id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_article_favorites", x => new { x.article_id, x.user_id });
                    table.ForeignKey(
                        name: "FK_article_favorites_articles_article_id",
                        column: x => x.article_id,
                        principalTable: "articles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "comments",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    body = table.Column<string>(nullable: false),
                    created_at_utc = table.Column<DateTimeOffset>(nullable: false),
                    updated_at_utc = table.Column<DateTimeOffset>(nullable: true),
                    author_id = table.Column<string>(nullable: true),
                    article_id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_comments", x => x.id);
                    table.ForeignKey(
                        name: "FK_comments_articles_article_id",
                        column: x => x.article_id,
                        principalTable: "articles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_comments_article_id",
                table: "comments",
                column: "article_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "article_favorites");

            migrationBuilder.DropTable(
                name: "comments");

            migrationBuilder.DropTable(
                name: "articles");
        }
    }
}
