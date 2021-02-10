using Microsoft.EntityFrameworkCore.Migrations;

namespace MPMAR.Analytics.Data.Migrations
{
    public partial class updatedfgdpbyaddingselfrelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DFGDPId",
                table: "DFGDP",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DFGDP_DFGDPId",
                table: "DFGDP",
                column: "DFGDPId");

            migrationBuilder.AddForeignKey(
                name: "FK_DFGDP_DFGDP_DFGDPId",
                table: "DFGDP",
                column: "DFGDPId",
                principalTable: "DFGDP",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DFGDP_DFGDP_DFGDPId",
                table: "DFGDP");

            migrationBuilder.DropIndex(
                name: "IX_DFGDP_DFGDPId",
                table: "DFGDP");

            migrationBuilder.DropColumn(
                name: "DFGDPId",
                table: "DFGDP");
        }
    }
}
