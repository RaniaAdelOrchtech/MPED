using Microsoft.EntityFrameworkCore.Migrations;

namespace MPMAR.Data.Migrations
{
    public partial class EditDynamicPageSectionTypeTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasCards",
                table: "DynamicPageSectionTypes",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "MediaType",
                table: "DynamicPageSectionTypes",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasCards",
                table: "DynamicPageSectionTypes");

            migrationBuilder.DropColumn(
                name: "MediaType",
                table: "DynamicPageSectionTypes");
        }
    }
}
