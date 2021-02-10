using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MPMAR.Data.Migrations
{
    public partial class AddLogoLinkAndLogoLinkVersionHomePageTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HomePageLogoLinks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    CreatedById = table.Column<string>(maxLength: 450, nullable: true),
                    ModificationDate = table.Column<DateTime>(nullable: true),
                    ModifiedById = table.Column<string>(maxLength: 450, nullable: true),
                    ApprovalDate = table.Column<DateTime>(nullable: true),
                    ApprovedById = table.Column<string>(maxLength: 450, nullable: true),
                    ImageUrl = table.Column<string>(nullable: false),
                    ArTitle = table.Column<string>(nullable: true),
                    EnTitle = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HomePageLogoLinks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HomePageLogoLinks_AspNetUsers_ApprovedById",
                        column: x => x.ApprovedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HomePageLogoLinks_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HomePageLogoLinks_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HomePageLogoLinkVersions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    CreatedById = table.Column<string>(maxLength: 450, nullable: true),
                    ModificationDate = table.Column<DateTime>(nullable: true),
                    ModifiedById = table.Column<string>(maxLength: 450, nullable: true),
                    ApprovalDate = table.Column<DateTime>(nullable: true),
                    ApprovedById = table.Column<string>(maxLength: 450, nullable: true),
                    ImageUrl = table.Column<string>(nullable: false),
                    ArTitle = table.Column<string>(nullable: true),
                    EnTitle = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true),
                    ChangeActionEnum = table.Column<int>(nullable: true),
                    VersionStatusEnum = table.Column<int>(nullable: true),
                    HomePageLogoLinkId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HomePageLogoLinkVersions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HomePageLogoLinkVersions_AspNetUsers_ApprovedById",
                        column: x => x.ApprovedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HomePageLogoLinkVersions_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HomePageLogoLinkVersions_HomePageLogoLinks_HomePageLogoLinkId",
                        column: x => x.HomePageLogoLinkId,
                        principalTable: "HomePageLogoLinks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HomePageLogoLinkVersions_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HomePageLogoLinks_ApprovedById",
                table: "HomePageLogoLinks",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_HomePageLogoLinks_CreatedById",
                table: "HomePageLogoLinks",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_HomePageLogoLinks_ModifiedById",
                table: "HomePageLogoLinks",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_HomePageLogoLinkVersions_ApprovedById",
                table: "HomePageLogoLinkVersions",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_HomePageLogoLinkVersions_CreatedById",
                table: "HomePageLogoLinkVersions",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_HomePageLogoLinkVersions_HomePageLogoLinkId",
                table: "HomePageLogoLinkVersions",
                column: "HomePageLogoLinkId");

            migrationBuilder.CreateIndex(
                name: "IX_HomePageLogoLinkVersions_ModifiedById",
                table: "HomePageLogoLinkVersions",
                column: "ModifiedById");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HomePageLogoLinkVersions");

            migrationBuilder.DropTable(
                name: "HomePageLogoLinks");
        }
    }
}
