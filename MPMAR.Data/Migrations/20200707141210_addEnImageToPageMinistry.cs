using Microsoft.EntityFrameworkCore.Migrations;

namespace MPMAR.Data.Migrations
{
    public partial class addEnImageToPageMinistry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EnImageUrl",
                table: "PageMinistryVersions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EnImageUrl",
                table: "PageMinistry",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EnImageUrl",
                table: "PageMinistryVersions");

            migrationBuilder.DropColumn(
                name: "EnImageUrl",
                table: "PageMinistry");
        }
    }
}
