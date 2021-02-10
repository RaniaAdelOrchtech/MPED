using Microsoft.EntityFrameworkCore.Migrations;

namespace MPMAR.Data.Migrations
{
    public partial class add_dfGov : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DFGovId",
                table: "CityPlanYearVersions",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DFGovId",
                table: "CityPlanYear",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DFGovs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArName = table.Column<string>(nullable: true),
                    EnName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DFGovs", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CityPlanYearVersions_DFGovId",
                table: "CityPlanYearVersions",
                column: "DFGovId");

            migrationBuilder.CreateIndex(
                name: "IX_CityPlanYear_DFGovId",
                table: "CityPlanYear",
                column: "DFGovId");

            migrationBuilder.AddForeignKey(
                name: "FK_CityPlanYear_DFGovs_DFGovId",
                table: "CityPlanYear",
                column: "DFGovId",
                principalTable: "DFGovs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CityPlanYearVersions_DFGovs_DFGovId",
                table: "CityPlanYearVersions",
                column: "DFGovId",
                principalTable: "DFGovs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CityPlanYear_DFGovs_DFGovId",
                table: "CityPlanYear");

            migrationBuilder.DropForeignKey(
                name: "FK_CityPlanYearVersions_DFGovs_DFGovId",
                table: "CityPlanYearVersions");

            migrationBuilder.DropTable(
                name: "DFGovs");

            migrationBuilder.DropIndex(
                name: "IX_CityPlanYearVersions_DFGovId",
                table: "CityPlanYearVersions");

            migrationBuilder.DropIndex(
                name: "IX_CityPlanYear_DFGovId",
                table: "CityPlanYear");

            migrationBuilder.DropColumn(
                name: "DFGovId",
                table: "CityPlanYearVersions");

            migrationBuilder.DropColumn(
                name: "DFGovId",
                table: "CityPlanYear");
        }
    }
}
