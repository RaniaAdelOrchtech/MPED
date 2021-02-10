using Microsoft.EntityFrameworkCore.Migrations;

namespace MPMAR.Data.Migrations
{
    public partial class updatePageContactVersion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PageContactVersions_PageRouteVersions_PageRouteVersionId",
                table: "PageContactVersions");

            migrationBuilder.AlterColumn<int>(
                name: "PageRouteVersionId",
                table: "PageContactVersions",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_PageContactVersions_PageRouteVersions_PageRouteVersionId",
                table: "PageContactVersions",
                column: "PageRouteVersionId",
                principalTable: "PageRouteVersions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PageContactVersions_PageRouteVersions_PageRouteVersionId",
                table: "PageContactVersions");

            migrationBuilder.AlterColumn<int>(
                name: "PageRouteVersionId",
                table: "PageContactVersions",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PageContactVersions_PageRouteVersions_PageRouteVersionId",
                table: "PageContactVersions",
                column: "PageRouteVersionId",
                principalTable: "PageRouteVersions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
