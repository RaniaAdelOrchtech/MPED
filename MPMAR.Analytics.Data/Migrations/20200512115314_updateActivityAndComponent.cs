using Microsoft.EntityFrameworkCore.Migrations;

namespace MPMAR.Analytics.Data.Migrations
{
    public partial class updateActivityAndComponent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActivityConstants_DFYears_DFYearBaseId",
                table: "ActivityConstants");

            migrationBuilder.DropForeignKey(
                name: "FK_ComponentConstants_DFYears_DFYearBaseId",
                table: "ComponentConstants");

            migrationBuilder.DropIndex(
                name: "IX_ComponentConstants_DFYearBaseId",
                table: "ComponentConstants");

            migrationBuilder.DropIndex(
                name: "IX_ActivityConstants_DFYearBaseId",
                table: "ActivityConstants");

            migrationBuilder.DropColumn(
                name: "DFYearBaseId",
                table: "ComponentConstants");

            migrationBuilder.DropColumn(
                name: "DFYearBaseId",
                table: "ActivityConstants");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DFYearBaseId",
                table: "ComponentConstants",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DFYearBaseId",
                table: "ActivityConstants",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ComponentConstants_DFYearBaseId",
                table: "ComponentConstants",
                column: "DFYearBaseId");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityConstants_DFYearBaseId",
                table: "ActivityConstants",
                column: "DFYearBaseId");

            migrationBuilder.AddForeignKey(
                name: "FK_ActivityConstants_DFYears_DFYearBaseId",
                table: "ActivityConstants",
                column: "DFYearBaseId",
                principalTable: "DFYears",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ComponentConstants_DFYears_DFYearBaseId",
                table: "ComponentConstants",
                column: "DFYearBaseId",
                principalTable: "DFYears",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
