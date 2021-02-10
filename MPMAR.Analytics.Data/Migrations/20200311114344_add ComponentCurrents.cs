using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MPMAR.Analytics.Data.Migrations
{
    public partial class addComponentCurrents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ComponentCurrents",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DFIndicatorId = table.Column<int>(nullable: false),
                    DFSourceId = table.Column<int>(nullable: false),
                    DFUnitId = table.Column<int>(nullable: false),
                    DFQuarterId = table.Column<int>(nullable: false),
                    DFYearFiscalId = table.Column<int>(nullable: false),
                    PrivateConsumption = table.Column<double>(nullable: true),
                    GovernmentConsumption = table.Column<double>(nullable: true),
                    GrossCapitalFormation = table.Column<double>(nullable: true),
                    ExportsOfGoodsAndServices = table.Column<double>(nullable: true),
                    ImportsOfGoodsAndServices = table.Column<double>(nullable: true),
                    TotalGrossDomesticProductAtMarketPrices = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComponentCurrents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComponentCurrents_DFIndicators_DFIndicatorId",
                        column: x => x.DFIndicatorId,
                        principalTable: "DFIndicators",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComponentCurrents_DFQuarters_DFQuarterId",
                        column: x => x.DFQuarterId,
                        principalTable: "DFQuarters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComponentCurrents_DFSources_DFSourceId",
                        column: x => x.DFSourceId,
                        principalTable: "DFSources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComponentCurrents_DFUnits_DFUnitId",
                        column: x => x.DFUnitId,
                        principalTable: "DFUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComponentCurrents_DFYears_DFYearFiscalId",
                        column: x => x.DFYearFiscalId,
                        principalTable: "DFYears",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ComponentCurrents_DFIndicatorId",
                table: "ComponentCurrents",
                column: "DFIndicatorId");

            migrationBuilder.CreateIndex(
                name: "IX_ComponentCurrents_DFQuarterId",
                table: "ComponentCurrents",
                column: "DFQuarterId");

            migrationBuilder.CreateIndex(
                name: "IX_ComponentCurrents_DFSourceId",
                table: "ComponentCurrents",
                column: "DFSourceId");

            migrationBuilder.CreateIndex(
                name: "IX_ComponentCurrents_DFUnitId",
                table: "ComponentCurrents",
                column: "DFUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_ComponentCurrents_DFYearFiscalId",
                table: "ComponentCurrents",
                column: "DFYearFiscalId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComponentCurrents");
        }
    }
}
