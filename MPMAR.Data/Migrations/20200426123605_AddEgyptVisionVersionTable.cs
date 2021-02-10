using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MPMAR.Data.Migrations
{
    public partial class AddEgyptVisionVersionTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EgyptVisionVersions",
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
                    SeoTitleEN = table.Column<string>(nullable: true),
                    SeoTitleAR = table.Column<string>(nullable: true),
                    SeoDescriptionEN = table.Column<string>(nullable: true),
                    SeoDescriptionAR = table.Column<string>(nullable: true),
                    SeoOgTitleEN = table.Column<string>(nullable: true),
                    SeoOgTitleAR = table.Column<string>(nullable: true),
                    SeoTwitterCardEN = table.Column<string>(nullable: true),
                    SeoTwitterCardAR = table.Column<string>(nullable: true),
                    PageRouteVersionId = table.Column<int>(nullable: false),
                    EnEgyptVisionName = table.Column<string>(nullable: true),
                    ArEgyptVisionName = table.Column<string>(nullable: true),
                    EnEgyptVisionSmallDesc = table.Column<string>(nullable: true),
                    ArEgyptVisionSmallDesc = table.Column<string>(nullable: true),
                    EnEgyptVisionDesc = table.Column<string>(nullable: true),
                    ArEgyptVisionDesc = table.Column<string>(nullable: true),
                    StatusId = table.Column<int>(nullable: true),
                    EnImagePath = table.Column<string>(nullable: true),
                    ArImagePath = table.Column<string>(nullable: true),
                    BgColor = table.Column<string>(nullable: true),
                    LineColor = table.Column<string>(nullable: true),
                    ImagePositionIsRight = table.Column<bool>(nullable: false),
                    Order = table.Column<int>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ChangeActionEnum = table.Column<int>(nullable: true),
                    VersionStatusEnum = table.Column<int>(nullable: true),
                    EgyptVisionId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EgyptVisionVersions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EgyptVisionVersions_AspNetUsers_ApprovedById",
                        column: x => x.ApprovedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EgyptVisionVersions_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EgyptVisionVersions_EgyptVision_EgyptVisionId",
                        column: x => x.EgyptVisionId,
                        principalTable: "EgyptVision",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EgyptVisionVersions_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EgyptVisionVersions_ApprovedById",
                table: "EgyptVisionVersions",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_EgyptVisionVersions_CreatedById",
                table: "EgyptVisionVersions",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_EgyptVisionVersions_EgyptVisionId",
                table: "EgyptVisionVersions",
                column: "EgyptVisionId");

            migrationBuilder.CreateIndex(
                name: "IX_EgyptVisionVersions_ModifiedById",
                table: "EgyptVisionVersions",
                column: "ModifiedById");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EgyptVisionVersions");
        }
    }
}
