using Microsoft.EntityFrameworkCore.Migrations;

namespace MPMAR.Data.Migrations
{
    public partial class PageRouteVersionChangesMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PageRoutes_PageRouteVersions_PageRouteVersionId",
                table: "PageRoutes");

            migrationBuilder.DropIndex(
                name: "IX_PageRoutes_PageRouteVersionId",
                table: "PageRoutes");

            migrationBuilder.AddColumn<int>(
                name: "ChangeActionEnum",
                table: "PageRouteVersions",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PageRouteId",
                table: "PageRouteVersions",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VersionStatusEnum",
                table: "PageRouteVersions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PageFilePathAr",
                table: "PageRoutes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PageFilePathEn",
                table: "PageRoutes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PageType",
                table: "PageRoutes",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PageRouteVersions_PageRoutes_PageRouteId",
                table: "PageRouteVersions");

            migrationBuilder.DropIndex(
                name: "IX_PageRouteVersions_PageRouteId",
                table: "PageRouteVersions");

            migrationBuilder.DropColumn(
                name: "ChangeActionEnum",
                table: "PageRouteVersions");

            migrationBuilder.DropColumn(
                name: "PageRouteId",
                table: "PageRouteVersions");

            migrationBuilder.DropColumn(
                name: "VersionStatusEnum",
                table: "PageRouteVersions");

            migrationBuilder.DropColumn(
                name: "PageFilePathAr",
                table: "PageRoutes");

            migrationBuilder.DropColumn(
                name: "PageFilePathEn",
                table: "PageRoutes");

            migrationBuilder.DropColumn(
                name: "PageType",
                table: "PageRoutes");

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
    }
}
