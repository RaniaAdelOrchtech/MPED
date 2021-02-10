using Microsoft.EntityFrameworkCore.Migrations;

namespace MPMAR.Data.Migrations
{
    public partial class addMinistryTimeLineVersionstoFormerMinistriesPageInfoVersions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FormerMinistriesPageInfoVersionsId",
                table: "MinistryTimeLineVersions",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MinistryTimeLineVersions_FormerMinistriesPageInfoVersionsId",
                table: "MinistryTimeLineVersions",
                column: "FormerMinistriesPageInfoVersionsId");

            migrationBuilder.AddForeignKey(
                name: "FK_MinistryTimeLineVersions_FormerMinistriesPageInfoVersions_FormerMinistriesPageInfoVersionsId",
                table: "MinistryTimeLineVersions",
                column: "FormerMinistriesPageInfoVersionsId",
                principalTable: "FormerMinistriesPageInfoVersions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MinistryTimeLineVersions_FormerMinistriesPageInfoVersions_FormerMinistriesPageInfoVersionsId",
                table: "MinistryTimeLineVersions");

            migrationBuilder.DropIndex(
                name: "IX_MinistryTimeLineVersions_FormerMinistriesPageInfoVersionsId",
                table: "MinistryTimeLineVersions");

            migrationBuilder.DropColumn(
                name: "FormerMinistriesPageInfoVersionsId",
                table: "MinistryTimeLineVersions");
        }
    }
}
