using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MPMAR.Analytics.Data.Migrations
{
    public partial class addActivityConstant : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ActivityConstants",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DFIndicatorId = table.Column<int>(nullable: false),
                    DFSourceId = table.Column<int>(nullable: false),
                    DFUnitId = table.Column<int>(nullable: false),
                    DFQuarterId = table.Column<int>(nullable: false),
                    DFYearBaseId = table.Column<int>(nullable: false),
                    DFYearId = table.Column<int>(nullable: false),
                    DFSectorId = table.Column<int>(nullable: false),
                    AgricultureForestryFishing = table.Column<double>(nullable: true),
                    MiningQuarrying = table.Column<double>(nullable: true),
                    Petroleum = table.Column<double>(nullable: true),
                    Gas = table.Column<double>(nullable: true),
                    OtherExtraction = table.Column<double>(nullable: true),
                    ManufacturingIndustries = table.Column<double>(nullable: true),
                    petroleumRefining = table.Column<double>(nullable: true),
                    OtherManufacturing = table.Column<double>(nullable: true),
                    Electricity = table.Column<double>(nullable: true),
                    WaterSewerageRemediationActivitie = table.Column<double>(nullable: true),
                    Construction = table.Column<double>(nullable: true),
                    TransportationAndStorage = table.Column<double>(nullable: true),
                    Communication = table.Column<double>(nullable: true),
                    Information = table.Column<double>(nullable: true),
                    SuezcCanal = table.Column<double>(nullable: true),
                    WholesaleAndRetailTrade = table.Column<double>(nullable: true),
                    FinancialIntermediariesAuxiliaryServices = table.Column<double>(nullable: true),
                    SocialSecurityAndInsurance = table.Column<double>(nullable: true),
                    AccommodationAndFoodServiceActivities = table.Column<double>(nullable: true),
                    RealEstateActivitie = table.Column<double>(nullable: true),
                    RealEstateOwnership = table.Column<double>(nullable: true),
                    BusinessServices = table.Column<double>(nullable: true),
                    GeneralGovernment = table.Column<double>(nullable: true),
                    SocialServices = table.Column<double>(nullable: true),
                    Education = table.Column<double>(nullable: true),
                    Health = table.Column<double>(nullable: true),
                    OtherServices = table.Column<double>(nullable: true),
                    TotalGDPAtFactorCost = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityConstants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActivityConstants_DFIndicators_DFIndicatorId",
                        column: x => x.DFIndicatorId,
                        principalTable: "DFIndicators",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActivityConstants_DFQuarters_DFQuarterId",
                        column: x => x.DFQuarterId,
                        principalTable: "DFQuarters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActivityConstants_DFSectors_DFSectorId",
                        column: x => x.DFSectorId,
                        principalTable: "DFSectors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActivityConstants_DFSources_DFSourceId",
                        column: x => x.DFSourceId,
                        principalTable: "DFSources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActivityConstants_DFUnits_DFUnitId",
                        column: x => x.DFUnitId,
                        principalTable: "DFUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActivityConstants_DFYears_DFYearBaseId",
                        column: x => x.DFYearBaseId,
                        principalTable: "DFYears",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ActivityConstants_DFYears_DFYearId",
                        column: x => x.DFYearId,
                        principalTable: "DFYears",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActivityConstants_DFIndicatorId",
                table: "ActivityConstants",
                column: "DFIndicatorId");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityConstants_DFQuarterId",
                table: "ActivityConstants",
                column: "DFQuarterId");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityConstants_DFSectorId",
                table: "ActivityConstants",
                column: "DFSectorId");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityConstants_DFSourceId",
                table: "ActivityConstants",
                column: "DFSourceId");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityConstants_DFUnitId",
                table: "ActivityConstants",
                column: "DFUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityConstants_DFYearBaseId",
                table: "ActivityConstants",
                column: "DFYearBaseId");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityConstants_DFYearId",
                table: "ActivityConstants",
                column: "DFYearId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActivityConstants");
        }
    }
}
