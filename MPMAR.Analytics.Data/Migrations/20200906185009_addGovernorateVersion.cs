using Microsoft.EntityFrameworkCore.Migrations;

namespace MPMAR.Analytics.Data.Migrations
{
    public partial class addGovernorateVersion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GovernorateVersions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DFIndicatorId = table.Column<int>(nullable: false),
                    Unit = table.Column<string>(nullable: true),
                    DFGovernorateId = table.Column<int>(nullable: false),
                    DFYearId = table.Column<int>(nullable: false),
                    Agriculture = table.Column<double>(nullable: true),
                    CrudePetroleumExtraction = table.Column<double>(nullable: true),
                    OtherExtractions = table.Column<double>(nullable: true),
                    PetroleumRefinement = table.Column<double>(nullable: true),
                    ManufacturingIndustries = table.Column<double>(nullable: true),
                    ElectricityandGas = table.Column<double>(nullable: true),
                    Water = table.Column<double>(nullable: true),
                    Sewerage = table.Column<double>(nullable: true),
                    WasteRecycling = table.Column<double>(nullable: true),
                    Construction = table.Column<double>(nullable: true),
                    WholesaleandRetailTrade = table.Column<double>(nullable: true),
                    Communication = table.Column<double>(nullable: true),
                    Information = table.Column<double>(nullable: true),
                    TransportationandStorage = table.Column<double>(nullable: true),
                    AccommodationandFoodServiceActivities = table.Column<double>(nullable: true),
                    RealEstateOwnership = table.Column<double>(nullable: true),
                    BusinessServices = table.Column<double>(nullable: true),
                    Education = table.Column<double>(nullable: true),
                    Health = table.Column<double>(nullable: true),
                    OtherServices = table.Column<double>(nullable: true),
                    NonFinancialCorporations = table.Column<double>(nullable: true),
                    FinancialCorporations = table.Column<double>(nullable: true),
                    GeneralGovernment = table.Column<double>(nullable: true),
                    NonProfitInstitutionsServingHouseholdSector = table.Column<double>(nullable: true),
                    DomesticWorkers = table.Column<double>(nullable: true),
                    TotalGovernorateGDP = table.Column<double>(nullable: true),
                    CustomFees = table.Column<double>(nullable: true),
                    TotalGDPEgyptWithCustomFees = table.Column<double>(nullable: true),
                    isDeleted = table.Column<bool>(nullable: true),
                    ChangeActionEnum = table.Column<int>(nullable: true),
                    VersionStatusEnum = table.Column<int>(nullable: true),
                    GovernorateId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GovernorateVersions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GovernorateVersions_DFGovernorates_DFGovernorateId",
                        column: x => x.DFGovernorateId,
                        principalTable: "DFGovernorates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GovernorateVersions_DFIndicators_DFIndicatorId",
                        column: x => x.DFIndicatorId,
                        principalTable: "DFIndicators",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GovernorateVersions_DFYears_DFYearId",
                        column: x => x.DFYearId,
                        principalTable: "DFYears",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GovernorateVersions_Governorates_GovernorateId",
                        column: x => x.GovernorateId,
                        principalTable: "Governorates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GovernorateVersions_DFGovernorateId",
                table: "GovernorateVersions",
                column: "DFGovernorateId");

            migrationBuilder.CreateIndex(
                name: "IX_GovernorateVersions_DFIndicatorId",
                table: "GovernorateVersions",
                column: "DFIndicatorId");

            migrationBuilder.CreateIndex(
                name: "IX_GovernorateVersions_DFYearId",
                table: "GovernorateVersions",
                column: "DFYearId");

            migrationBuilder.CreateIndex(
                name: "IX_GovernorateVersions_GovernorateId",
                table: "GovernorateVersions",
                column: "GovernorateId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GovernorateVersions");
        }
    }
}
