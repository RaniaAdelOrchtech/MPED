using Microsoft.EntityFrameworkCore.Migrations;

namespace MPMAR.Data.Migrations
{
    public partial class RemoveUniqueIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex("IX_PageSectionVersions_PageSectionId", "PageSectionVersions");
            migrationBuilder.CreateIndex(
              name: "IX_PageSectionVersions_PageSectionId",
              table: "PageSectionVersions",
              column: "PageSectionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
