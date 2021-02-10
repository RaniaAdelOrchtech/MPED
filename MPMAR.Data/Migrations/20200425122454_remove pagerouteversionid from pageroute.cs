using Microsoft.EntityFrameworkCore.Migrations;

namespace MPMAR.Data.Migrations
{
    public partial class removepagerouteversionidfrompageroute : Migration
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
                name: "PageRouteVersionId",
                table: "PageRoutes");

            migrationBuilder.CreateIndex(
                name: "IX_PageRouteVersions_PageRouteId",
                table: "PageRouteVersions",
                column: "PageRouteId");

            migrationBuilder.AddForeignKey(
                name: "FK_PageRouteVersions_PageRoutes_PageRouteId",
                table: "PageRouteVersions",
                column: "PageRouteId",
                principalTable: "PageRoutes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PageRouteVersions_PageRoutes_PageRouteId",
                table: "PageRouteVersions");

            migrationBuilder.DropIndex(
                name: "IX_PageRouteVersions_PageRouteId",
                table: "PageRouteVersions");

            migrationBuilder.AddColumn<int>(
                name: "PageRouteVersionId",
                table: "PageRoutes",
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
