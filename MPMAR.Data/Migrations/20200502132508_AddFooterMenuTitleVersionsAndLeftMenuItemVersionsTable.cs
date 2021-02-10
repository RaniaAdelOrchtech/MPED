using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MPMAR.Data.Migrations
{
    public partial class AddFooterMenuTitleVersionsAndLeftMenuItemVersionsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FooterMenuTitleVersionsId",
                table: "FooterMenuItem",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "FooterMenuTitleVersions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    CreatedById = table.Column<string>(maxLength: 450, nullable: true),
                    ApprovalDate = table.Column<DateTime>(nullable: true),
                    ApprovedById = table.Column<string>(maxLength: 450, nullable: true),
                    EnTitle = table.Column<string>(nullable: true),
                    ArTitle = table.Column<string>(nullable: true),
                    Order = table.Column<int>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ChangeActionEnum = table.Column<int>(nullable: true),
                    VersionStatusEnum = table.Column<int>(nullable: true),
                    FooterMenuTitleId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FooterMenuTitleVersions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FooterMenuTitleVersions_AspNetUsers_ApprovedById",
                        column: x => x.ApprovedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FooterMenuTitleVersions_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FooterMenuTitleVersions_FooterMenuTitles_FooterMenuTitleId",
                        column: x => x.FooterMenuTitleId,
                        principalTable: "FooterMenuTitles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LeftMenuItemVersions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    CreatedById = table.Column<string>(maxLength: 450, nullable: true),
                    ApprovalDate = table.Column<DateTime>(nullable: true),
                    ApprovedById = table.Column<string>(maxLength: 450, nullable: true),
                    LeftMenuType = table.Column<string>(nullable: true),
                    EnTitle = table.Column<string>(nullable: true),
                    ArTitle = table.Column<string>(nullable: true),
                    Link = table.Column<string>(nullable: true),
                    Order = table.Column<int>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ImagePath = table.Column<string>(nullable: true),
                    ChangeActionEnum = table.Column<int>(nullable: true),
                    VersionStatusEnum = table.Column<int>(nullable: true),
                    LeftMenuItemId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeftMenuItemVersions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LeftMenuItemVersions_AspNetUsers_ApprovedById",
                        column: x => x.ApprovedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LeftMenuItemVersions_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LeftMenuItemVersions_LeftMenuItem_LeftMenuItemId",
                        column: x => x.LeftMenuItemId,
                        principalTable: "LeftMenuItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FooterMenuItem_FooterMenuTitleVersionsId",
                table: "FooterMenuItem",
                column: "FooterMenuTitleVersionsId");

            migrationBuilder.CreateIndex(
                name: "IX_FooterMenuTitleVersions_ApprovedById",
                table: "FooterMenuTitleVersions",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_FooterMenuTitleVersions_CreatedById",
                table: "FooterMenuTitleVersions",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_FooterMenuTitleVersions_FooterMenuTitleId",
                table: "FooterMenuTitleVersions",
                column: "FooterMenuTitleId");

            migrationBuilder.CreateIndex(
                name: "IX_LeftMenuItemVersions_ApprovedById",
                table: "LeftMenuItemVersions",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_LeftMenuItemVersions_CreatedById",
                table: "LeftMenuItemVersions",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_LeftMenuItemVersions_LeftMenuItemId",
                table: "LeftMenuItemVersions",
                column: "LeftMenuItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_FooterMenuItem_FooterMenuTitleVersions_FooterMenuTitleVersionsId",
                table: "FooterMenuItem",
                column: "FooterMenuTitleVersionsId",
                principalTable: "FooterMenuTitleVersions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FooterMenuItem_FooterMenuTitleVersions_FooterMenuTitleVersionsId",
                table: "FooterMenuItem");

            migrationBuilder.DropTable(
                name: "FooterMenuTitleVersions");

            migrationBuilder.DropTable(
                name: "LeftMenuItemVersions");

            migrationBuilder.DropIndex(
                name: "IX_FooterMenuItem_FooterMenuTitleVersionsId",
                table: "FooterMenuItem");

            migrationBuilder.DropColumn(
                name: "FooterMenuTitleVersionsId",
                table: "FooterMenuItem");
        }
    }
}
