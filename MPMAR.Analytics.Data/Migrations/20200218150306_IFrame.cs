using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MPMAR.Analytics.Data.Migrations
{
    public partial class IFrame : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Analytics",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Indicator = table.Column<string>(nullable: true),
                    Source = table.Column<string>(nullable: true),
                    MeasureUnit = table.Column<string>(nullable: true),
                    Quarter = table.Column<string>(nullable: true),
                    BaseYear = table.Column<string>(nullable: true),
                    FinancialYear = table.Column<string>(nullable: true),
                    FamilyConsumption = table.Column<double>(nullable: false),
                    GovernmentConsumption = table.Column<double>(nullable: false),
                    InvestmentAndInventory = table.Column<double>(nullable: false),
                    ExportedGoodsAndServices = table.Column<double>(nullable: false),
                    ImportedGoodsAndServices = table.Column<double>(nullable: false),
                    GrossDomesticProductWithMarketPrice = table.Column<double>(nullable: false),
                    GrowthRate = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Analytics", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DFIndicators",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    NameAr = table.Column<string>(nullable: true),
                    NameEn = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DFIndicators", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DFQuarters",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    NameAr = table.Column<string>(nullable: true),
                    NameEn = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DFQuarters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DFRegions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    NameAr = table.Column<string>(nullable: true),
                    NameEn = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DFRegions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DFSectors",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    NameAr = table.Column<string>(nullable: true),
                    NameEn = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DFSectors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DFYears",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    NameAr = table.Column<string>(nullable: true),
                    NameEn = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DFYears", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FinalGovernorates",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Indicator = table.Column<string>(nullable: true),
                    Unit = table.Column<string>(nullable: true),
                    Governoratee = table.Column<string>(nullable: true),
                    Region = table.Column<string>(nullable: true),
                    Year = table.Column<string>(nullable: true),
                    Sector = table.Column<string>(nullable: true),
                    GDPValue = table.Column<decimal>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinalGovernorates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GDPUnderways",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Indicator = table.Column<string>(nullable: true),
                    Source = table.Column<string>(nullable: true),
                    MeasureUnit = table.Column<string>(nullable: true),
                    Quarter = table.Column<string>(nullable: true),
                    FinancialYear = table.Column<string>(nullable: true),
                    FamilyConsumption = table.Column<double>(nullable: true),
                    GovernmentConsumption = table.Column<double>(nullable: true),
                    InvestmentAndInventory = table.Column<double>(nullable: true),
                    ExportedGoodsAndServices = table.Column<double>(nullable: true),
                    ImportedGoodsAndServices = table.Column<double>(nullable: true),
                    GrossDomesticProductWithMarketPrice = table.Column<double>(nullable: true),
                    GrowthRate = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GDPUnderways", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LocalizedColumnNames",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Key = table.Column<string>(nullable: true),
                    NameAr = table.Column<string>(nullable: true),
                    NameEn = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocalizedColumnNames", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OngoingActivities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Quarter = table.Column<string>(nullable: true),
                    Year = table.Column<string>(nullable: true),
                    PerEGMillion = table.Column<string>(nullable: true),
                    AgricultureForestryandFishing = table.Column<double>(nullable: true),
                    Extractions = table.Column<double>(nullable: true),
                    Oil = table.Column<double>(nullable: true),
                    Gas = table.Column<double>(nullable: true),
                    ExtractionsOthers = table.Column<double>(nullable: true),
                    TransformativeIndustries = table.Column<double>(nullable: true),
                    PetroleumRefining = table.Column<double>(nullable: true),
                    AnotherExtension = table.Column<double>(nullable: true),
                    Electricity = table.Column<double>(nullable: true),
                    WaterandRecycling = table.Column<double>(nullable: true),
                    ConstructionandBuilding = table.Column<double>(nullable: true),
                    TransportationandSaving = table.Column<double>(nullable: true),
                    CommunicationandInformation = table.Column<double>(nullable: true),
                    SuezCanal = table.Column<double>(nullable: true),
                    WholesaleandRetail = table.Column<double>(nullable: true),
                    FinancialIntermediationandAuxiliaryActivities = table.Column<double>(nullable: true),
                    SocialInsurance = table.Column<double>(nullable: true),
                    HotelandRestaurants = table.Column<double>(nullable: true),
                    RealEstateActivities = table.Column<double>(nullable: true),
                    RealEstateProperty = table.Column<double>(nullable: true),
                    BusinessServices = table.Column<double>(nullable: true),
                    GeneralGovernment = table.Column<double>(nullable: true),
                    EducationandPersonalServices = table.Column<double>(nullable: true),
                    Education = table.Column<double>(nullable: true),
                    Health = table.Column<double>(nullable: true),
                    OtherServices = table.Column<double>(nullable: true),
                    TotalGeneral = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OngoingActivities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StaticActivities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BaseYear = table.Column<string>(nullable: true),
                    Quarter = table.Column<string>(nullable: true),
                    Year = table.Column<string>(nullable: true),
                    PerEGMillion = table.Column<string>(nullable: true),
                    AgricultureForestryandFishing = table.Column<double>(nullable: true),
                    Extractions = table.Column<double>(nullable: true),
                    Oil = table.Column<double>(nullable: true),
                    Gas = table.Column<double>(nullable: true),
                    ExtractionsOthers = table.Column<double>(nullable: true),
                    TransformativeIndustries = table.Column<double>(nullable: true),
                    PetroleumRefining = table.Column<double>(nullable: true),
                    AnotherExtension = table.Column<double>(nullable: true),
                    Electricity = table.Column<double>(nullable: true),
                    WaterandRecycling = table.Column<double>(nullable: true),
                    ConstructionandBuilding = table.Column<double>(nullable: true),
                    TransportationandSaving = table.Column<double>(nullable: true),
                    CommunicationandInformation = table.Column<double>(nullable: true),
                    SuezCanal = table.Column<double>(nullable: true),
                    WholesaleandRetailTrade = table.Column<double>(nullable: true),
                    FinancialIntermediationandAuxiliaryActivities = table.Column<double>(nullable: true),
                    SocialInsurance = table.Column<double>(nullable: true),
                    HotelandRestaurants = table.Column<double>(nullable: true),
                    RealEstateActivities = table.Column<double>(nullable: true),
                    RealEstateProperty = table.Column<double>(nullable: true),
                    BusinessServices = table.Column<double>(nullable: true),
                    GeneralGovernment = table.Column<double>(nullable: true),
                    EducationandPersonalServices = table.Column<double>(nullable: true),
                    Education = table.Column<double>(nullable: true),
                    Health = table.Column<double>(nullable: true),
                    OtherServices = table.Column<double>(nullable: true),
                    TotalGeneral = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaticActivities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DFGovernorates",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    NameAr = table.Column<string>(nullable: true),
                    NameEn = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DFRegionId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DFGovernorates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DFGovernorates_DFRegions_DFRegionId",
                        column: x => x.DFRegionId,
                        principalTable: "DFRegions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Governorates",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
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
                    TotalGDPEgyptWithCustomFees = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Governorates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Governorates_DFGovernorates_DFGovernorateId",
                        column: x => x.DFGovernorateId,
                        principalTable: "DFGovernorates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Governorates_DFIndicators_DFIndicatorId",
                        column: x => x.DFIndicatorId,
                        principalTable: "DFIndicators",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Governorates_DFYears_DFYearId",
                        column: x => x.DFYearId,
                        principalTable: "DFYears",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MPMARGovernorates",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DFIndicatorId = table.Column<int>(nullable: false),
                    Unit = table.Column<string>(nullable: true),
                    DFGovernorateId = table.Column<int>(nullable: false),
                    DFRegionId = table.Column<int>(nullable: false),
                    DFYearId = table.Column<int>(nullable: false),
                    Agriculture = table.Column<double>(nullable: true),
                    Crude_Petroleum_Extraction = table.Column<double>(nullable: true),
                    Other_Extractions = table.Column<double>(nullable: true),
                    Petroleum_Refinement = table.Column<double>(nullable: true),
                    Manufacturing_Industries = table.Column<double>(nullable: true),
                    Electricity_and_Gas = table.Column<double>(nullable: true),
                    Water = table.Column<double>(nullable: true),
                    Sewerage = table.Column<double>(nullable: true),
                    Waste_Recycling = table.Column<double>(nullable: true),
                    Construction = table.Column<double>(nullable: true),
                    Whole_sale_and_Retail_Trade = table.Column<double>(nullable: true),
                    Communication = table.Column<double>(nullable: true),
                    Information = table.Column<double>(nullable: true),
                    Transportation_and_Storage = table.Column<double>(nullable: true),
                    Accommodation_and_Food_Service_Activities = table.Column<double>(nullable: true),
                    Real_Estate_Ownership = table.Column<double>(nullable: true),
                    Business_Services = table.Column<double>(nullable: true),
                    Education = table.Column<double>(nullable: true),
                    Health = table.Column<double>(nullable: true),
                    Other_Services = table.Column<double>(nullable: true),
                    Non_Financial_Corporations = table.Column<double>(nullable: true),
                    Financial_Corporations = table.Column<double>(nullable: true),
                    General_Government = table.Column<double>(nullable: true),
                    Non_Profit_Institutions_Serving_House_hold_Sector = table.Column<double>(nullable: true),
                    Domestic_Workers = table.Column<double>(nullable: true),
                    Total_Governorate_GDP = table.Column<double>(nullable: true),
                    Custom_Fees = table.Column<double>(nullable: true),
                    Total_GDP_Egypt_With_Custom_Fees = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MPMARGovernorates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MPMARGovernorates_DFGovernorates_DFGovernorateId",
                        column: x => x.DFGovernorateId,
                        principalTable: "DFGovernorates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MPMARGovernorates_DFIndicators_DFIndicatorId",
                        column: x => x.DFIndicatorId,
                        principalTable: "DFIndicators",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MPMARGovernorates_DFRegions_DFRegionId",
                        column: x => x.DFRegionId,
                        principalTable: "DFRegions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MPMARGovernorates_DFYears_DFYearId",
                        column: x => x.DFYearId,
                        principalTable: "DFYears",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DFGovernorates_DFRegionId",
                table: "DFGovernorates",
                column: "DFRegionId");

            migrationBuilder.CreateIndex(
                name: "IX_Governorates_DFGovernorateId",
                table: "Governorates",
                column: "DFGovernorateId");

            migrationBuilder.CreateIndex(
                name: "IX_Governorates_DFIndicatorId",
                table: "Governorates",
                column: "DFIndicatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Governorates_DFYearId",
                table: "Governorates",
                column: "DFYearId");

            migrationBuilder.CreateIndex(
                name: "IX_MPMARGovernorates_DFGovernorateId",
                table: "MPMARGovernorates",
                column: "DFGovernorateId");

            migrationBuilder.CreateIndex(
                name: "IX_MPMARGovernorates_DFIndicatorId",
                table: "MPMARGovernorates",
                column: "DFIndicatorId");

            migrationBuilder.CreateIndex(
                name: "IX_MPMARGovernorates_DFRegionId",
                table: "MPMARGovernorates",
                column: "DFRegionId");

            migrationBuilder.CreateIndex(
                name: "IX_MPMARGovernorates_DFYearId",
                table: "MPMARGovernorates",
                column: "DFYearId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Analytics");

            migrationBuilder.DropTable(
                name: "DFQuarters");

            migrationBuilder.DropTable(
                name: "DFSectors");

            migrationBuilder.DropTable(
                name: "FinalGovernorates");

            migrationBuilder.DropTable(
                name: "GDPUnderways");

            migrationBuilder.DropTable(
                name: "Governorates");

            migrationBuilder.DropTable(
                name: "LocalizedColumnNames");

            migrationBuilder.DropTable(
                name: "MPMARGovernorates");

            migrationBuilder.DropTable(
                name: "OngoingActivities");

            migrationBuilder.DropTable(
                name: "StaticActivities");

            migrationBuilder.DropTable(
                name: "DFGovernorates");

            migrationBuilder.DropTable(
                name: "DFIndicators");

            migrationBuilder.DropTable(
                name: "DFYears");

            migrationBuilder.DropTable(
                name: "DFRegions");
        }
    }
}
