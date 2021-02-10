using Microsoft.EntityFrameworkCore.Migrations;

namespace MPMAR.Data.Migrations
{
    public partial class updatePageMinistryVersion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PageMinistryVersions_PageRouteVersions_PageRouteVersionId",
                table: "PageMinistryVersions");

            migrationBuilder.DropIndex(
                name: "IX_PageMinistryVersions_PageRouteVersionId",
                table: "PageMinistryVersions");

            migrationBuilder.DropColumn(
                name: "PageRouteVersionId",
                table: "PageMinistryVersions");

            migrationBuilder.AddColumn<int>(
                name: "PageRouteId",
                table: "PageMinistryVersions",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PageMinistryVersions_PageRouteId",
                table: "PageMinistryVersions",
                column: "PageRouteId");

            migrationBuilder.AddForeignKey(
                name: "FK_PageMinistryVersions_PageRoutes_PageRouteId",
                table: "PageMinistryVersions",
                column: "PageRouteId",
                principalTable: "PageRoutes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PageMinistryVersions_PageRoutes_PageRouteId",
                table: "PageMinistryVersions");

            migrationBuilder.DropIndex(
                name: "IX_PageMinistryVersions_PageRouteId",
                table: "PageMinistryVersions");

            migrationBuilder.DropColumn(
                name: "PageRouteId",
                table: "PageMinistryVersions");

            migrationBuilder.AddColumn<int>(
                name: "PageRouteVersionId",
                table: "PageMinistryVersions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PageMinistryVersions_PageRouteVersionId",
                table: "PageMinistryVersions",
                column: "PageRouteVersionId");

            migrationBuilder.AddForeignKey(
                name: "FK_PageMinistryVersions_PageRouteVersions_PageRouteVersionId",
                table: "PageMinistryVersions",
                column: "PageRouteVersionId",
                principalTable: "PageRouteVersions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
