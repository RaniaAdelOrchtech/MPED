using Microsoft.EntityFrameworkCore.Migrations;

namespace MPMAR.Data.Migrations
{
    public partial class EditRelationBetweenPageRouteAndPageRouteVersion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PageRouteVersions_PageRoutes_PageRouteId",
                table: "PageRouteVersions");

            migrationBuilder.DropIndex(
                name: "IX_PageRouteVersions_PageRouteId",
                table: "PageRouteVersions");

            migrationBuilder.DropColumn(
                name: "PageRouteId",
                table: "PageRouteVersions");

            migrationBuilder.AddColumn<int>(
                name: "PageRouteVersionId",
                table: "PageRoutes",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PageRoutes_PageRouteVersionId",
                table: "PageRoutes",
                column: "PageRouteVersionId",
                unique: true,
                filter: "[PageRouteVersionId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_PageRoutes_PageRouteVersions_PageRouteVersionId",
                table: "PageRoutes",
                column: "PageRouteVersionId",
                principalTable: "PageRouteVersions",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PageRoutes_PageRouteVersions_PageRouteVersionId",
                table: "PageRoutes");

            migrationBuilder.DropIndex(
                name: "IX_PageRoutes_PageRouteVersionId",
                table: "PageRoutes");

            migrationBuilder.DropColumn(
                name: "PageRouteVersionId",
                table: "PageRoutes");

            migrationBuilder.AddColumn<int>(
                name: "PageRouteId",
                table: "PageRouteVersions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PageRouteVersions_PageRouteId",
                table: "PageRouteVersions",
                column: "PageRouteId",
                unique: true,
                filter: "[PageRouteId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_PageRouteVersions_PageRoutes_PageRouteId",
                table: "PageRouteVersions",
                column: "PageRouteId",
                principalTable: "PageRoutes",
                principalColumn: "Id");
        }
    }
}
