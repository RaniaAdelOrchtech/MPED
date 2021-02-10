using Microsoft.EntityFrameworkCore.Migrations;

namespace MPMAR.Analytics.Data.Migrations
{
    public partial class addComponentConstantVersion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
          

            migrationBuilder.CreateTable(
                name: "ComponentConstantVersions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
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
                    TotalGrossDomesticProductAtMarketPrices = table.Column<double>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    ChangeActionEnum = table.Column<int>(nullable: true),
                    VersionStatusEnum = table.Column<int>(nullable: true),
                    ComponentConstantId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComponentConstantVersions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComponentConstantVersions_ComponentConstants_ComponentConstantId",
                        column: x => x.ComponentConstantId,
                        principalTable: "ComponentConstants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ComponentConstantVersions_DFIndicators_DFIndicatorId",
                        column: x => x.DFIndicatorId,
                        principalTable: "DFIndicators",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComponentConstantVersions_DFQuarters_DFQuarterId",
                        column: x => x.DFQuarterId,
                        principalTable: "DFQuarters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComponentConstantVersions_DFSources_DFSourceId",
                        column: x => x.DFSourceId,
                        principalTable: "DFSources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComponentConstantVersions_DFUnits_DFUnitId",
                        column: x => x.DFUnitId,
                        principalTable: "DFUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComponentConstantVersions_DFYears_DFYearFiscalId",
                        column: x => x.DFYearFiscalId,
                        principalTable: "DFYears",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ComponentConstantVersions_ComponentConstantId",
                table: "ComponentConstantVersions",
                column: "ComponentConstantId");

            migrationBuilder.CreateIndex(
                name: "IX_ComponentConstantVersions_DFIndicatorId",
                table: "ComponentConstantVersions",
                column: "DFIndicatorId");

            migrationBuilder.CreateIndex(
                name: "IX_ComponentConstantVersions_DFQuarterId",
                table: "ComponentConstantVersions",
                column: "DFQuarterId");

            migrationBuilder.CreateIndex(
                name: "IX_ComponentConstantVersions_DFSourceId",
                table: "ComponentConstantVersions",
                column: "DFSourceId");

            migrationBuilder.CreateIndex(
                name: "IX_ComponentConstantVersions_DFUnitId",
                table: "ComponentConstantVersions",
                column: "DFUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_ComponentConstantVersions_DFYearFiscalId",
                table: "ComponentConstantVersions",
                column: "DFYearFiscalId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComponentConstantVersions");

        }
    }
}
