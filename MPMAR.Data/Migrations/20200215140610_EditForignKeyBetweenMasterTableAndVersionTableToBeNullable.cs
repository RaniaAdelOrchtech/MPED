using Microsoft.EntityFrameworkCore.Migrations;

namespace MPMAR.Data.Migrations
{
    public partial class EditForignKeyBetweenMasterTableAndVersionTableToBeNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PageRouteVersions_PageRouteId",
                table: "PageRouteVersions");

            migrationBuilder.DropIndex(
                name: "IX_NavItemVersions_NavItemId",
                table: "NavItemVersions");

            migrationBuilder.AlterColumn<int>(
                name: "PageRouteId",
                table: "PageRouteVersions",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "NavItemId",
                table: "NavItemVersions",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_PageRouteVersions_PageRouteId",
                table: "PageRouteVersions",
                column: "PageRouteId",
                unique: true,
                filter: "[PageRouteId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_NavItemVersions_NavItemId",
                table: "NavItemVersions",
                column: "NavItemId",
                unique: true,
                filter: "[NavItemId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PageRouteVersions_PageRouteId",
                table: "PageRouteVersions");

            migrationBuilder.DropIndex(
                name: "IX_NavItemVersions_NavItemId",
                table: "NavItemVersions");

            migrationBuilder.AlterColumn<int>(
                name: "PageRouteId",
                table: "PageRouteVersions",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "NavItemId",
                table: "NavItemVersions",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PageRouteVersions_PageRouteId",
                table: "PageRouteVersions",
                column: "PageRouteId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NavItemVersions_NavItemId",
                table: "NavItemVersions",
                column: "NavItemId",
                unique: true);
        }
    }
}
