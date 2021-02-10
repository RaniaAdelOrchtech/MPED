using Microsoft.EntityFrameworkCore.Migrations;

namespace MPMAR.Analytics.Data.Migrations
{
    public partial class addRGDPGrowthRateVersion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RGDPGrowthRateVersions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DFIndicatorId = table.Column<int>(nullable: false),
                    DFSourceId = table.Column<int>(nullable: false),
                    DFUnitId = table.Column<int>(nullable: false),
                    DFQuarterId = table.Column<int>(nullable: false),
                    DFYearId = table.Column<int>(nullable: false),
                    GrowthRate = table.Column<double>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    ChangeActionEnum = table.Column<int>(nullable: true),
                    VersionStatusEnum = table.Column<int>(nullable: true),
                    RGDPGrowthRateId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RGDPGrowthRateVersions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RGDPGrowthRateVersions_DFIndicators_DFIndicatorId",
                        column: x => x.DFIndicatorId,
                        principalTable: "DFIndicators",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RGDPGrowthRateVersions_DFQuarters_DFQuarterId",
                        column: x => x.DFQuarterId,
                        principalTable: "DFQuarters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RGDPGrowthRateVersions_DFSources_DFSourceId",
                        column: x => x.DFSourceId,
                        principalTable: "DFSources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RGDPGrowthRateVersions_DFUnits_DFUnitId",
                        column: x => x.DFUnitId,
                        principalTable: "DFUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RGDPGrowthRateVersions_DFYears_DFYearId",
                        column: x => x.DFYearId,
                        principalTable: "DFYears",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RGDPGrowthRateVersions_RGDPGrowthRates_RGDPGrowthRateId",
                        column: x => x.RGDPGrowthRateId,
                        principalTable: "RGDPGrowthRates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RGDPGrowthRateVersions_DFIndicatorId",
                table: "RGDPGrowthRateVersions",
                column: "DFIndicatorId");

            migrationBuilder.CreateIndex(
                name: "IX_RGDPGrowthRateVersions_DFQuarterId",
                table: "RGDPGrowthRateVersions",
                column: "DFQuarterId");

            migrationBuilder.CreateIndex(
                name: "IX_RGDPGrowthRateVersions_DFSourceId",
                table: "RGDPGrowthRateVersions",
                column: "DFSourceId");

            migrationBuilder.CreateIndex(
                name: "IX_RGDPGrowthRateVersions_DFUnitId",
                table: "RGDPGrowthRateVersions",
                column: "DFUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_RGDPGrowthRateVersions_DFYearId",
                table: "RGDPGrowthRateVersions",
                column: "DFYearId");

            migrationBuilder.CreateIndex(
                name: "IX_RGDPGrowthRateVersions_RGDPGrowthRateId",
                table: "RGDPGrowthRateVersions",
                column: "RGDPGrowthRateId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RGDPGrowthRateVersions");
        }
    }
}
