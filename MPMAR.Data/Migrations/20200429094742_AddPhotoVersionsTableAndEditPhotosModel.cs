using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MPMAR.Data.Migrations
{
    public partial class AddPhotoVersionsTableAndEditPhotosModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovalDate",
                table: "homePagePhotos",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApprovedById",
                table: "homePagePhotos",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "homePagePhotos",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "homePagePhotos",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModificationDate",
                table: "homePagePhotos",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedById",
                table: "homePagePhotos",
                maxLength: 450,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CityPlan",
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
                    EnPageDescription = table.Column<string>(nullable: true),
                    ArPageDescription = table.Column<string>(nullable: true),
                    EnAlexandria = table.Column<string>(nullable: true),
                    ArAlexandria = table.Column<string>(nullable: true),
                    EnAswan = table.Column<string>(nullable: true),
                    ArAswan = table.Column<string>(nullable: true),
                    EnAsyut = table.Column<string>(nullable: true),
                    ArAsyut = table.Column<string>(nullable: true),
                    EnBeheira = table.Column<string>(nullable: true),
                    ArBeheira = table.Column<string>(nullable: true),
                    EnBeniSuef = table.Column<string>(nullable: true),
                    ArBeniSuef = table.Column<string>(nullable: true),
                    EnCairo = table.Column<string>(nullable: true),
                    ArCairo = table.Column<string>(nullable: true),
                    EnDakahlia = table.Column<string>(nullable: true),
                    ArDakahlia = table.Column<string>(nullable: true),
                    EnDamietta = table.Column<string>(nullable: true),
                    ArDamietta = table.Column<string>(nullable: true),
                    EnFaiyum = table.Column<string>(nullable: true),
                    ArFaiyum = table.Column<string>(nullable: true),
                    EnGharbia = table.Column<string>(nullable: true),
                    ArGharbia = table.Column<string>(nullable: true),
                    EnGiza = table.Column<string>(nullable: true),
                    ArGiza = table.Column<string>(nullable: true),
                    EnIsmailia = table.Column<string>(nullable: true),
                    ArIsmailia = table.Column<string>(nullable: true),
                    EnKafrElSheikh = table.Column<string>(nullable: true),
                    ArKafrElSheikh = table.Column<string>(nullable: true),
                    EnLuxor = table.Column<string>(nullable: true),
                    ArLuxor = table.Column<string>(nullable: true),
                    EnMatruh = table.Column<string>(nullable: true),
                    ArMatruh = table.Column<string>(nullable: true),
                    EnMinya = table.Column<string>(nullable: true),
                    ArMinya = table.Column<string>(nullable: true),
                    EnMonufia = table.Column<string>(nullable: true),
                    ArMonufia = table.Column<string>(nullable: true),
                    EnNewValley = table.Column<string>(nullable: true),
                    ArNewValley = table.Column<string>(nullable: true),
                    EnNorthSinai = table.Column<string>(nullable: true),
                    ArNorthSinai = table.Column<string>(nullable: true),
                    EnPortSaid = table.Column<string>(nullable: true),
                    ArPortSaid = table.Column<string>(nullable: true),
                    EnQalyubia = table.Column<string>(nullable: true),
                    ArQalyubia = table.Column<string>(nullable: true),
                    EnQena = table.Column<string>(nullable: true),
                    ArQena = table.Column<string>(nullable: true),
                    EnRedSea = table.Column<string>(nullable: true),
                    ArRedSea = table.Column<string>(nullable: true),
                    EnSharqia = table.Column<string>(nullable: true),
                    ArSharqia = table.Column<string>(nullable: true),
                    EnSohag = table.Column<string>(nullable: true),
                    ArSohag = table.Column<string>(nullable: true),
                    EnSouthSinai = table.Column<string>(nullable: true),
                    ArSouthSinai = table.Column<string>(nullable: true),
                    EnSuez = table.Column<string>(nullable: true),
                    ArSuez = table.Column<string>(nullable: true),
                    StatusId = table.Column<int>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CityPlan", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CityPlan_AspNetUsers_ApprovedById",
                        column: x => x.ApprovedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CityPlan_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CityPlan_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CityPlanYear",
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
                    CityPlanId = table.Column<int>(nullable: false),
                    GovName = table.Column<string>(nullable: true),
                    GovYear = table.Column<string>(nullable: true),
                    IsMapActive = table.Column<bool>(nullable: false),
                    EnFileUrl = table.Column<string>(nullable: true),
                    ArFileUrl = table.Column<string>(nullable: true),
                    StatusId = table.Column<int>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CityPlanYear", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CityPlanYear_AspNetUsers_ApprovedById",
                        column: x => x.ApprovedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CityPlanYear_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CityPlanYear_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DFRegion",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    NameAr = table.Column<string>(nullable: true),
                    NameEn = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DFRegion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HomePagePhotoVersions",
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
                    ArDescription = table.Column<string>(nullable: true),
                    EnTitle = table.Column<string>(nullable: true),
                    EnDescription = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ChangeActionEnum = table.Column<int>(nullable: true),
                    VersionStatusEnum = table.Column<int>(nullable: true),
                    HomePagePhotoId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HomePagePhotoVersions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HomePagePhotoVersions_AspNetUsers_ApprovedById",
                        column: x => x.ApprovedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HomePagePhotoVersions_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HomePagePhotoVersions_homePagePhotos_HomePagePhotoId",
                        column: x => x.HomePagePhotoId,
                        principalTable: "homePagePhotos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HomePagePhotoVersions_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DFGovernorates",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    NameAr = table.Column<string>(nullable: true),
                    NameEn = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DFRegionId = table.Column<int>(nullable: true),
                    isTotal = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DFGovernorates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DFGovernorates_DFRegion_DFRegionId",
                        column: x => x.DFRegionId,
                        principalTable: "DFRegion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });
               
            migrationBuilder.CreateIndex(
                name: "IX_homePagePhotos_ApprovedById",
                table: "homePagePhotos",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_homePagePhotos_CreatedById",
                table: "homePagePhotos",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_homePagePhotos_ModifiedById",
                table: "homePagePhotos",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_CityPlan_ApprovedById",
                table: "CityPlan",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_CityPlan_CreatedById",
                table: "CityPlan",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_CityPlan_ModifiedById",
                table: "CityPlan",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_CityPlanYear_ApprovedById",
                table: "CityPlanYear",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_CityPlanYear_CreatedById",
                table: "CityPlanYear",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_CityPlanYear_ModifiedById",
                table: "CityPlanYear",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_DFGovernorates_DFRegionId",
                table: "DFGovernorates",
                column: "DFRegionId");

            migrationBuilder.CreateIndex(
                name: "IX_HomePagePhotoVersions_ApprovedById",
                table: "HomePagePhotoVersions",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_HomePagePhotoVersions_CreatedById",
                table: "HomePagePhotoVersions",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_HomePagePhotoVersions_HomePagePhotoId",
                table: "HomePagePhotoVersions",
                column: "HomePagePhotoId");

            migrationBuilder.CreateIndex(
                name: "IX_HomePagePhotoVersions_ModifiedById",
                table: "HomePagePhotoVersions",
                column: "ModifiedById");

            migrationBuilder.AddForeignKey(
                name: "FK_homePagePhotos_AspNetUsers_ApprovedById",
                table: "homePagePhotos",
                column: "ApprovedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_homePagePhotos_AspNetUsers_CreatedById",
                table: "homePagePhotos",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_homePagePhotos_AspNetUsers_ModifiedById",
                table: "homePagePhotos",
                column: "ModifiedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_homePagePhotos_AspNetUsers_ApprovedById",
                table: "homePagePhotos");

            migrationBuilder.DropForeignKey(
                name: "FK_homePagePhotos_AspNetUsers_CreatedById",
                table: "homePagePhotos");

            migrationBuilder.DropForeignKey(
                name: "FK_homePagePhotos_AspNetUsers_ModifiedById",
                table: "homePagePhotos");

            migrationBuilder.DropTable(
                name: "CityPlan");

            migrationBuilder.DropTable(
                name: "CityPlanYear");

            migrationBuilder.DropTable(
                name: "DFGovernorates");

            migrationBuilder.DropTable(
                name: "HomePagePhotoVersions");

            migrationBuilder.DropTable(
                name: "DFRegion");

            migrationBuilder.DropIndex(
                name: "IX_homePagePhotos_ApprovedById",
                table: "homePagePhotos");

            migrationBuilder.DropIndex(
                name: "IX_homePagePhotos_CreatedById",
                table: "homePagePhotos");

            migrationBuilder.DropIndex(
                name: "IX_homePagePhotos_ModifiedById",
                table: "homePagePhotos");

            migrationBuilder.DropColumn(
                name: "ApprovalDate",
                table: "homePagePhotos");

            migrationBuilder.DropColumn(
                name: "ApprovedById",
                table: "homePagePhotos");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "homePagePhotos");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "homePagePhotos");

            migrationBuilder.DropColumn(
                name: "ModificationDate",
                table: "homePagePhotos");

            migrationBuilder.DropColumn(
                name: "ModifiedById",
                table: "homePagePhotos");
        }
    }
}
