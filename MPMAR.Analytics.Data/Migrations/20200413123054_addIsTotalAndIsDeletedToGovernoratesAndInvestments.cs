using Microsoft.EntityFrameworkCore.Migrations;

namespace MPMAR.Analytics.Data.Migrations
{
    public partial class addIsTotalAndIsDeletedToGovernoratesAndInvestments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "Investments",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "Governorates",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isTotal",
                table: "DFGovernorates",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "Investments");

            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "Governorates");

            migrationBuilder.DropColumn(
                name: "isTotal",
                table: "DFGovernorates");
        }
    }
}
