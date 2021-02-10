using Microsoft.EntityFrameworkCore.Migrations;

namespace MPMAR.Data.Migrations
{
    public partial class AddBackGroundImageToEcoDevelopmentHomePage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BackGroundImage",
                table: "EconomicDevelopmentVersions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BackGroundImage",
                table: "EconomicDevelopments",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BackGroundImage",
                table: "EconomicDevelopmentVersions");

            migrationBuilder.DropColumn(
                name: "BackGroundImage",
                table: "EconomicDevelopments");
        }
    }
}
