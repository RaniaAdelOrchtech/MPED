using Microsoft.EntityFrameworkCore.Migrations;

namespace MPMAR.Data.Migrations
{
    public partial class AddEngDynamicPathMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PageFilePath",
                table: "PageRouteVersions");

            migrationBuilder.AddColumn<string>(
                name: "PageFilePathAr",
                table: "PageRouteVersions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PageFilePathEn",
                table: "PageRouteVersions",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PageFilePathAr",
                table: "PageRouteVersions");

            migrationBuilder.DropColumn(
                name: "PageFilePathEn",
                table: "PageRouteVersions");

            migrationBuilder.AddColumn<string>(
                name: "PageFilePath",
                table: "PageRouteVersions",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
