using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MPMAR.Data.Migrations
{
    public partial class AddContentPageContentTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DynamicPageContentVersions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    CreatedById = table.Column<string>(maxLength: 450, nullable: true),
                    ApprovalDate = table.Column<DateTime>(nullable: true),
                    ApprovedById = table.Column<string>(maxLength: 450, nullable: true),
                    IsIgnored = table.Column<bool>(nullable: false),
                    Order = table.Column<int>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    IsApproved = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DynamicPageContentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DynamicPageContentVersions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DynamicPageContentVersions_AspNetUsers_ApprovedById",
                        column: x => x.ApprovedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DynamicPageContentVersions_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DynamicPageContentVersions_DynamicPageContents_DynamicPageContentId",
                        column: x => x.DynamicPageContentId,
                        principalTable: "DynamicPageContents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DynamicPageContentVersions_ApprovedById",
                table: "DynamicPageContentVersions",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_DynamicPageContentVersions_CreatedById",
                table: "DynamicPageContentVersions",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_DynamicPageContentVersions_DynamicPageContentId",
                table: "DynamicPageContentVersions",
                column: "DynamicPageContentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DynamicPageContentVersions");
        }
    }
}
