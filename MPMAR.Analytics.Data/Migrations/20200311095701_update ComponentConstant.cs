using Microsoft.EntityFrameworkCore.Migrations;

namespace MPMAR.Analytics.Data.Migrations
{
    public partial class updateComponentConstant : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DFQuarterId",
                table: "ComponentConstants",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ComponentConstants_DFQuarterId",
                table: "ComponentConstants",
                column: "DFQuarterId");

            migrationBuilder.AddForeignKey(
                name: "FK_ComponentConstants_DFQuarters_DFQuarterId",
                table: "ComponentConstants",
                column: "DFQuarterId",
                principalTable: "DFQuarters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComponentConstants_DFQuarters_DFQuarterId",
                table: "ComponentConstants");

            migrationBuilder.DropIndex(
                name: "IX_ComponentConstants_DFQuarterId",
                table: "ComponentConstants");

            migrationBuilder.DropColumn(
                name: "DFQuarterId",
                table: "ComponentConstants");
        }
    }
}
