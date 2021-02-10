using Microsoft.EntityFrameworkCore.Migrations;

namespace MPMAR.Data.Migrations
{
    public partial class update_HP_basic_info : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FavIconUrl",
                table: "HomePageBasicInfo",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LogoUrl",
                table: "HomePageBasicInfo",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FavIconUrl",
                table: "HomePageBasicInfo");

            migrationBuilder.DropColumn(
                name: "LogoUrl",
                table: "HomePageBasicInfo");
        }
    }
}
