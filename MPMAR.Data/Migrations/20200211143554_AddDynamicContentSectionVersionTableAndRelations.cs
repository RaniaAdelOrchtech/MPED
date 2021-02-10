using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MPMAR.Data.Migrations
{
    public partial class AddDynamicContentSectionVersionTableAndRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ArPageName",
                table: "PageRouteVersions");

            migrationBuilder.DropColumn(
                name: "EnPageName",
                table: "PageRouteVersions");

            migrationBuilder.DropColumn(
                name: "SectionName",
                table: "DynamicPageSectionTypes");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "DynamicPageSections");

            migrationBuilder.DropColumn(
                name: "ImageAlt",
                table: "DynamicPageSections");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "DynamicPageSections");

            migrationBuilder.AddColumn<string>(
                name: "ArName",
                table: "PageRouteVersions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EnName",
                table: "PageRouteVersions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ArName",
                table: "DynamicPageSectionTypes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EnName",
                table: "DynamicPageSectionTypes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ArDescription",
                table: "DynamicPageSections",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ArImageAlt",
                table: "DynamicPageSections",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ArTitle",
                table: "DynamicPageSections",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EnDescription",
                table: "DynamicPageSections",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EnImageAlt",
                table: "DynamicPageSections",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EnTitle",
                table: "DynamicPageSections",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "DynamicPageContentVersions",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DynamicPageSectionVersions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    CreatedById = table.Column<string>(maxLength: 450, nullable: true),
                    ApprovalDate = table.Column<DateTime>(nullable: true),
                    ApprovedById = table.Column<string>(maxLength: 450, nullable: true),
                    IsIgnored = table.Column<bool>(nullable: false),
                    EnTitle = table.Column<string>(nullable: true),
                    ArTitle = table.Column<string>(nullable: true),
                    EnDescription = table.Column<string>(nullable: true),
                    ArDescription = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true),
                    EnImageAlt = table.Column<string>(nullable: true),
                    ArImageAlt = table.Column<string>(nullable: true),
                    Order = table.Column<int>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    IsApproved = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DynamicPageSectionId = table.Column<int>(nullable: false),
                    ApplicationUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DynamicPageSectionVersions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DynamicPageSectionVersions_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DynamicPageSectionVersions_AspNetUsers_ApprovedById",
                        column: x => x.ApprovedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DynamicPageSectionVersions_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DynamicPageSectionVersions_DynamicPageSections_DynamicPageSectionId",
                        column: x => x.DynamicPageSectionId,
                        principalTable: "DynamicPageSections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DynamicPageContentVersions_ApplicationUserId",
                table: "DynamicPageContentVersions",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_DynamicPageSectionVersions_ApplicationUserId",
                table: "DynamicPageSectionVersions",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_DynamicPageSectionVersions_ApprovedById",
                table: "DynamicPageSectionVersions",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_DynamicPageSectionVersions_CreatedById",
                table: "DynamicPageSectionVersions",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_DynamicPageSectionVersions_DynamicPageSectionId",
                table: "DynamicPageSectionVersions",
                column: "DynamicPageSectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_DynamicPageContentVersions_AspNetUsers_ApplicationUserId",
                table: "DynamicPageContentVersions",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DynamicPageContentVersions_AspNetUsers_ApplicationUserId",
                table: "DynamicPageContentVersions");

            migrationBuilder.DropTable(
                name: "DynamicPageSectionVersions");

            migrationBuilder.DropIndex(
                name: "IX_DynamicPageContentVersions_ApplicationUserId",
                table: "DynamicPageContentVersions");

            migrationBuilder.DropColumn(
                name: "ArName",
                table: "PageRouteVersions");

            migrationBuilder.DropColumn(
                name: "EnName",
                table: "PageRouteVersions");

            migrationBuilder.DropColumn(
                name: "ArName",
                table: "DynamicPageSectionTypes");

            migrationBuilder.DropColumn(
                name: "EnName",
                table: "DynamicPageSectionTypes");

            migrationBuilder.DropColumn(
                name: "ArDescription",
                table: "DynamicPageSections");

            migrationBuilder.DropColumn(
                name: "ArImageAlt",
                table: "DynamicPageSections");

            migrationBuilder.DropColumn(
                name: "ArTitle",
                table: "DynamicPageSections");

            migrationBuilder.DropColumn(
                name: "EnDescription",
                table: "DynamicPageSections");

            migrationBuilder.DropColumn(
                name: "EnImageAlt",
                table: "DynamicPageSections");

            migrationBuilder.DropColumn(
                name: "EnTitle",
                table: "DynamicPageSections");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "DynamicPageContentVersions");

            migrationBuilder.AddColumn<string>(
                name: "ArPageName",
                table: "PageRouteVersions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EnPageName",
                table: "PageRouteVersions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SectionName",
                table: "DynamicPageSectionTypes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "DynamicPageSections",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageAlt",
                table: "DynamicPageSections",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "DynamicPageSections",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
