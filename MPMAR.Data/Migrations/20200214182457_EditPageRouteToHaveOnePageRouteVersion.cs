using Microsoft.EntityFrameworkCore.Migrations;

namespace MPMAR.Data.Migrations
{
    public partial class EditPageRouteToHaveOnePageRouteVersion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PageRouteVersions_PageRouteId",
                table: "PageRouteVersions");

            migrationBuilder.CreateIndex(
                name: "IX_PageRouteVersions_PageRouteId",
                table: "PageRouteVersions",
                column: "PageRouteId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PageRouteVersions_PageRouteId",
                table: "PageRouteVersions");

            migrationBuilder.CreateIndex(
                name: "IX_PageRouteVersions_PageRouteId",
                table: "PageRouteVersions",
                column: "PageRouteId");
        }
    }
}
