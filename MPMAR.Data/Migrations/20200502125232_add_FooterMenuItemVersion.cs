using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MPMAR.Data.Migrations
{
    public partial class add_FooterMenuItemVersion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FooterMenuItemVersions",
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
                    Link = table.Column<string>(nullable: true),
                    Order = table.Column<int>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    EnColumnPostion = table.Column<string>(nullable: true),
                    ArColumnPostion = table.Column<string>(nullable: true),
                    FooterMenuTitleId = table.Column<int>(nullable: false),
                    ChangeActionEnum = table.Column<int>(nullable: true),
                    VersionStatusEnum = table.Column<int>(nullable: true),
                    FooterMenuItemId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FooterMenuItemVersions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FooterMenuItemVersions_AspNetUsers_ApprovedById",
                        column: x => x.ApprovedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FooterMenuItemVersions_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FooterMenuItemVersions_FooterMenuItem_FooterMenuItemId",
                        column: x => x.FooterMenuItemId,
                        principalTable: "FooterMenuItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FooterMenuItemVersions_FooterMenuTitles_FooterMenuTitleId",
                        column: x => x.FooterMenuTitleId,
                        principalTable: "FooterMenuTitles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FooterMenuItemVersions_ApprovedById",
                table: "FooterMenuItemVersions",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_FooterMenuItemVersions_CreatedById",
                table: "FooterMenuItemVersions",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_FooterMenuItemVersions_FooterMenuItemId",
                table: "FooterMenuItemVersions",
                column: "FooterMenuItemId");

            migrationBuilder.CreateIndex(
                name: "IX_FooterMenuItemVersions_FooterMenuTitleId",
                table: "FooterMenuItemVersions",
                column: "FooterMenuTitleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FooterMenuItemVersions");
        }
    }
}
