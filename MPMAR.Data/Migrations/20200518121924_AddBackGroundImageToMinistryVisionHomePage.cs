using Microsoft.EntityFrameworkCore.Migrations;

namespace MPMAR.Data.Migrations
{
    public partial class AddBackGroundImageToMinistryVisionHomePage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BackGroundImage",
                table: "MinistryVissionVersions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BackGroundImage",
                table: "MinistryVissions",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BackGroundImage",
                table: "MinistryVissionVersions");

            migrationBuilder.DropColumn(
                name: "BackGroundImage",
                table: "MinistryVissions");
        }
    }
}
