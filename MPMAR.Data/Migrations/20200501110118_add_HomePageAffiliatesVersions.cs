using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MPMAR.Data.Migrations
{
    public partial class add_HomePageAffiliatesVersions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HomePageAffiliatesVersions",
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
                    ImageUrl = table.Column<string>(nullable: true),
                    ArDescription = table.Column<string>(nullable: false),
                    EnDescription = table.Column<string>(nullable: false),
                    Url = table.Column<string>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ChangeActionEnum = table.Column<int>(nullable: true),
                    VersionStatusEnum = table.Column<int>(nullable: true),
                    HomePageAffiliatesId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HomePageAffiliatesVersions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HomePageAffiliatesVersions_AspNetUsers_ApprovedById",
                        column: x => x.ApprovedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HomePageAffiliatesVersions_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HomePageAffiliatesVersions_HomePageAffiliates_HomePageAffiliatesId",
                        column: x => x.HomePageAffiliatesId,
                        principalTable: "HomePageAffiliates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HomePageAffiliatesVersions_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HomePageAffiliatesVersions_ApprovedById",
                table: "HomePageAffiliatesVersions",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_HomePageAffiliatesVersions_CreatedById",
                table: "HomePageAffiliatesVersions",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_HomePageAffiliatesVersions_HomePageAffiliatesId",
                table: "HomePageAffiliatesVersions",
                column: "HomePageAffiliatesId");

            migrationBuilder.CreateIndex(
                name: "IX_HomePageAffiliatesVersions_ModifiedById",
                table: "HomePageAffiliatesVersions",
                column: "ModifiedById");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HomePageAffiliatesVersions");
        }
    }
}
