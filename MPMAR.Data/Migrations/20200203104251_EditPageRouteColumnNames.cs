using Microsoft.EntityFrameworkCore.Migrations;

namespace MPMAR.Data.Migrations
{
    public partial class EditPageRouteColumnNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ArPageName",
                table: "PageRoutes");

            migrationBuilder.DropColumn(
                name: "EnPageName",
                table: "PageRoutes");

            migrationBuilder.AddColumn<string>(
                name: "ArName",
                table: "PageRoutes",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EnName",
                table: "PageRoutes",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ArName",
                table: "PageRoutes");

            migrationBuilder.DropColumn(
                name: "EnName",
                table: "PageRoutes");

            migrationBuilder.AddColumn<string>(
                name: "ArPageName",
                table: "PageRoutes",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EnPageName",
                table: "PageRoutes",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }
    }
}
