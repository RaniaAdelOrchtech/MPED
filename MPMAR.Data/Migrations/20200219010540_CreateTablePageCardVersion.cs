using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MPMAR.Data.Migrations
{
    public partial class CreateTablePageCardVersion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PageSectionCardVersionId",
                table: "PageSectionCards",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PageSectionCardVersion",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    CreatedById = table.Column<string>(maxLength: 450, nullable: true),
                    ApprovalDate = table.Column<DateTime>(nullable: true),
                    ApprovedById = table.Column<string>(maxLength: 450, nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    EnImageAlt = table.Column<string>(nullable: true),
                    ArImageAlt = table.Column<string>(nullable: true),
                    ImageUrl = table.Column<string>(nullable: true),
                    FileUrl = table.Column<string>(nullable: true),
                    PageSectionVersionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PageSectionCardVersion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PageSectionCardVersion_AspNetUsers_ApprovedById",
                        column: x => x.ApprovedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PageSectionCardVersion_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PageSectionCardVersion_PageSectionVersions_PageSectionVersionId",
                        column: x => x.PageSectionVersionId,
                        principalTable: "PageSectionVersions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PageSectionCards_PageSectionCardVersionId",
                table: "PageSectionCards",
                column: "PageSectionCardVersionId",
                unique: true,
                filter: "[PageSectionCardVersionId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PageSectionCardVersion_ApprovedById",
                table: "PageSectionCardVersion",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_PageSectionCardVersion_CreatedById",
                table: "PageSectionCardVersion",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_PageSectionCardVersion_PageSectionVersionId",
                table: "PageSectionCardVersion",
                column: "PageSectionVersionId");

            migrationBuilder.AddForeignKey(
                name: "FK_PageSectionCards_PageSectionCardVersion_PageSectionCardVersionId",
                table: "PageSectionCards",
                column: "PageSectionCardVersionId",
                principalTable: "PageSectionCardVersion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PageSectionCards_PageSectionCardVersion_PageSectionCardVersionId",
                table: "PageSectionCards");

            migrationBuilder.DropTable(
                name: "PageSectionCardVersion");

            migrationBuilder.DropIndex(
                name: "IX_PageSectionCards_PageSectionCardVersionId",
                table: "PageSectionCards");

            migrationBuilder.DropColumn(
                name: "PageSectionCardVersionId",
                table: "PageSectionCards");
        }
    }
}
