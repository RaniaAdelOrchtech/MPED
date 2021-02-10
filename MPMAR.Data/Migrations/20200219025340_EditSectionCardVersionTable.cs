using Microsoft.EntityFrameworkCore.Migrations;

namespace MPMAR.Data.Migrations
{
    public partial class EditSectionCardVersionTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "PageSectionCardVersions");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "PageSectionCardVersions");

            migrationBuilder.AddColumn<string>(
                name: "ArDescription",
                table: "PageSectionCardVersions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ArTitle",
                table: "PageSectionCardVersions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EnDescription",
                table: "PageSectionCardVersions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EnTitle",
                table: "PageSectionCardVersions",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ArDescription",
                table: "PageSectionCardVersions");

            migrationBuilder.DropColumn(
                name: "ArTitle",
                table: "PageSectionCardVersions");

            migrationBuilder.DropColumn(
                name: "EnDescription",
                table: "PageSectionCardVersions");

            migrationBuilder.DropColumn(
                name: "EnTitle",
                table: "PageSectionCardVersions");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "PageSectionCardVersions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "PageSectionCardVersions",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
