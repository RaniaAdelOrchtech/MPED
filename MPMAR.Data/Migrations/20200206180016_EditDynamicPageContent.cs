using Microsoft.EntityFrameworkCore.Migrations;

namespace MPMAR.Data.Migrations
{
    public partial class EditDynamicPageContent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SeoDescriptionAR",
                table: "DynamicPageContentVersions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SeoDescriptionEN",
                table: "DynamicPageContentVersions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SeoOgTitleAR",
                table: "DynamicPageContentVersions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SeoOgTitleEN",
                table: "DynamicPageContentVersions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SeoTitleAR",
                table: "DynamicPageContentVersions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SeoTitleEN",
                table: "DynamicPageContentVersions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SeoTwitterCardAR",
                table: "DynamicPageContentVersions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SeoTwitterCardEN",
                table: "DynamicPageContentVersions",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SeoDescriptionAR",
                table: "DynamicPageContentVersions");

            migrationBuilder.DropColumn(
                name: "SeoDescriptionEN",
                table: "DynamicPageContentVersions");

            migrationBuilder.DropColumn(
                name: "SeoOgTitleAR",
                table: "DynamicPageContentVersions");

            migrationBuilder.DropColumn(
                name: "SeoOgTitleEN",
                table: "DynamicPageContentVersions");

            migrationBuilder.DropColumn(
                name: "SeoTitleAR",
                table: "DynamicPageContentVersions");

            migrationBuilder.DropColumn(
                name: "SeoTitleEN",
                table: "DynamicPageContentVersions");

            migrationBuilder.DropColumn(
                name: "SeoTwitterCardAR",
                table: "DynamicPageContentVersions");

            migrationBuilder.DropColumn(
                name: "SeoTwitterCardEN",
                table: "DynamicPageContentVersions");
        }
    }
}
