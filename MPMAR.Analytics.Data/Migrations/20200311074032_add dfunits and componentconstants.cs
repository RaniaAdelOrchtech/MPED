using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MPMAR.Analytics.Data.Migrations
{
    public partial class adddfunitsandcomponentconstants : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DFUnits",
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
                    table.PrimaryKey("PK_DFUnits", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GrossDomesticComponent",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Indicator = table.Column<string>(nullable: true),
                    Governorate = table.Column<string>(nullable: true),
                    Unit = table.Column<string>(nullable: true),
                    Quarter = table.Column<string>(nullable: true),
                    BaseYear = table.Column<string>(nullable: true),
                    FiscalYear = table.Column<string>(nullable: true),
                    PrivateConsumption = table.Column<double>(nullable: true),
                    GovernmentConsumption = table.Column<double>(nullable: true),
                    GrossCapitalFormation = table.Column<double>(nullable: true),
                    ExportsOfGoodsAndServices = table.Column<double>(nullable: true),
                    ImportsOfGoodsAndServices = table.Column<double>(nullable: true),
                    TotalGrossDomesticProductAtMarketPrices = table.Column<double>(nullable: true),
                    RealGrowthRate = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GrossDomesticComponent", x => x.Id);
                    
                });

            migrationBuilder.CreateTable(
                name: "ComponentConstants",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DFIndicatorId = table.Column<int>(nullable: false),
                    DFGovernorateId = table.Column<int>(nullable: false),
                    DFUnitId = table.Column<int>(nullable: false),
                    DFYearBaseId = table.Column<int>(nullable: false),
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
                    table.PrimaryKey("PK_ComponentConstants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComponentConstants_DFGovernorates_DFGovernorateId",
                        column: x => x.DFGovernorateId,
                        principalTable: "DFGovernorates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComponentConstants_DFIndicators_DFIndicatorId",
                        column: x => x.DFIndicatorId,
                        principalTable: "DFIndicators",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComponentConstants_DFUnits_DFUnitId",
                        column: x => x.DFUnitId,
                        principalTable: "DFUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComponentConstants_DFYears_DFYearBaseId",
                        column: x => x.DFYearBaseId,
                        principalTable: "DFYears",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ComponentConstants_DFYears_DFYearFiscalId",
                        column: x => x.DFYearFiscalId,
                        principalTable: "DFYears",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ComponentConstants_DFGovernorateId",
                table: "ComponentConstants",
                column: "DFGovernorateId");

            migrationBuilder.CreateIndex(
                name: "IX_ComponentConstants_DFIndicatorId",
                table: "ComponentConstants",
                column: "DFIndicatorId");

            migrationBuilder.CreateIndex(
                name: "IX_ComponentConstants_DFUnitId",
                table: "ComponentConstants",
                column: "DFUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_ComponentConstants_DFYearBaseId",
                table: "ComponentConstants",
                column: "DFYearBaseId");

            migrationBuilder.CreateIndex(
                name: "IX_ComponentConstants_DFYearFiscalId",
                table: "ComponentConstants",
                column: "DFYearFiscalId");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComponentConstants");

            migrationBuilder.DropTable(
                name: "GrossDomesticComponent");

            migrationBuilder.DropTable(
                name: "DFUnits");
        }
    }
}
