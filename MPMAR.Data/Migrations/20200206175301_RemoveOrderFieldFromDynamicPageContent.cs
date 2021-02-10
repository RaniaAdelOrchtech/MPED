using Microsoft.EntityFrameworkCore.Migrations;

namespace MPMAR.Data.Migrations
{
    public partial class RemoveOrderFieldFromDynamicPageContent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Order",
                table: "DynamicPageContentVersions");

            migrationBuilder.DropColumn(
                name: "Order",
                table: "DynamicPageContents");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "DynamicPageContentVersions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "DynamicPageContents",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
