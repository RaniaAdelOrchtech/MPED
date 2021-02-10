using Microsoft.EntityFrameworkCore.Migrations;

namespace MPMAR.Data.Migrations
{
    public partial class AddFirstLoginProbertyToApplicationUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isFirstLogin",
                table: "AspNetUsers",
                nullable: true);

            /*migrationBuilder.CreateTable(
                name: "NewsTypesForNewsVersions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PageNewsVersionId = table.Column<int>(nullable: false),
                    NewsTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsTypesForNewsVersions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NewsTypesForNewsVersions_PageNewsType_NewsTypeId",
                        column: x => x.NewsTypeId,
                        principalTable: "PageNewsType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NewsTypesForNewsVersions_PageNewsVersions_PageNewsVersionId",
                        column: x => x.PageNewsVersionId,
                        principalTable: "PageNewsVersions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NewsTypesForNewsVersions_NewsTypeId",
                table: "NewsTypesForNewsVersions",
                column: "NewsTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_NewsTypesForNewsVersions_PageNewsVersionId",
                table: "NewsTypesForNewsVersions",
                column: "PageNewsVersionId");*/
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            /*migrationBuilder.DropTable(
                name: "NewsTypesForNewsVersions");*/

            migrationBuilder.DropColumn(
                name: "isFirstLogin",
                table: "AspNetUsers");
        }
    }
}
