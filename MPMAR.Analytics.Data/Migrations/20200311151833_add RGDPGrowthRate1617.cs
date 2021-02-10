using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MPMAR.Analytics.Data.Migrations
{
    public partial class addRGDPGrowthRate1617 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RGDPGrowthRates1617",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DFIndicatorId = table.Column<int>(nullable: false),
                    DFSourceId = table.Column<int>(nullable: false),
                    DFUnitId = table.Column<int>(nullable: false),
                    DFQuarterId = table.Column<int>(nullable: false),
                    DFYearFiscalId = table.Column<int>(nullable: false),
                    Value = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RGDPGrowthRates1617", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RGDPGrowthRates1617_DFIndicators_DFIndicatorId",
                        column: x => x.DFIndicatorId,
                        principalTable: "DFIndicators",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RGDPGrowthRates1617_DFQuarters_DFQuarterId",
                        column: x => x.DFQuarterId,
                        principalTable: "DFQuarters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RGDPGrowthRates1617_DFSources_DFSourceId",
                        column: x => x.DFSourceId,
                        principalTable: "DFSources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RGDPGrowthRates1617_DFUnits_DFUnitId",
                        column: x => x.DFUnitId,
                        principalTable: "DFUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RGDPGrowthRates1617_DFYears_DFYearFiscalId",
                        column: x => x.DFYearFiscalId,
                        principalTable: "DFYears",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RGDPGrowthRates1617_DFIndicatorId",
                table: "RGDPGrowthRates1617",
                column: "DFIndicatorId");

            migrationBuilder.CreateIndex(
                name: "IX_RGDPGrowthRates1617_DFQuarterId",
                table: "RGDPGrowthRates1617",
                column: "DFQuarterId");

            migrationBuilder.CreateIndex(
                name: "IX_RGDPGrowthRates1617_DFSourceId",
                table: "RGDPGrowthRates1617",
                column: "DFSourceId");

            migrationBuilder.CreateIndex(
                name: "IX_RGDPGrowthRates1617_DFUnitId",
                table: "RGDPGrowthRates1617",
                column: "DFUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_RGDPGrowthRates1617_DFYearFiscalId",
                table: "RGDPGrowthRates1617",
                column: "DFYearFiscalId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RGDPGrowthRates1617");
        }
    }
}
