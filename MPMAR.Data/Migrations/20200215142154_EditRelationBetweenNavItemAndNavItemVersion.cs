using Microsoft.EntityFrameworkCore.Migrations;

namespace MPMAR.Data.Migrations
{
    public partial class EditRelationBetweenNavItemAndNavItemVersion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NavItemVersions_NavItems_NavItemId",
                table: "NavItemVersions");

            migrationBuilder.DropIndex(
                name: "IX_NavItemVersions_NavItemId",
                table: "NavItemVersions");

            migrationBuilder.DropColumn(
                name: "NavItemId",
                table: "NavItemVersions");

            migrationBuilder.AddColumn<int>(
                name: "NavItemVersionId",
                table: "NavItems",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_NavItems_NavItemVersionId",
                table: "NavItems",
                column: "NavItemVersionId",
                unique: true,
                filter: "[NavItemVersionId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_NavItems_NavItemVersions_NavItemVersionId",
                table: "NavItems",
                column: "NavItemVersionId",
                principalTable: "NavItemVersions",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NavItems_NavItemVersions_NavItemVersionId",
                table: "NavItems");

            migrationBuilder.DropIndex(
                name: "IX_NavItems_NavItemVersionId",
                table: "NavItems");

            migrationBuilder.DropColumn(
                name: "NavItemVersionId",
                table: "NavItems");

            migrationBuilder.AddColumn<int>(
                name: "NavItemId",
                table: "NavItemVersions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_NavItemVersions_NavItemId",
                table: "NavItemVersions",
                column: "NavItemId",
                unique: true,
                filter: "[NavItemId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_NavItemVersions_NavItems_NavItemId",
                table: "NavItemVersions",
                column: "NavItemId",
                principalTable: "NavItems",
                principalColumn: "Id");
        }
    }
}
