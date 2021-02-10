using Microsoft.EntityFrameworkCore.Migrations;

namespace MPMAR.Data.Migrations
{
    public partial class EditPageRouteToHaveSectionsDirectly : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DynamicPageContents_AspNetUsers_ApprovedById",
                table: "DynamicPageContents");

            migrationBuilder.DropForeignKey(
                name: "FK_DynamicPageContents_AspNetUsers_CreatedById",
                table: "DynamicPageContents");

            migrationBuilder.DropForeignKey(
                name: "FK_DynamicPageContents_AspNetUsers_ModifiedById",
                table: "DynamicPageContents");

            migrationBuilder.DropForeignKey(
                name: "FK_DynamicPageContents_PageRoutes_PageRouteId",
                table: "DynamicPageContents");

            migrationBuilder.DropForeignKey(
                name: "FK_DynamicPageContentVersions_AspNetUsers_ApprovedById",
                table: "DynamicPageContentVersions");

            migrationBuilder.DropForeignKey(
                name: "FK_DynamicPageContentVersions_AspNetUsers_CreatedById",
                table: "DynamicPageContentVersions");

            migrationBuilder.DropForeignKey(
                name: "FK_DynamicPageContentVersions_DynamicPageContents_DynamicPageContentId",
                table: "DynamicPageContentVersions");

            migrationBuilder.DropForeignKey(
                name: "FK_DynamicPageSections_DynamicPageContents_DynamicPageContentId",
                table: "DynamicPageSections");

            migrationBuilder.DropIndex(
                name: "IX_DynamicPageSectionVersions_DynamicPageSectionId",
                table: "DynamicPageSectionVersions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DynamicPageContentVersions",
                table: "DynamicPageContentVersions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DynamicPageContents",
                table: "DynamicPageContents");

            migrationBuilder.DropIndex(
                name: "IX_DynamicPageContents_PageRouteId",
                table: "DynamicPageContents");

            migrationBuilder.DropColumn(
                name: "PageRouteId",
                table: "DynamicPageContents");

            migrationBuilder.RenameTable(
                name: "DynamicPageContentVersions",
                newName: "DynamicPageContentVersion");

            migrationBuilder.RenameTable(
                name: "DynamicPageContents",
                newName: "DynamicPageContent");

            migrationBuilder.RenameIndex(
                name: "IX_DynamicPageContentVersions_DynamicPageContentId",
                table: "DynamicPageContentVersion",
                newName: "IX_DynamicPageContentVersion_DynamicPageContentId");

            migrationBuilder.RenameIndex(
                name: "IX_DynamicPageContentVersions_CreatedById",
                table: "DynamicPageContentVersion",
                newName: "IX_DynamicPageContentVersion_CreatedById");

            migrationBuilder.RenameIndex(
                name: "IX_DynamicPageContentVersions_ApprovedById",
                table: "DynamicPageContentVersion",
                newName: "IX_DynamicPageContentVersion_ApprovedById");

            migrationBuilder.RenameIndex(
                name: "IX_DynamicPageContents_ModifiedById",
                table: "DynamicPageContent",
                newName: "IX_DynamicPageContent_ModifiedById");

            migrationBuilder.RenameIndex(
                name: "IX_DynamicPageContents_CreatedById",
                table: "DynamicPageContent",
                newName: "IX_DynamicPageContent_CreatedById");

            migrationBuilder.RenameIndex(
                name: "IX_DynamicPageContents_ApprovedById",
                table: "DynamicPageContent",
                newName: "IX_DynamicPageContent_ApprovedById");

            migrationBuilder.AddColumn<int>(
                name: "PageRouteVersionId",
                table: "DynamicPageSectionVersions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "DynamicPageContentId",
                table: "DynamicPageSections",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "PageRouteId",
                table: "DynamicPageSections",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_DynamicPageContentVersion",
                table: "DynamicPageContentVersion",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DynamicPageContent",
                table: "DynamicPageContent",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_DynamicPageSectionVersions_DynamicPageSectionId",
                table: "DynamicPageSectionVersions",
                column: "DynamicPageSectionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DynamicPageSectionVersions_PageRouteVersionId",
                table: "DynamicPageSectionVersions",
                column: "PageRouteVersionId");

            migrationBuilder.CreateIndex(
                name: "IX_DynamicPageSections_PageRouteId",
                table: "DynamicPageSections",
                column: "PageRouteId");

            migrationBuilder.AddForeignKey(
                name: "FK_DynamicPageContent_AspNetUsers_ApprovedById",
                table: "DynamicPageContent",
                column: "ApprovedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DynamicPageContent_AspNetUsers_CreatedById",
                table: "DynamicPageContent",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DynamicPageContent_AspNetUsers_ModifiedById",
                table: "DynamicPageContent",
                column: "ModifiedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DynamicPageContentVersion_AspNetUsers_ApprovedById",
                table: "DynamicPageContentVersion",
                column: "ApprovedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DynamicPageContentVersion_AspNetUsers_CreatedById",
                table: "DynamicPageContentVersion",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DynamicPageContentVersion_DynamicPageContent_DynamicPageContentId",
                table: "DynamicPageContentVersion",
                column: "DynamicPageContentId",
                principalTable: "DynamicPageContent",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DynamicPageSections_DynamicPageContent_DynamicPageContentId",
                table: "DynamicPageSections",
                column: "DynamicPageContentId",
                principalTable: "DynamicPageContent",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DynamicPageSections_PageRoutes_PageRouteId",
                table: "DynamicPageSections",
                column: "PageRouteId",
                principalTable: "PageRoutes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DynamicPageSectionVersions_PageRouteVersions_PageRouteVersionId",
                table: "DynamicPageSectionVersions",
                column: "PageRouteVersionId",
                principalTable: "PageRouteVersions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DynamicPageContent_AspNetUsers_ApprovedById",
                table: "DynamicPageContent");

            migrationBuilder.DropForeignKey(
                name: "FK_DynamicPageContent_AspNetUsers_CreatedById",
                table: "DynamicPageContent");

            migrationBuilder.DropForeignKey(
                name: "FK_DynamicPageContent_AspNetUsers_ModifiedById",
                table: "DynamicPageContent");

            migrationBuilder.DropForeignKey(
                name: "FK_DynamicPageContentVersion_AspNetUsers_ApprovedById",
                table: "DynamicPageContentVersion");

            migrationBuilder.DropForeignKey(
                name: "FK_DynamicPageContentVersion_AspNetUsers_CreatedById",
                table: "DynamicPageContentVersion");

            migrationBuilder.DropForeignKey(
                name: "FK_DynamicPageContentVersion_DynamicPageContent_DynamicPageContentId",
                table: "DynamicPageContentVersion");

            migrationBuilder.DropForeignKey(
                name: "FK_DynamicPageSections_DynamicPageContent_DynamicPageContentId",
                table: "DynamicPageSections");

            migrationBuilder.DropForeignKey(
                name: "FK_DynamicPageSections_PageRoutes_PageRouteId",
                table: "DynamicPageSections");

            migrationBuilder.DropForeignKey(
                name: "FK_DynamicPageSectionVersions_PageRouteVersions_PageRouteVersionId",
                table: "DynamicPageSectionVersions");

            migrationBuilder.DropIndex(
                name: "IX_DynamicPageSectionVersions_DynamicPageSectionId",
                table: "DynamicPageSectionVersions");

            migrationBuilder.DropIndex(
                name: "IX_DynamicPageSectionVersions_PageRouteVersionId",
                table: "DynamicPageSectionVersions");

            migrationBuilder.DropIndex(
                name: "IX_DynamicPageSections_PageRouteId",
                table: "DynamicPageSections");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DynamicPageContentVersion",
                table: "DynamicPageContentVersion");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DynamicPageContent",
                table: "DynamicPageContent");

            migrationBuilder.DropColumn(
                name: "PageRouteVersionId",
                table: "DynamicPageSectionVersions");

            migrationBuilder.DropColumn(
                name: "PageRouteId",
                table: "DynamicPageSections");

            migrationBuilder.RenameTable(
                name: "DynamicPageContentVersion",
                newName: "DynamicPageContentVersions");

            migrationBuilder.RenameTable(
                name: "DynamicPageContent",
                newName: "DynamicPageContents");

            migrationBuilder.RenameIndex(
                name: "IX_DynamicPageContentVersion_DynamicPageContentId",
                table: "DynamicPageContentVersions",
                newName: "IX_DynamicPageContentVersions_DynamicPageContentId");

            migrationBuilder.RenameIndex(
                name: "IX_DynamicPageContentVersion_CreatedById",
                table: "DynamicPageContentVersions",
                newName: "IX_DynamicPageContentVersions_CreatedById");

            migrationBuilder.RenameIndex(
                name: "IX_DynamicPageContentVersion_ApprovedById",
                table: "DynamicPageContentVersions",
                newName: "IX_DynamicPageContentVersions_ApprovedById");

            migrationBuilder.RenameIndex(
                name: "IX_DynamicPageContent_ModifiedById",
                table: "DynamicPageContents",
                newName: "IX_DynamicPageContents_ModifiedById");

            migrationBuilder.RenameIndex(
                name: "IX_DynamicPageContent_CreatedById",
                table: "DynamicPageContents",
                newName: "IX_DynamicPageContents_CreatedById");

            migrationBuilder.RenameIndex(
                name: "IX_DynamicPageContent_ApprovedById",
                table: "DynamicPageContents",
                newName: "IX_DynamicPageContents_ApprovedById");

            migrationBuilder.AlterColumn<int>(
                name: "DynamicPageContentId",
                table: "DynamicPageSections",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PageRouteId",
                table: "DynamicPageContents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_DynamicPageContentVersions",
                table: "DynamicPageContentVersions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DynamicPageContents",
                table: "DynamicPageContents",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_DynamicPageSectionVersions_DynamicPageSectionId",
                table: "DynamicPageSectionVersions",
                column: "DynamicPageSectionId");

            migrationBuilder.CreateIndex(
                name: "IX_DynamicPageContents_PageRouteId",
                table: "DynamicPageContents",
                column: "PageRouteId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DynamicPageContents_AspNetUsers_ApprovedById",
                table: "DynamicPageContents",
                column: "ApprovedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DynamicPageContents_AspNetUsers_CreatedById",
                table: "DynamicPageContents",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DynamicPageContents_AspNetUsers_ModifiedById",
                table: "DynamicPageContents",
                column: "ModifiedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DynamicPageContents_PageRoutes_PageRouteId",
                table: "DynamicPageContents",
                column: "PageRouteId",
                principalTable: "PageRoutes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DynamicPageContentVersions_AspNetUsers_ApprovedById",
                table: "DynamicPageContentVersions",
                column: "ApprovedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DynamicPageContentVersions_AspNetUsers_CreatedById",
                table: "DynamicPageContentVersions",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DynamicPageContentVersions_DynamicPageContents_DynamicPageContentId",
                table: "DynamicPageContentVersions",
                column: "DynamicPageContentId",
                principalTable: "DynamicPageContents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DynamicPageSections_DynamicPageContents_DynamicPageContentId",
                table: "DynamicPageSections",
                column: "DynamicPageContentId",
                principalTable: "DynamicPageContents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
