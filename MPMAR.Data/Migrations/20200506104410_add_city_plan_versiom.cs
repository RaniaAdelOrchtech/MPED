using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MPMAR.Data.Migrations
{
    public partial class add_city_plan_versiom : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CityPlanVersions",
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
                    IsDeleted = table.Column<bool>(nullable: false),
                    ChangeActionEnum = table.Column<int>(nullable: true),
                    VersionStatusEnum = table.Column<int>(nullable: true),
                    CityPlanId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CityPlanVersions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CityPlanVersions_AspNetUsers_ApprovedById",
                        column: x => x.ApprovedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CityPlanVersions_CityPlan_CityPlanId",
                        column: x => x.CityPlanId,
                        principalTable: "CityPlan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CityPlanVersions_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CityPlanVersions_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CityPlanYearVersions",
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
                    IsDeleted = table.Column<bool>(nullable: false),
                    ChangeActionEnum = table.Column<int>(nullable: true),
                    VersionStatusEnum = table.Column<int>(nullable: true),
                    CityPlanYearId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CityPlanYearVersions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CityPlanYearVersions_AspNetUsers_ApprovedById",
                        column: x => x.ApprovedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CityPlanYearVersions_CityPlan_CityPlanId",
                        column: x => x.CityPlanId,
                        principalTable: "CityPlan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CityPlanYearVersions_CityPlanYear_CityPlanYearId",
                        column: x => x.CityPlanYearId,
                        principalTable: "CityPlanYear",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CityPlanYearVersions_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CityPlanYearVersions_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CityPlanYear_CityPlanId",
                table: "CityPlanYear",
                column: "CityPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_CityPlanVersions_ApprovedById",
                table: "CityPlanVersions",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_CityPlanVersions_CityPlanId",
                table: "CityPlanVersions",
                column: "CityPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_CityPlanVersions_CreatedById",
                table: "CityPlanVersions",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_CityPlanVersions_ModifiedById",
                table: "CityPlanVersions",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_CityPlanYearVersions_ApprovedById",
                table: "CityPlanYearVersions",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_CityPlanYearVersions_CityPlanId",
                table: "CityPlanYearVersions",
                column: "CityPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_CityPlanYearVersions_CityPlanYearId",
                table: "CityPlanYearVersions",
                column: "CityPlanYearId");

            migrationBuilder.CreateIndex(
                name: "IX_CityPlanYearVersions_CreatedById",
                table: "CityPlanYearVersions",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_CityPlanYearVersions_ModifiedById",
                table: "CityPlanYearVersions",
                column: "ModifiedById");

            migrationBuilder.AddForeignKey(
                name: "FK_CityPlanYear_CityPlan_CityPlanId",
                table: "CityPlanYear",
                column: "CityPlanId",
                principalTable: "CityPlan",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CityPlanYear_CityPlan_CityPlanId",
                table: "CityPlanYear");

            migrationBuilder.DropTable(
                name: "CityPlanVersions");

            migrationBuilder.DropTable(
                name: "CityPlanYearVersions");

            migrationBuilder.DropIndex(
                name: "IX_CityPlanYear_CityPlanId",
                table: "CityPlanYear");
        }
    }
}
