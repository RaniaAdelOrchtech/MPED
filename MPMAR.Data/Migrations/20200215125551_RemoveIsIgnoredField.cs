using Microsoft.EntityFrameworkCore.Migrations;

namespace MPMAR.Data.Migrations
{
    public partial class RemoveIsIgnoredField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsIgnored",
                table: "PageSectionVersions");

            migrationBuilder.DropColumn(
                name: "IsIgnored",
                table: "PageRouteVersions");

            migrationBuilder.DropColumn(
                name: "IsIgnored",
                table: "NavItemVersions");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsIgnored",
                table: "PageSectionVersions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsIgnored",
                table: "PageRouteVersions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsIgnored",
                table: "NavItemVersions",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
