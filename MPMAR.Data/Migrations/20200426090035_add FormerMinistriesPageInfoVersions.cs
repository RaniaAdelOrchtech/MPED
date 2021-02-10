using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MPMAR.Data.Migrations
{
    public partial class addFormerMinistriesPageInfoVersions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FormerMinistriesPageInfoVersions",
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
                    Title1Ar = table.Column<string>(maxLength: 100, nullable: false),
                    Title1En = table.Column<string>(maxLength: 100, nullable: false),
                    DescriptionAr = table.Column<string>(maxLength: 1500, nullable: false),
                    DescriptionEn = table.Column<string>(maxLength: 1500, nullable: false),
                    Title2Ar = table.Column<string>(maxLength: 100, nullable: false),
                    Title2En = table.Column<string>(maxLength: 100, nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ChangeActionEnum = table.Column<int>(nullable: true),
                    VersionStatusEnum = table.Column<int>(nullable: true),
                    FormerMinistriesPageInfoId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormerMinistriesPageInfoVersions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FormerMinistriesPageInfoVersions_AspNetUsers_ApprovedById",
                        column: x => x.ApprovedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FormerMinistriesPageInfoVersions_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FormerMinistriesPageInfoVersions_FormerMinistriesPageInfos_FormerMinistriesPageInfoId",
                        column: x => x.FormerMinistriesPageInfoId,
                        principalTable: "FormerMinistriesPageInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FormerMinistriesPageInfoVersions_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FormerMinistriesPageInfoVersions_ApprovedById",
                table: "FormerMinistriesPageInfoVersions",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_FormerMinistriesPageInfoVersions_CreatedById",
                table: "FormerMinistriesPageInfoVersions",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_FormerMinistriesPageInfoVersions_FormerMinistriesPageInfoId",
                table: "FormerMinistriesPageInfoVersions",
                column: "FormerMinistriesPageInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_FormerMinistriesPageInfoVersions_ModifiedById",
                table: "FormerMinistriesPageInfoVersions",
                column: "ModifiedById");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FormerMinistriesPageInfoVersions");
        }
    }
}
