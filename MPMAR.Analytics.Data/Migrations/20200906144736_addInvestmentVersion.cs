using Microsoft.EntityFrameworkCore.Migrations;

namespace MPMAR.Analytics.Data.Migrations
{
    public partial class addInvestmentVersion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InvestmentVersions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DFIndicatorId = table.Column<int>(nullable: false),
                    DFSourceId = table.Column<int>(nullable: false),
                    DFUnitId = table.Column<int>(nullable: false),
                    DFQuarterId = table.Column<int>(nullable: false),
                    DFYearId = table.Column<int>(nullable: false),
                    Agriculture = table.Column<double>(nullable: true),
                    Petroleum = table.Column<double>(nullable: true),
                    NaturalGas = table.Column<double>(nullable: true),
                    OtherExtractions = table.Column<double>(nullable: true),
                    PetroleumRefining = table.Column<double>(nullable: true),
                    OtherManufacturing = table.Column<double>(nullable: true),
                    Electricity = table.Column<double>(nullable: true),
                    WaterAndSewerage = table.Column<double>(nullable: true),
                    Construction = table.Column<double>(nullable: true),
                    StorageAndTransportation = table.Column<double>(nullable: true),
                    InformationAndCommunication = table.Column<double>(nullable: true),
                    SuezCanal = table.Column<double>(nullable: true),
                    WholesaleAndRetailTrade = table.Column<double>(nullable: true),
                    FinancialIntermediaryInsuranceAndSocialSecurity = table.Column<double>(nullable: true),
                    AccommodationAndFoodServiceActivities = table.Column<double>(nullable: true),
                    RealEstateActivities = table.Column<double>(nullable: true),
                    Education = table.Column<double>(nullable: true),
                    Health = table.Column<double>(nullable: true),
                    OtherSrvices = table.Column<double>(nullable: true),
                    TotalInvestments = table.Column<double>(nullable: true),
                    isDeleted = table.Column<bool>(nullable: true),
                    ChangeActionEnum = table.Column<int>(nullable: true),
                    VersionStatusEnum = table.Column<int>(nullable: true),
                    InvestmentsId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvestmentVersions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvestmentVersions_DFIndicators_DFIndicatorId",
                        column: x => x.DFIndicatorId,
                        principalTable: "DFIndicators",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InvestmentVersions_DFQuarters_DFQuarterId",
                        column: x => x.DFQuarterId,
                        principalTable: "DFQuarters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InvestmentVersions_DFSources_DFSourceId",
                        column: x => x.DFSourceId,
                        principalTable: "DFSources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InvestmentVersions_DFUnits_DFUnitId",
                        column: x => x.DFUnitId,
                        principalTable: "DFUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InvestmentVersions_DFYears_DFYearId",
                        column: x => x.DFYearId,
                        principalTable: "DFYears",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InvestmentVersions_Investments_InvestmentsId",
                        column: x => x.InvestmentsId,
                        principalTable: "Investments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InvestmentVersions_DFIndicatorId",
                table: "InvestmentVersions",
                column: "DFIndicatorId");

            migrationBuilder.CreateIndex(
                name: "IX_InvestmentVersions_DFQuarterId",
                table: "InvestmentVersions",
                column: "DFQuarterId");

            migrationBuilder.CreateIndex(
                name: "IX_InvestmentVersions_DFSourceId",
                table: "InvestmentVersions",
                column: "DFSourceId");

            migrationBuilder.CreateIndex(
                name: "IX_InvestmentVersions_DFUnitId",
                table: "InvestmentVersions",
                column: "DFUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_InvestmentVersions_DFYearId",
                table: "InvestmentVersions",
                column: "DFYearId");

            migrationBuilder.CreateIndex(
                name: "IX_InvestmentVersions_InvestmentsId",
                table: "InvestmentVersions",
                column: "InvestmentsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InvestmentVersions");
        }
    }
}
