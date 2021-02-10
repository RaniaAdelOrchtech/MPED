using Microsoft.EntityFrameworkCore.Migrations;

namespace MPMAR.Data.Migrations
{
    public partial class updatePageRouteVersion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PageRouteVersions_Statuses_StatusId",
                table: "PageRouteVersions");

            migrationBuilder.AlterColumn<int>(
                name: "StatusId",
                table: "PageRouteVersions",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_PageRouteVersions_Statuses_StatusId",
                table: "PageRouteVersions",
                column: "StatusId",
                principalTable: "Statuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PageRouteVersions_Statuses_StatusId",
                table: "PageRouteVersions");

            migrationBuilder.AlterColumn<int>(
                name: "StatusId",
                table: "PageRouteVersions",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PageRouteVersions_Statuses_StatusId",
                table: "PageRouteVersions",
                column: "StatusId",
                principalTable: "Statuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
