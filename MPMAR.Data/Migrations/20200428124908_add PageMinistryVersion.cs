using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MPMAR.Data.Migrations
{
    public partial class addPageMinistryVersion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PageMinistry_PageRouteVersions",
                table: "PageMinistry");

            //migrationBuilder.DropIndex(
            //    name: "IX_PageMinistry_PageRouteVersionId",
            //    table: "PageMinistry");

            migrationBuilder.DropColumn(
                name: "PageRouteVersionId",
                table: "PageMinistry");

            migrationBuilder.AddColumn<int>(
                name: "PageRouteId",
                table: "PageMinistry",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PageMinistryVersions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    CreatedById = table.Column<string>(maxLength: 450, nullable: true),
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
                    Order = table.Column<int>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    NavItemId = table.Column<int>(nullable: true),
                    ArName = table.Column<string>(nullable: true),
                    EnName = table.Column<string>(nullable: true),
                    StatusId = table.Column<int>(nullable: true),
                    ArContent = table.Column<string>(nullable: true),
                    EnContent = table.Column<string>(nullable: true),
                    IsHeading = table.Column<bool>(nullable: false),
                    IsSection = table.Column<bool>(nullable: false),
                    IsDobulQuote = table.Column<bool>(nullable: false),
                    PageRouteVersionId = table.Column<int>(nullable: true),
                    ImageUrl = table.Column<string>(nullable: true),
                    ChangeActionEnum = table.Column<int>(nullable: true),
                    VersionStatusEnum = table.Column<int>(nullable: true),
                    PageMinistryId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PageMinistryVersions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PageMinistryVersions_AspNetUsers_ApprovedById",
                        column: x => x.ApprovedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PageMinistryVersions_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PageMinistryVersions_PageMinistry_PageMinistryId",
                        column: x => x.PageMinistryId,
                        principalTable: "PageMinistry",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PageMinistryVersions_PageRouteVersions_PageRouteVersionId",
                        column: x => x.PageRouteVersionId,
                        principalTable: "PageRouteVersions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PageMinistry_PageRouteId",
                table: "PageMinistry",
                column: "PageRouteId");

            migrationBuilder.CreateIndex(
                name: "IX_PageMinistryVersions_ApprovedById",
                table: "PageMinistryVersions",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_PageMinistryVersions_CreatedById",
                table: "PageMinistryVersions",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_PageMinistryVersions_PageMinistryId",
                table: "PageMinistryVersions",
                column: "PageMinistryId");

            migrationBuilder.CreateIndex(
                name: "IX_PageMinistryVersions_PageRouteVersionId",
                table: "PageMinistryVersions",
                column: "PageRouteVersionId");

            migrationBuilder.AddForeignKey(
                name: "FK_PageMinistry_PageRoutes_PageRouteId",
                table: "PageMinistry",
                column: "PageRouteId",
                principalTable: "PageRoutes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PageMinistry_PageRoutes_PageRouteId",
                table: "PageMinistry");

            migrationBuilder.DropTable(
                name: "PageMinistryVersions");

            migrationBuilder.DropIndex(
                name: "IX_PageMinistry_PageRouteId",
                table: "PageMinistry");

            migrationBuilder.DropColumn(
                name: "PageRouteId",
                table: "PageMinistry");

            migrationBuilder.AddColumn<int>(
                name: "PageRouteVersionId",
                table: "PageMinistry",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PageMinistry_PageRouteVersionId",
                table: "PageMinistry",
                column: "PageRouteVersionId");

            migrationBuilder.AddForeignKey(
                name: "FK_PageMinistry_PageRouteVersions_PageRouteVersionId",
                table: "PageMinistry",
                column: "PageRouteVersionId",
                principalTable: "PageRouteVersions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
