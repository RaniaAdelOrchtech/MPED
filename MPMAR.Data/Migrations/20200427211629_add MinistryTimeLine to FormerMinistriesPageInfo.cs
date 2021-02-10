using Microsoft.EntityFrameworkCore.Migrations;

namespace MPMAR.Data.Migrations
{
    public partial class addMinistryTimeLinetoFormerMinistriesPageInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FormerMinistriesPageInfoId",
                table: "MinistryTimeLine",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MinistryTimeLine_FormerMinistriesPageInfoId",
                table: "MinistryTimeLine",
                column: "FormerMinistriesPageInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_MinistryTimeLine_FormerMinistriesPageInfos_FormerMinistriesPageInfoId",
                table: "MinistryTimeLine",
                column: "FormerMinistriesPageInfoId",
                principalTable: "FormerMinistriesPageInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MinistryTimeLine_FormerMinistriesPageInfos_FormerMinistriesPageInfoId",
                table: "MinistryTimeLine");

            migrationBuilder.DropIndex(
                name: "IX_MinistryTimeLine_FormerMinistriesPageInfoId",
                table: "MinistryTimeLine");

            migrationBuilder.DropColumn(
                name: "FormerMinistriesPageInfoId",
                table: "MinistryTimeLine");
        }
    }
}
