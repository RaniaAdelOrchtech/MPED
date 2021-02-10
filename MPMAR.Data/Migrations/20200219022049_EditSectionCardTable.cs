using Microsoft.EntityFrameworkCore.Migrations;

namespace MPMAR.Data.Migrations
{
    public partial class EditSectionCardTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "PageSectionCardVersions",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "PageSectionCardVersions",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "PageSectionCards",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "PageSectionCards",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "PageSectionCardVersions");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "PageSectionCardVersions");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "PageSectionCards");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "PageSectionCards");
        }
    }
}
