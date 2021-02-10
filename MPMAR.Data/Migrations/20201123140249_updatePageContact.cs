using Microsoft.EntityFrameworkCore.Migrations;

namespace MPMAR.Data.Migrations
{
    public partial class updatePageContact : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("update PageContact set PageRouteVersionId=(select max(Id) from PageRouteVersions)");

            migrationBuilder.AlterColumn<int>(
                name: "PageRouteVersionId",
                table: "PageContact",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_PageContact_PageRouteVersions_PageRouteVersionId",
                table: "PageContact",
                column: "PageRouteVersionId",
                principalTable: "PageRouteVersions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.Sql("update PageContact set PageRouteVersionId=NULL");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
        

            migrationBuilder.AlterColumn<int>(
                name: "PageRouteVersionId",
                table: "PageContact",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PageContact_PageRouteVersions_PageRouteVersionId",
                table: "PageContact",
                column: "PageRouteVersionId",
                principalTable: "PageRouteVersions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
