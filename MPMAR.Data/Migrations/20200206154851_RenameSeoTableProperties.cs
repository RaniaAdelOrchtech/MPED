using Microsoft.EntityFrameworkCore.Migrations;

namespace MPMAR.Data.Migrations
{
    public partial class RenameSeoTableProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description_AR",
                table: "DynamicPageContents");

            migrationBuilder.DropColumn(
                name: "Description_EN",
                table: "DynamicPageContents");

            migrationBuilder.DropColumn(
                name: "Og_Title_AR",
                table: "DynamicPageContents");

            migrationBuilder.DropColumn(
                name: "Og_Title_EN",
                table: "DynamicPageContents");

            migrationBuilder.DropColumn(
                name: "Title_AR",
                table: "DynamicPageContents");

            migrationBuilder.DropColumn(
                name: "Title_EN",
                table: "DynamicPageContents");

            migrationBuilder.DropColumn(
                name: "Twitter_Card_AR",
                table: "DynamicPageContents");

            migrationBuilder.DropColumn(
                name: "Twitter_Card_EN",
                table: "DynamicPageContents");

            migrationBuilder.AddColumn<string>(
                name: "SeoDescriptionAR",
                table: "DynamicPageContents",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SeoDescriptionEN",
                table: "DynamicPageContents",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SeoOgTitleAR",
                table: "DynamicPageContents",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SeoOgTitleEN",
                table: "DynamicPageContents",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SeoTitleAR",
                table: "DynamicPageContents",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SeoTitleEN",
                table: "DynamicPageContents",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SeoTwitterCardAR",
                table: "DynamicPageContents",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SeoTwitterCardEN",
                table: "DynamicPageContents",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SeoDescriptionAR",
                table: "DynamicPageContents");

            migrationBuilder.DropColumn(
                name: "SeoDescriptionEN",
                table: "DynamicPageContents");

            migrationBuilder.DropColumn(
                name: "SeoOgTitleAR",
                table: "DynamicPageContents");

            migrationBuilder.DropColumn(
                name: "SeoOgTitleEN",
                table: "DynamicPageContents");

            migrationBuilder.DropColumn(
                name: "SeoTitleAR",
                table: "DynamicPageContents");

            migrationBuilder.DropColumn(
                name: "SeoTitleEN",
                table: "DynamicPageContents");

            migrationBuilder.DropColumn(
                name: "SeoTwitterCardAR",
                table: "DynamicPageContents");

            migrationBuilder.DropColumn(
                name: "SeoTwitterCardEN",
                table: "DynamicPageContents");

            migrationBuilder.AddColumn<string>(
                name: "Description_AR",
                table: "DynamicPageContents",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description_EN",
                table: "DynamicPageContents",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Og_Title_AR",
                table: "DynamicPageContents",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Og_Title_EN",
                table: "DynamicPageContents",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title_AR",
                table: "DynamicPageContents",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title_EN",
                table: "DynamicPageContents",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Twitter_Card_AR",
                table: "DynamicPageContents",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Twitter_Card_EN",
                table: "DynamicPageContents",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
