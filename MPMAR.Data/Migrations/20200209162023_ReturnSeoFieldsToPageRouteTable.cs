using Microsoft.EntityFrameworkCore.Migrations;

namespace MPMAR.Data.Migrations
{
    public partial class ReturnSeoFieldsToPageRouteTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "SeoDescriptionAR",
                table: "PageRouteVersions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SeoDescriptionEN",
                table: "PageRouteVersions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SeoOgTitleAR",
                table: "PageRouteVersions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SeoOgTitleEN",
                table: "PageRouteVersions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SeoTitleAR",
                table: "PageRouteVersions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SeoTitleEN",
                table: "PageRouteVersions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SeoTwitterCardAR",
                table: "PageRouteVersions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SeoTwitterCardEN",
                table: "PageRouteVersions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SeoDescriptionAR",
                table: "PageRoutes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SeoDescriptionEN",
                table: "PageRoutes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SeoOgTitleAR",
                table: "PageRoutes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SeoOgTitleEN",
                table: "PageRoutes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SeoTitleAR",
                table: "PageRoutes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SeoTitleEN",
                table: "PageRoutes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SeoTwitterCardAR",
                table: "PageRoutes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SeoTwitterCardEN",
                table: "PageRoutes",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SeoDescriptionAR",
                table: "PageRouteVersions");

            migrationBuilder.DropColumn(
                name: "SeoDescriptionEN",
                table: "PageRouteVersions");

            migrationBuilder.DropColumn(
                name: "SeoOgTitleAR",
                table: "PageRouteVersions");

            migrationBuilder.DropColumn(
                name: "SeoOgTitleEN",
                table: "PageRouteVersions");

            migrationBuilder.DropColumn(
                name: "SeoTitleAR",
                table: "PageRouteVersions");

            migrationBuilder.DropColumn(
                name: "SeoTitleEN",
                table: "PageRouteVersions");

            migrationBuilder.DropColumn(
                name: "SeoTwitterCardAR",
                table: "PageRouteVersions");

            migrationBuilder.DropColumn(
                name: "SeoTwitterCardEN",
                table: "PageRouteVersions");

            migrationBuilder.DropColumn(
                name: "SeoDescriptionAR",
                table: "PageRoutes");

            migrationBuilder.DropColumn(
                name: "SeoDescriptionEN",
                table: "PageRoutes");

            migrationBuilder.DropColumn(
                name: "SeoOgTitleAR",
                table: "PageRoutes");

            migrationBuilder.DropColumn(
                name: "SeoOgTitleEN",
                table: "PageRoutes");

            migrationBuilder.DropColumn(
                name: "SeoTitleAR",
                table: "PageRoutes");

            migrationBuilder.DropColumn(
                name: "SeoTitleEN",
                table: "PageRoutes");

            migrationBuilder.DropColumn(
                name: "SeoTwitterCardAR",
                table: "PageRoutes");

            migrationBuilder.DropColumn(
                name: "SeoTwitterCardEN",
                table: "PageRoutes");

            migrationBuilder.AddColumn<string>(
                name: "SeoDescriptionAR",
                table: "DynamicPageContentVersions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SeoDescriptionEN",
                table: "DynamicPageContentVersions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SeoOgTitleAR",
                table: "DynamicPageContentVersions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SeoOgTitleEN",
                table: "DynamicPageContentVersions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SeoTitleAR",
                table: "DynamicPageContentVersions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SeoTitleEN",
                table: "DynamicPageContentVersions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SeoTwitterCardAR",
                table: "DynamicPageContentVersions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SeoTwitterCardEN",
                table: "DynamicPageContentVersions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SeoDescriptionAR",
                table: "DynamicPageContents",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SeoDescriptionEN",
                table: "DynamicPageContents",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SeoOgTitleAR",
                table: "DynamicPageContents",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SeoOgTitleEN",
                table: "DynamicPageContents",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SeoTitleAR",
                table: "DynamicPageContents",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SeoTitleEN",
                table: "DynamicPageContents",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SeoTwitterCardAR",
                table: "DynamicPageContents",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SeoTwitterCardEN",
                table: "DynamicPageContents",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
