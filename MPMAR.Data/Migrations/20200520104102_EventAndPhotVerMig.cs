using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MPMAR.Data.Migrations
{
    public partial class EventAndPhotVerMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_PageEventVersions_Statuses_StatusId",
            //    table: "PageEventVersions");

            //migrationBuilder.DropIndex(
            //    name: "IX_PageEventVersions_StatusId",
            //    table: "PageEventVersions");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "PhotoArchive");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "PageEventVersions");

            migrationBuilder.AddColumn<int>(
                name: "PageRouteId",
                table: "PhotoArchive",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ChangeActionEnum",
                table: "PageEventVersions",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "PageEventVersions",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PageEventId",
                table: "PageEventVersions",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VersionStatusEnum",
                table: "PageEventVersions",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PageEvents",
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
                    EnTitle = table.Column<string>(nullable: true),
                    ArTitle = table.Column<string>(nullable: true),
                    EnDescription = table.Column<string>(nullable: true),
                    ArDescription = table.Column<string>(nullable: true),
                    EnImageAlt = table.Column<string>(nullable: true),
                    ArImageAlt = table.Column<string>(nullable: true),
                    Order = table.Column<int>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    EnUrl = table.Column<string>(nullable: true),
                    ArUrl = table.Column<string>(nullable: true),
                    EnAddress = table.Column<string>(nullable: true),
                    ArAddress = table.Column<string>(nullable: true),
                    EventLocation = table.Column<string>(nullable: true),
                    EventLocationUrl = table.Column<string>(nullable: true),
                    EventDateColor = table.Column<string>(nullable: true),
                    EventCaption = table.Column<string>(nullable: true),
                    EventSocialLinks = table.Column<string>(nullable: true),
                    EventLon = table.Column<decimal>(nullable: true),
                    EventLat = table.Column<decimal>(nullable: true),
                    EventStartDate = table.Column<DateTime>(nullable: true),
                    EventEndDate = table.Column<DateTime>(nullable: true),
                    ShowInHome = table.Column<bool>(nullable: false),
                    PageRouteId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PageEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PageEvents_AspNetUsers_ApprovedById",
                        column: x => x.ApprovedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PageEvents_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PageEvents_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PageEvents_PageRoutes_PageRouteId",
                        column: x => x.PageRouteId,
                        principalTable: "PageRoutes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PhotoArchiveVersions",
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
                    EnPhotoArchiveName = table.Column<string>(nullable: true),
                    ArPhotoArchiveName = table.Column<string>(nullable: true),
                    EnPhotoArchiveDesc = table.Column<string>(nullable: true),
                    ArPhotoArchiveDesc = table.Column<string>(nullable: true),
                    StatusId = table.Column<int>(nullable: true),
                    ImageUrl = table.Column<string>(nullable: true),
                    EnPhotoArchiveType = table.Column<string>(nullable: true),
                    ArPhotoArchiveType = table.Column<string>(nullable: true),
                    Order = table.Column<int>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Date = table.Column<DateTime>(nullable: true),
                    ChangeActionEnum = table.Column<int>(nullable: true),
                    VersionStatusEnum = table.Column<int>(nullable: true),
                    PhotoArchiveId = table.Column<int>(nullable: true),
                    PageRouteVersionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhotoArchiveVersions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhotoArchiveVersions_AspNetUsers_ApprovedById",
                        column: x => x.ApprovedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PhotoArchiveVersions_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PhotoArchiveVersions_PageRouteVersions_PageRouteVersionId",
                        column: x => x.PageRouteVersionId,
                        principalTable: "PageRouteVersions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PhotoArchiveVersions_PhotoArchive_PhotoArchiveId",
                        column: x => x.PhotoArchiveId,
                        principalTable: "PhotoArchive",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PhotoArchive_PageRouteId",
                table: "PhotoArchive",
                column: "PageRouteId");

            migrationBuilder.CreateIndex(
                name: "IX_PageEventVersions_PageEventId",
                table: "PageEventVersions",
                column: "PageEventId");

            migrationBuilder.CreateIndex(
                name: "IX_PageEvents_ApprovedById",
                table: "PageEvents",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_PageEvents_CreatedById",
                table: "PageEvents",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_PageEvents_ModifiedById",
                table: "PageEvents",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_PageEvents_PageRouteId",
                table: "PageEvents",
                column: "PageRouteId");

            migrationBuilder.CreateIndex(
                name: "IX_PhotoArchiveVersions_ApprovedById",
                table: "PhotoArchiveVersions",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_PhotoArchiveVersions_CreatedById",
                table: "PhotoArchiveVersions",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_PhotoArchiveVersions_PageRouteVersionId",
                table: "PhotoArchiveVersions",
                column: "PageRouteVersionId");

            migrationBuilder.CreateIndex(
                name: "IX_PhotoArchiveVersions_PhotoArchiveId",
                table: "PhotoArchiveVersions",
                column: "PhotoArchiveId");

            migrationBuilder.AddForeignKey(
                name: "FK_PageEventVersions_PageEvents_PageEventId",
                table: "PageEventVersions",
                column: "PageEventId",
                principalTable: "PageEvents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PhotoArchive_PageRoutes_PageRouteId",
                table: "PhotoArchive",
                column: "PageRouteId",
                principalTable: "PageRoutes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PageEventVersions_PageEvents_PageEventId",
                table: "PageEventVersions");

            migrationBuilder.DropForeignKey(
                name: "FK_PhotoArchive_PageRoutes_PageRouteId",
                table: "PhotoArchive");

            migrationBuilder.DropTable(
                name: "PageEvents");

            migrationBuilder.DropTable(
                name: "PhotoArchiveVersions");

            migrationBuilder.DropIndex(
                name: "IX_PhotoArchive_PageRouteId",
                table: "PhotoArchive");

            migrationBuilder.DropIndex(
                name: "IX_PageEventVersions_PageEventId",
                table: "PageEventVersions");

            migrationBuilder.DropColumn(
                name: "PageRouteId",
                table: "PhotoArchive");

            migrationBuilder.DropColumn(
                name: "ChangeActionEnum",
                table: "PageEventVersions");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "PageEventVersions");

            migrationBuilder.DropColumn(
                name: "PageEventId",
                table: "PageEventVersions");

            migrationBuilder.DropColumn(
                name: "VersionStatusEnum",
                table: "PageEventVersions");

            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "PhotoArchive",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "PageEventVersions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PageEventVersions_StatusId",
                table: "PageEventVersions",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_PageEventVersions_Statuses_StatusId",
                table: "PageEventVersions",
                column: "StatusId",
                principalTable: "Statuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
