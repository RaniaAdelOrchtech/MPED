using Microsoft.EntityFrameworkCore.Migrations;

namespace MPMAR.Data.Migrations
{
    public partial class UpdateVersionTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DynamicPageContentVersions_AspNetUsers_ApplicationUserId",
                table: "DynamicPageContentVersions");

            migrationBuilder.DropForeignKey(
                name: "FK_DynamicPageSectionVersions_AspNetUsers_ApplicationUserId",
                table: "DynamicPageSectionVersions");

            migrationBuilder.DropForeignKey(
                name: "FK_NavItemVersions_AspNetUsers_ApplicationUserId",
                table: "NavItemVersions");

            migrationBuilder.DropForeignKey(
                name: "FK_PageRouteVersions_AspNetUsers_ApplicationUserId",
                table: "PageRouteVersions");

            migrationBuilder.DropIndex(
                name: "IX_PageRouteVersions_ApplicationUserId",
                table: "PageRouteVersions");

            migrationBuilder.DropIndex(
                name: "IX_NavItemVersions_ApplicationUserId",
                table: "NavItemVersions");

            migrationBuilder.DropIndex(
                name: "IX_DynamicPageSectionVersions_ApplicationUserId",
                table: "DynamicPageSectionVersions");

            migrationBuilder.DropIndex(
                name: "IX_DynamicPageContentVersions_ApplicationUserId",
                table: "DynamicPageContentVersions");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "PageRouteVersions");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "NavItemVersions");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "DynamicPageSectionVersions");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "DynamicPageContentVersions");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "PageRouteVersions",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "NavItemVersions",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "DynamicPageSectionVersions",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "DynamicPageContentVersions",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PageRouteVersions_ApplicationUserId",
                table: "PageRouteVersions",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_NavItemVersions_ApplicationUserId",
                table: "NavItemVersions",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_DynamicPageSectionVersions_ApplicationUserId",
                table: "DynamicPageSectionVersions",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_DynamicPageContentVersions_ApplicationUserId",
                table: "DynamicPageContentVersions",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_DynamicPageContentVersions_AspNetUsers_ApplicationUserId",
                table: "DynamicPageContentVersions",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DynamicPageSectionVersions_AspNetUsers_ApplicationUserId",
                table: "DynamicPageSectionVersions",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_NavItemVersions_AspNetUsers_ApplicationUserId",
                table: "NavItemVersions",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PageRouteVersions_AspNetUsers_ApplicationUserId",
                table: "PageRouteVersions",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
