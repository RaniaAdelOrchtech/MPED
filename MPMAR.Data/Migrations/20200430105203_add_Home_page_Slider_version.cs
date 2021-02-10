using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MPMAR.Data.Migrations
{
    public partial class add_Home_page_Slider_version : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HomePagePhotoSliderVersions",
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
                    ArTitle = table.Column<string>(nullable: true),
                    EnTitle = table.Column<string>(nullable: true),
                    ArDescription = table.Column<string>(nullable: true),
                    EnDescription = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true),
                    ImageUrl = table.Column<string>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ChangeActionEnum = table.Column<int>(nullable: true),
                    VersionStatusEnum = table.Column<int>(nullable: true),
                    HomePagePhotoSliderId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HomePagePhotoSliderVersions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HomePagePhotoSliderVersions_AspNetUsers_ApprovedById",
                        column: x => x.ApprovedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HomePagePhotoSliderVersions_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HomePagePhotoSliderVersions_homePagePhotoSlider_HomePagePhotoSliderId",
                        column: x => x.HomePagePhotoSliderId,
                        principalTable: "homePagePhotoSlider",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HomePagePhotoSliderVersions_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HomePagePhotoSliderVersions_ApprovedById",
                table: "HomePagePhotoSliderVersions",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_HomePagePhotoSliderVersions_CreatedById",
                table: "HomePagePhotoSliderVersions",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_HomePagePhotoSliderVersions_HomePagePhotoSliderId",
                table: "HomePagePhotoSliderVersions",
                column: "HomePagePhotoSliderId");

            migrationBuilder.CreateIndex(
                name: "IX_HomePagePhotoSliderVersions_ModifiedById",
                table: "HomePagePhotoSliderVersions",
                column: "ModifiedById");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HomePagePhotoSliderVersions");
        }
    }
}
