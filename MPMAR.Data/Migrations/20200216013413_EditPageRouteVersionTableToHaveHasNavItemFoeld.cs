using Microsoft.EntityFrameworkCore.Migrations;

namespace MPMAR.Data.Migrations
{
    public partial class EditPageRouteVersionTableToHaveHasNavItemFoeld : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasNavItem",
                table: "PageRouteVersions",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_PageRouteVersions_NavItemId",
                table: "PageRouteVersions",
                column: "NavItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_PageRouteVersions_NavItems_NavItemId",
                table: "PageRouteVersions",
                column: "NavItemId",
                principalTable: "NavItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PageRouteVersions_NavItems_NavItemId",
                table: "PageRouteVersions");

            migrationBuilder.DropIndex(
                name: "IX_PageRouteVersions_NavItemId",
                table: "PageRouteVersions");

            migrationBuilder.DropColumn(
                name: "HasNavItem",
                table: "PageRouteVersions");
        }
    }
}
