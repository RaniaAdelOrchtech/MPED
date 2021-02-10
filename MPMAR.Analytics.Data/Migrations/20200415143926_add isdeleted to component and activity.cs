using Microsoft.EntityFrameworkCore.Migrations;

namespace MPMAR.Analytics.Data.Migrations
{
    public partial class addisdeletedtocomponentandactivity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "ComponentCurrents",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "ComponentConstants",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "ActivityCurrents",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "ActivityConstants",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "ComponentCurrents");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "ComponentConstants");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "ActivityCurrents");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "ActivityConstants");
        }
    }
}
