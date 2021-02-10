using Microsoft.EntityFrameworkCore.Migrations;

namespace MPMAR.Data.Migrations
{
    public partial class AddSectionCardTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PageSectionCards_PageSectionCardVersion_PageSectionCardVersionId",
                table: "PageSectionCards");

            migrationBuilder.DropForeignKey(
                name: "FK_PageSectionCardVersion_AspNetUsers_ApprovedById",
                table: "PageSectionCardVersion");

            migrationBuilder.DropForeignKey(
                name: "FK_PageSectionCardVersion_AspNetUsers_CreatedById",
                table: "PageSectionCardVersion");

            migrationBuilder.DropForeignKey(
                name: "FK_PageSectionCardVersion_PageSectionVersions_PageSectionVersionId",
                table: "PageSectionCardVersion");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PageSectionCardVersion",
                table: "PageSectionCardVersion");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "PageSectionCards");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "PageSectionCards");

            migrationBuilder.RenameTable(
                name: "PageSectionCardVersion",
                newName: "PageSectionCardVersions");

            migrationBuilder.RenameIndex(
                name: "IX_PageSectionCardVersion_PageSectionVersionId",
                table: "PageSectionCardVersions",
                newName: "IX_PageSectionCardVersions_PageSectionVersionId");

            migrationBuilder.RenameIndex(
                name: "IX_PageSectionCardVersion_CreatedById",
                table: "PageSectionCardVersions",
                newName: "IX_PageSectionCardVersions_CreatedById");

            migrationBuilder.RenameIndex(
                name: "IX_PageSectionCardVersion_ApprovedById",
                table: "PageSectionCardVersions",
                newName: "IX_PageSectionCardVersions_ApprovedById");

            migrationBuilder.AddColumn<string>(
                name: "ArDescription",
                table: "PageSectionCards",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ArTitle",
                table: "PageSectionCards",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EnDescription",
                table: "PageSectionCards",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EnTitle",
                table: "PageSectionCards",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PageSectionCardVersions",
                table: "PageSectionCardVersions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PageSectionCards_PageSectionCardVersions_PageSectionCardVersionId",
                table: "PageSectionCards",
                column: "PageSectionCardVersionId",
                principalTable: "PageSectionCardVersions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PageSectionCardVersions_AspNetUsers_ApprovedById",
                table: "PageSectionCardVersions",
                column: "ApprovedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PageSectionCardVersions_AspNetUsers_CreatedById",
                table: "PageSectionCardVersions",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PageSectionCardVersions_PageSectionVersions_PageSectionVersionId",
                table: "PageSectionCardVersions",
                column: "PageSectionVersionId",
                principalTable: "PageSectionVersions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PageSectionCards_PageSectionCardVersions_PageSectionCardVersionId",
                table: "PageSectionCards");

            migrationBuilder.DropForeignKey(
                name: "FK_PageSectionCardVersions_AspNetUsers_ApprovedById",
                table: "PageSectionCardVersions");

            migrationBuilder.DropForeignKey(
                name: "FK_PageSectionCardVersions_AspNetUsers_CreatedById",
                table: "PageSectionCardVersions");

            migrationBuilder.DropForeignKey(
                name: "FK_PageSectionCardVersions_PageSectionVersions_PageSectionVersionId",
                table: "PageSectionCardVersions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PageSectionCardVersions",
                table: "PageSectionCardVersions");

            migrationBuilder.DropColumn(
                name: "ArDescription",
                table: "PageSectionCards");

            migrationBuilder.DropColumn(
                name: "ArTitle",
                table: "PageSectionCards");

            migrationBuilder.DropColumn(
                name: "EnDescription",
                table: "PageSectionCards");

            migrationBuilder.DropColumn(
                name: "EnTitle",
                table: "PageSectionCards");

            migrationBuilder.RenameTable(
                name: "PageSectionCardVersions",
                newName: "PageSectionCardVersion");

            migrationBuilder.RenameIndex(
                name: "IX_PageSectionCardVersions_PageSectionVersionId",
                table: "PageSectionCardVersion",
                newName: "IX_PageSectionCardVersion_PageSectionVersionId");

            migrationBuilder.RenameIndex(
                name: "IX_PageSectionCardVersions_CreatedById",
                table: "PageSectionCardVersion",
                newName: "IX_PageSectionCardVersion_CreatedById");

            migrationBuilder.RenameIndex(
                name: "IX_PageSectionCardVersions_ApprovedById",
                table: "PageSectionCardVersion",
                newName: "IX_PageSectionCardVersion_ApprovedById");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "PageSectionCards",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "PageSectionCards",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PageSectionCardVersion",
                table: "PageSectionCardVersion",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PageSectionCards_PageSectionCardVersion_PageSectionCardVersionId",
                table: "PageSectionCards",
                column: "PageSectionCardVersionId",
                principalTable: "PageSectionCardVersion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PageSectionCardVersion_AspNetUsers_ApprovedById",
                table: "PageSectionCardVersion",
                column: "ApprovedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PageSectionCardVersion_AspNetUsers_CreatedById",
                table: "PageSectionCardVersion",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PageSectionCardVersion_PageSectionVersions_PageSectionVersionId",
                table: "PageSectionCardVersion",
                column: "PageSectionVersionId",
                principalTable: "PageSectionVersions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
