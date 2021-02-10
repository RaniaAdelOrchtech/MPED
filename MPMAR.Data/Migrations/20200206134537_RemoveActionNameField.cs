using Microsoft.EntityFrameworkCore.Migrations;

namespace MPMAR.Data.Migrations
{
    public partial class RemoveActionNameField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActionName",
                table: "PageRouteVersions");

            migrationBuilder.DropColumn(
                name: "ActionName",
                table: "PageRoutes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ActionName",
                table: "PageRouteVersions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ActionName",
                table: "PageRoutes",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
