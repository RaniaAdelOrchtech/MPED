using Microsoft.EntityFrameworkCore.Migrations;

namespace MPMAR.Data.Migrations
{
    public partial class PageSectionApprovalMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PageSectionCards_PageSectionCardVersions_PageSectionCardVersionId",
                table: "PageSectionCards");

            migrationBuilder.DropForeignKey(
                name: "FK_PageSections_PageSectionVersions_PageSectionVersionId",
                table: "PageSections");

            migrationBuilder.DropIndex(
                name: "IX_PageSections_PageSectionVersionId",
                table: "PageSections");

            migrationBuilder.DropIndex(
                name: "IX_PageSectionCards_PageSectionCardVersionId",
                table: "PageSectionCards");

            migrationBuilder.DropColumn(
                name: "PageSectionCardVersionId",
                table: "PageSectionCards");

            migrationBuilder.AddColumn<int>(
                name: "PageSectionId",
                table: "PageSectionVersions",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PageSectionCardId",
                table: "PageSectionCardVersions",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PageSectionVersions_PageSectionId",
                table: "PageSectionVersions",
                column: "PageSectionId",
                unique: true,
                filter: "[PageSectionId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PageSectionCardVersions_PageSectionCardId",
                table: "PageSectionCardVersions",
                column: "PageSectionCardId");

            migrationBuilder.AddForeignKey(
                name: "FK_PageSectionCardVersions_PageSectionCards_PageSectionCardId",
                table: "PageSectionCardVersions",
                column: "PageSectionCardId",
                principalTable: "PageSectionCards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PageSectionVersions_PageSections_PageSectionId",
                table: "PageSectionVersions",
                column: "PageSectionId",
                principalTable: "PageSections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PageSectionCardVersions_PageSectionCards_PageSectionCardId",
                table: "PageSectionCardVersions");

            migrationBuilder.DropForeignKey(
                name: "FK_PageSectionVersions_PageSections_PageSectionId",
                table: "PageSectionVersions");

            migrationBuilder.DropIndex(
                name: "IX_PageSectionVersions_PageSectionId",
                table: "PageSectionVersions");

            migrationBuilder.DropIndex(
                name: "IX_PageSectionCardVersions_PageSectionCardId",
                table: "PageSectionCardVersions");

            migrationBuilder.DropColumn(
                name: "PageSectionId",
                table: "PageSectionVersions");

            migrationBuilder.DropColumn(
                name: "PageSectionCardId",
                table: "PageSectionCardVersions");

            migrationBuilder.AddColumn<int>(
                name: "PageSectionCardVersionId",
                table: "PageSectionCards",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PageSections_PageSectionVersionId",
                table: "PageSections",
                column: "PageSectionVersionId",
                unique: true,
                filter: "[PageSectionVersionId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PageSectionCards_PageSectionCardVersionId",
                table: "PageSectionCards",
                column: "PageSectionCardVersionId",
                unique: true,
                filter: "[PageSectionCardVersionId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_PageSectionCards_PageSectionCardVersions_PageSectionCardVersionId",
                table: "PageSectionCards",
                column: "PageSectionCardVersionId",
                principalTable: "PageSectionCardVersions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PageSections_PageSectionVersions_PageSectionVersionId",
                table: "PageSections",
                column: "PageSectionVersionId",
                principalTable: "PageSectionVersions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
