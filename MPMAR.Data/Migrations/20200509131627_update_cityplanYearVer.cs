using Microsoft.EntityFrameworkCore.Migrations;

namespace MPMAR.Data.Migrations
{
    public partial class update_cityplanYearVer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CityPlanYearVersions_CityPlan_CityPlanId",
                table: "CityPlanYearVersions");

            migrationBuilder.DropIndex(
                name: "IX_CityPlanYearVersions_CityPlanId",
                table: "CityPlanYearVersions");

            migrationBuilder.DropColumn(
                name: "CityPlanId",
                table: "CityPlanYearVersions");

            migrationBuilder.AddColumn<int>(
                name: "CityPlanVersionId",
                table: "CityPlanYearVersions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CityPlanYearVersions_CityPlanVersionId",
                table: "CityPlanYearVersions",
                column: "CityPlanVersionId");

            migrationBuilder.AddForeignKey(
                name: "FK_CityPlanYearVersions_CityPlanVersions_CityPlanVersionId",
                table: "CityPlanYearVersions",
                column: "CityPlanVersionId",
                principalTable: "CityPlanVersions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CityPlanYearVersions_CityPlanVersions_CityPlanVersionId",
                table: "CityPlanYearVersions");

            migrationBuilder.DropIndex(
                name: "IX_CityPlanYearVersions_CityPlanVersionId",
                table: "CityPlanYearVersions");

            migrationBuilder.DropColumn(
                name: "CityPlanVersionId",
                table: "CityPlanYearVersions");

            migrationBuilder.AddColumn<int>(
                name: "CityPlanId",
                table: "CityPlanYearVersions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CityPlanYearVersions_CityPlanId",
                table: "CityPlanYearVersions",
                column: "CityPlanId");

            migrationBuilder.AddForeignKey(
                name: "FK_CityPlanYearVersions_CityPlan_CityPlanId",
                table: "CityPlanYearVersions",
                column: "CityPlanId",
                principalTable: "CityPlan",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
