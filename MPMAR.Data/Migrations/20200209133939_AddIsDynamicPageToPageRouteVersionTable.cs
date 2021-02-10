using Microsoft.EntityFrameworkCore.Migrations;

namespace MPMAR.Data.Migrations
{
    public partial class AddIsDynamicPageToPageRouteVersionTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDynamicPage",
                table: "PageRouteVersions",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDynamicPage",
                table: "PageRouteVersions");
        }
    }
}
