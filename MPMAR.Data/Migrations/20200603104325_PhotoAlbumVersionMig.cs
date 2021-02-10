using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MPMAR.Data.Migrations
{
    public partial class PhotoAlbumVersionMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "PhotosAlbum");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "PhotoArchiveVersions");

            migrationBuilder.CreateTable(
                name: "PhotosAlbumVersions",
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
                    PhotoArchiveVersionId = table.Column<int>(nullable: false),
                    PhotosAlbumId = table.Column<int>(nullable: true),
                    ImageName = table.Column<string>(nullable: true),
                    ImagePath = table.Column<string>(nullable: true),
                    EnPhotosAlbumName = table.Column<string>(nullable: true),
                    ArPhotosAlbumName = table.Column<string>(nullable: true),
                    EnPhotosAlbumDesc = table.Column<string>(nullable: true),
                    ArPhotosAlbumDesc = table.Column<string>(nullable: true),
                    Order = table.Column<int>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Date = table.Column<DateTime>(nullable: true),
                    ChangeActionEnum = table.Column<int>(nullable: true),
                    VersionStatusEnum = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhotosAlbumVersions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhotosAlbumVersions_AspNetUsers_ApprovedById",
                        column: x => x.ApprovedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PhotosAlbumVersions_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PhotosAlbumVersions_PhotoArchiveVersions_PhotoArchiveVersionId",
                        column: x => x.PhotoArchiveVersionId,
                        principalTable: "PhotoArchiveVersions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PhotosAlbumVersions_PhotosAlbum_PhotosAlbumId",
                        column: x => x.PhotosAlbumId,
                        principalTable: "PhotosAlbum",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PhotosAlbum_PhotoArchiveId",
                table: "PhotosAlbum",
                column: "PhotoArchiveId");

            migrationBuilder.CreateIndex(
                name: "IX_PhotosAlbumVersions_ApprovedById",
                table: "PhotosAlbumVersions",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_PhotosAlbumVersions_CreatedById",
                table: "PhotosAlbumVersions",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_PhotosAlbumVersions_PhotoArchiveVersionId",
                table: "PhotosAlbumVersions",
                column: "PhotoArchiveVersionId");

            migrationBuilder.CreateIndex(
                name: "IX_PhotosAlbumVersions_PhotosAlbumId",
                table: "PhotosAlbumVersions",
                column: "PhotosAlbumId");

            migrationBuilder.AddForeignKey(
                name: "FK_PhotosAlbum_PhotoArchive_PhotoArchiveId",
                table: "PhotosAlbum",
                column: "PhotoArchiveId",
                principalTable: "PhotoArchive",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhotosAlbum_PhotoArchive_PhotoArchiveId",
                table: "PhotosAlbum");

            migrationBuilder.DropTable(
                name: "PhotosAlbumVersions");

            migrationBuilder.DropIndex(
                name: "IX_PhotosAlbum_PhotoArchiveId",
                table: "PhotosAlbum");

            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "PhotosAlbum",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "PhotoArchiveVersions",
                type: "int",
                nullable: true);
        }
    }
}
