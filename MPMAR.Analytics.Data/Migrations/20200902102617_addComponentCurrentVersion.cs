using Microsoft.EntityFrameworkCore.Migrations;

namespace MPMAR.Analytics.Data.Migrations
{
    public partial class addComponentCurrentVersion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ComponentCurrentVersions",
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
                    ComponentCurrentId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComponentCurrentVersions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComponentCurrentVersions_ComponentCurrents_ComponentCurrentId",
                        column: x => x.ComponentCurrentId,
                        principalTable: "ComponentCurrents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ComponentCurrentVersions_DFIndicators_DFIndicatorId",
                        column: x => x.DFIndicatorId,
                        principalTable: "DFIndicators",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComponentCurrentVersions_DFQuarters_DFQuarterId",
                        column: x => x.DFQuarterId,
                        principalTable: "DFQuarters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComponentCurrentVersions_DFSources_DFSourceId",
                        column: x => x.DFSourceId,
                        principalTable: "DFSources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComponentCurrentVersions_DFUnits_DFUnitId",
                        column: x => x.DFUnitId,
                        principalTable: "DFUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComponentCurrentVersions_DFYears_DFYearFiscalId",
                        column: x => x.DFYearFiscalId,
                        principalTable: "DFYears",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ComponentCurrentVersions_ComponentCurrentId",
                table: "ComponentCurrentVersions",
                column: "ComponentCurrentId");

            migrationBuilder.CreateIndex(
                name: "IX_ComponentCurrentVersions_DFIndicatorId",
                table: "ComponentCurrentVersions",
                column: "DFIndicatorId");

            migrationBuilder.CreateIndex(
                name: "IX_ComponentCurrentVersions_DFQuarterId",
                table: "ComponentCurrentVersions",
                column: "DFQuarterId");

            migrationBuilder.CreateIndex(
                name: "IX_ComponentCurrentVersions_DFSourceId",
                table: "ComponentCurrentVersions",
                column: "DFSourceId");

            migrationBuilder.CreateIndex(
                name: "IX_ComponentCurrentVersions_DFUnitId",
                table: "ComponentCurrentVersions",
                column: "DFUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_ComponentCurrentVersions_DFYearFiscalId",
                table: "ComponentCurrentVersions",
                column: "DFYearFiscalId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComponentCurrentVersions");
        }
    }
}
