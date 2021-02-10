using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MPMAR.Data.Migrations
{
    public partial class addFooterMenuTitletable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FooterMenuTitleId",
                table: "FooterMenuItem",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "FooterMenuTitles",
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
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FooterMenuTitles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FooterMenuTitles_AspNetUsers_ApprovedById",
                        column: x => x.ApprovedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FooterMenuTitles_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FooterMenuItem_FooterMenuTitleId",
                table: "FooterMenuItem",
                column: "FooterMenuTitleId");

            migrationBuilder.CreateIndex(
                name: "IX_FooterMenuTitles_ApprovedById",
                table: "FooterMenuTitles",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_FooterMenuTitles_CreatedById",
                table: "FooterMenuTitles",
                column: "CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_FooterMenuItem_FooterMenuTitles_FooterMenuTitleId",
                table: "FooterMenuItem",
                column: "FooterMenuTitleId",
                principalTable: "FooterMenuTitles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FooterMenuItem_FooterMenuTitles_FooterMenuTitleId",
                table: "FooterMenuItem");

            migrationBuilder.DropTable(
                name: "FooterMenuTitles");

            migrationBuilder.DropIndex(
                name: "IX_FooterMenuItem_FooterMenuTitleId",
                table: "FooterMenuItem");

            migrationBuilder.DropColumn(
                name: "FooterMenuTitleId",
                table: "FooterMenuItem");
        }
    }
}
