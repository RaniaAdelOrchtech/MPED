using Microsoft.EntityFrameworkCore.Migrations;

namespace MPMAR.Analytics.Data.Migrations
{
    public partial class addtypetodfsector : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Quarter",
                table: "GrossDomesticComponent",
                newName: "_Source");

            migrationBuilder.RenameColumn(
                name: "Governorate",
                table: "GrossDomesticComponent",
                newName: "_Quarter");

            migrationBuilder.AddColumn<double>(
                name: "_Value",
                table: "GrossDomesticComponent",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "DFSectors",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "_Value",
                table: "GrossDomesticComponent");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "DFSectors");

            migrationBuilder.RenameColumn(
                name: "_Source",
                table: "GrossDomesticComponent",
                newName: "Quarter");

            migrationBuilder.RenameColumn(
                name: "_Quarter",
                table: "GrossDomesticComponent",
                newName: "Governorate");
        }
    }
}
