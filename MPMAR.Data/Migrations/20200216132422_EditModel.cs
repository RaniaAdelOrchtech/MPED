using Microsoft.EntityFrameworkCore.Migrations;

namespace MPMAR.Data.Migrations
{
    public partial class EditModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PageSectionVersions_PageSections_PageSectionId",
                table: "PageSectionVersions");

            migrationBuilder.DropIndex(
                name: "IX_PageSectionVersions_PageSectionId",
                table: "PageSectionVersions");

            migrationBuilder.DropColumn(
                name: "PageSectionId",
                table: "PageSectionVersions");

            migrationBuilder.AddColumn<int>(
                name: "PageSectionTypeId",
                table: "PageSectionVersions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PageSectionVersionId",
                table: "PageSections",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PageSectionVersions_PageSectionTypeId",
                table: "PageSectionVersions",
                column: "PageSectionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PageSections_PageSectionVersionId",
                table: "PageSections",
                column: "PageSectionVersionId",
                unique: true,
                filter: "[PageSectionVersionId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_PageSections_PageSectionVersions_PageSectionVersionId",
                table: "PageSections",
                column: "PageSectionVersionId",
                principalTable: "PageSectionVersions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PageSectionVersions_PageSectionTypes_PageSectionTypeId",
                table: "PageSectionVersions",
                column: "PageSectionTypeId",
                principalTable: "PageSectionTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PageSections_PageSectionVersions_PageSectionVersionId",
                table: "PageSections");

            migrationBuilder.DropForeignKey(
                name: "FK_PageSectionVersions_PageSectionTypes_PageSectionTypeId",
                table: "PageSectionVersions");

            migrationBuilder.DropIndex(
                name: "IX_PageSectionVersions_PageSectionTypeId",
                table: "PageSectionVersions");

            migrationBuilder.DropIndex(
                name: "IX_PageSections_PageSectionVersionId",
                table: "PageSections");

            migrationBuilder.DropColumn(
                name: "PageSectionTypeId",
                table: "PageSectionVersions");

            migrationBuilder.DropColumn(
                name: "PageSectionVersionId",
                table: "PageSections");

            migrationBuilder.AddColumn<int>(
                name: "PageSectionId",
                table: "PageSectionVersions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PageSectionVersions_PageSectionId",
                table: "PageSectionVersions",
                column: "PageSectionId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PageSectionVersions_PageSections_PageSectionId",
                table: "PageSectionVersions",
                column: "PageSectionId",
                principalTable: "PageSections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
