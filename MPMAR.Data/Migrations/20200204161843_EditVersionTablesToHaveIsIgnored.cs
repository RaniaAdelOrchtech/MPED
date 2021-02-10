using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MPMAR.Data.Migrations
{
    public partial class EditVersionTablesToHaveIsIgnored : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NavItemVersions_AspNetUsers_ModifiedById",
                table: "NavItemVersions");

            migrationBuilder.DropForeignKey(
                name: "FK_PageRouteVersions_AspNetUsers_ModifiedById",
                table: "PageRouteVersions");

            migrationBuilder.DropIndex(
                name: "IX_PageRouteVersions_ModifiedById",
                table: "PageRouteVersions");

            migrationBuilder.DropIndex(
                name: "IX_NavItemVersions_ModifiedById",
                table: "NavItemVersions");

            migrationBuilder.DropColumn(
                name: "ModificationDate",
                table: "PageRouteVersions");

            migrationBuilder.DropColumn(
                name: "ModifiedById",
                table: "PageRouteVersions");

            migrationBuilder.DropColumn(
                name: "ModificationDate",
                table: "NavItemVersions");

            migrationBuilder.DropColumn(
                name: "ModifiedById",
                table: "NavItemVersions");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "PageRouteVersions",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsIgnored",
                table: "PageRouteVersions",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "NavItemVersions",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsIgnored",
                table: "NavItemVersions",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_PageRouteVersions_ApplicationUserId",
                table: "PageRouteVersions",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_NavItemVersions_ApplicationUserId",
                table: "NavItemVersions",
                column: "ApplicationUserId");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "PageRouteVersions");

            migrationBuilder.DropColumn(
                name: "IsIgnored",
                table: "PageRouteVersions");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "NavItemVersions");

            migrationBuilder.DropColumn(
                name: "IsIgnored",
                table: "NavItemVersions");

            migrationBuilder.AddColumn<DateTime>(
                name: "ModificationDate",
                table: "PageRouteVersions",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedById",
                table: "PageRouteVersions",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModificationDate",
                table: "NavItemVersions",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedById",
                table: "NavItemVersions",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PageRouteVersions_ModifiedById",
                table: "PageRouteVersions",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_NavItemVersions_ModifiedById",
                table: "NavItemVersions",
                column: "ModifiedById");

            migrationBuilder.AddForeignKey(
                name: "FK_NavItemVersions_AspNetUsers_ModifiedById",
                table: "NavItemVersions",
                column: "ModifiedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PageRouteVersions_AspNetUsers_ModifiedById",
                table: "PageRouteVersions",
                column: "ModifiedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
