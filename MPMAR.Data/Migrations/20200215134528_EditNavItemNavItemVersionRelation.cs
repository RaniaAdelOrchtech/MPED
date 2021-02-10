using Microsoft.EntityFrameworkCore.Migrations;

namespace MPMAR.Data.Migrations
{
    public partial class EditNavItemNavItemVersionRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NavItems_NavItemVersions_NavItemId",
                table: "NavItems");

            migrationBuilder.DropIndex(
                name: "IX_NavItems_NavItemId",
                table: "NavItems");

            migrationBuilder.DropColumn(
                name: "NavItemId",
                table: "NavItems");

            migrationBuilder.CreateIndex(
                name: "IX_NavItemVersions_NavItemId",
                table: "NavItemVersions",
                column: "NavItemId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_NavItemVersions_NavItems_NavItemId",
                table: "NavItemVersions",
                column: "NavItemId",
                principalTable: "NavItems",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NavItemVersions_NavItems_NavItemId",
                table: "NavItemVersions");

            migrationBuilder.DropIndex(
                name: "IX_NavItemVersions_NavItemId",
                table: "NavItemVersions");

            migrationBuilder.AddColumn<int>(
                name: "NavItemId",
                table: "NavItems",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_NavItems_NavItemId",
                table: "NavItems",
                column: "NavItemId",
                unique: true,
                filter: "[NavItemId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_NavItems_NavItemVersions_NavItemId",
                table: "NavItems",
                column: "NavItemId",
                principalTable: "NavItemVersions",
                principalColumn: "Id");
        }
    }
}
