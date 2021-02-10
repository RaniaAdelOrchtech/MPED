using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MPMAR.Analytics.Data.Migrations
{
    public partial class updatedfgdpbyaddingisbasic : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
          

            migrationBuilder.AddColumn<bool>(
                name: "IsBasic",
                table: "DFGDP",
                nullable: false,
                defaultValue: false);

           
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GrossDomesticActivity");

            migrationBuilder.DropTable(
                name: "GrossDomesticComponentViewModel");

            migrationBuilder.DropColumn(
                name: "IsBasic",
                table: "DFGDP");

            migrationBuilder.CreateTable(
                name: "GrossDomesticComponent",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BaseYear = table.Column<string>(nullable: true),
                    DFRegionId = table.Column<int>(nullable: true),
                    ExportsOfGoodsAndServices = table.Column<double>(nullable: true),
                    FiscalYear = table.Column<string>(nullable: true),
                    GovernmentConsumption = table.Column<double>(nullable: true),
                    GrossCapitalFormation = table.Column<double>(nullable: true),
                    ImportsOfGoodsAndServices = table.Column<double>(nullable: true),
                    Indicator = table.Column<string>(nullable: true),
                    PrivateConsumption = table.Column<double>(nullable: true),
                    RealGrowthRate = table.Column<double>(nullable: true),
                    TotalGrossDomesticProductAtMarketPrices = table.Column<double>(nullable: true),
                    Unit = table.Column<string>(nullable: true),
                    _Quarter = table.Column<string>(nullable: true),
                    _Source = table.Column<string>(nullable: true),
                    _Value = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GrossDomesticComponent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GrossDomesticComponent_DFRegions_DFRegionId",
                        column: x => x.DFRegionId,
                        principalTable: "DFRegions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GrossDomesticComponent_DFRegionId",
                table: "GrossDomesticComponent",
                column: "DFRegionId");
        }
    }
}
