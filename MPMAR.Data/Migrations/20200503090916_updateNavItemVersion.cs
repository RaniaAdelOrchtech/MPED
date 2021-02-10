using Microsoft.EntityFrameworkCore.Migrations;

namespace MPMAR.Data.Migrations
{
    public partial class updateNavItemVersion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NavItemVersions_Statuses_StatusId",
                table: "NavItemVersions");

            migrationBuilder.AlterColumn<int>(
                name: "StatusId",
                table: "NavItemVersions",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_NavItemVersions_Statuses_StatusId",
                table: "NavItemVersions",
                column: "StatusId",
                principalTable: "Statuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NavItemVersions_Statuses_StatusId",
                table: "NavItemVersions");

            migrationBuilder.AlterColumn<int>(
                name: "StatusId",
                table: "NavItemVersions",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_NavItemVersions_Statuses_StatusId",
                table: "NavItemVersions",
                column: "StatusId",
                principalTable: "Statuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
