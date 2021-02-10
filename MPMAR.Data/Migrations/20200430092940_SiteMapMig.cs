using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MPMAR.Data.Migrations
{
    public partial class SiteMapMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SiteMap",
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
                    EnContent = table.Column<string>(nullable: true),
                    ArContent = table.Column<string>(nullable: true),
                    StatusId = table.Column<int>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SiteMap", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SiteMap_AspNetUsers_ApprovedById",
                        column: x => x.ApprovedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SiteMap_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SiteMap_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SiteMap_ApprovedById",
                table: "SiteMap",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_SiteMap_CreatedById",
                table: "SiteMap",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_SiteMap_ModifiedById",
                table: "SiteMap",
                column: "ModifiedById");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SiteMap");
        }
    }
}
