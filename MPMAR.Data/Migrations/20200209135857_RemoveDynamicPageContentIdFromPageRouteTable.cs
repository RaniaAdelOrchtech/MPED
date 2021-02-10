using Microsoft.EntityFrameworkCore.Migrations;

namespace MPMAR.Data.Migrations
{
    public partial class RemoveDynamicPageContentIdFromPageRouteTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DynamicPageContentId",
                table: "PageRoutes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DynamicPageContentId",
                table: "PageRoutes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
