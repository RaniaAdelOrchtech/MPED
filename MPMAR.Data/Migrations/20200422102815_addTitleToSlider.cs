using Microsoft.EntityFrameworkCore.Migrations;

namespace MPMAR.Data.Migrations
{
    public partial class addTitleToSlider : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ArTitle",
                table: "homePagePhotoSlider",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EnTitle",
                table: "homePagePhotoSlider",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ArTitle",
                table: "homePagePhotoSlider");

            migrationBuilder.DropColumn(
                name: "EnTitle",
                table: "homePagePhotoSlider");
        }
    }
}
