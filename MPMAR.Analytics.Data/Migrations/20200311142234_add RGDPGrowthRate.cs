using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MPMAR.Analytics.Data.Migrations
{
    public partial class addRGDPGrowthRate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RGDPGrowthRates",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DFIndicatorId = table.Column<int>(nullable: false),
                    DFSourceId = table.Column<int>(nullable: false),
                    DFUnitId = table.Column<int>(nullable: false),
                    DFQuarterId = table.Column<int>(nullable: false),
                    DFYearId = table.Column<int>(nullable: false),
                    GrowthRate = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RGDPGrowthRates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RGDPGrowthRates_DFIndicators_DFIndicatorId",
                        column: x => x.DFIndicatorId,
                        principalTable: "DFIndicators",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RGDPGrowthRates_DFQuarters_DFQuarterId",
                        column: x => x.DFQuarterId,
                        principalTable: "DFQuarters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RGDPGrowthRates_DFSources_DFSourceId",
                        column: x => x.DFSourceId,
                        principalTable: "DFSources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RGDPGrowthRates_DFUnits_DFUnitId",
                        column: x => x.DFUnitId,
                        principalTable: "DFUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RGDPGrowthRates_DFYears_DFYearId",
                        column: x => x.DFYearId,
                        principalTable: "DFYears",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RGDPGrowthRates_DFIndicatorId",
                table: "RGDPGrowthRates",
                column: "DFIndicatorId");

            migrationBuilder.CreateIndex(
                name: "IX_RGDPGrowthRates_DFQuarterId",
                table: "RGDPGrowthRates",
                column: "DFQuarterId");

            migrationBuilder.CreateIndex(
                name: "IX_RGDPGrowthRates_DFSourceId",
                table: "RGDPGrowthRates",
                column: "DFSourceId");

            migrationBuilder.CreateIndex(
                name: "IX_RGDPGrowthRates_DFUnitId",
                table: "RGDPGrowthRates",
                column: "DFUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_RGDPGrowthRates_DFYearId",
                table: "RGDPGrowthRates",
                column: "DFYearId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RGDPGrowthRates");
        }
    }
}
