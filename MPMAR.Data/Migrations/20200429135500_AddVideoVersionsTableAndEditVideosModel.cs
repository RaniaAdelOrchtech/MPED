using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MPMAR.Data.Migrations
{
    public partial class AddVideoVersionsTableAndEditVideosModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovalDate",
                table: "homePageVideos",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApprovedById",
                table: "homePageVideos",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "homePageVideos",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "homePageVideos",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModificationDate",
                table: "homePageVideos",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedById",
                table: "homePageVideos",
                maxLength: 450,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_homePageVideos_ApprovedById",
                table: "homePageVideos",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_homePageVideos_CreatedById",
                table: "homePageVideos",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_homePageVideos_ModifiedById",
                table: "homePageVideos",
                column: "ModifiedById");

            migrationBuilder.AddForeignKey(
                name: "FK_homePageVideos_AspNetUsers_ApprovedById",
                table: "homePageVideos",
                column: "ApprovedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_homePageVideos_AspNetUsers_CreatedById",
                table: "homePageVideos",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_homePageVideos_AspNetUsers_ModifiedById",
                table: "homePageVideos",
                column: "ModifiedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_homePageVideos_AspNetUsers_ApprovedById",
                table: "homePageVideos");

            migrationBuilder.DropForeignKey(
                name: "FK_homePageVideos_AspNetUsers_CreatedById",
                table: "homePageVideos");

            migrationBuilder.DropForeignKey(
                name: "FK_homePageVideos_AspNetUsers_ModifiedById",
                table: "homePageVideos");

            migrationBuilder.DropIndex(
                name: "IX_homePageVideos_ApprovedById",
                table: "homePageVideos");

            migrationBuilder.DropIndex(
                name: "IX_homePageVideos_CreatedById",
                table: "homePageVideos");

            migrationBuilder.DropIndex(
                name: "IX_homePageVideos_ModifiedById",
                table: "homePageVideos");

            migrationBuilder.DropColumn(
                name: "ApprovalDate",
                table: "homePageVideos");

            migrationBuilder.DropColumn(
                name: "ApprovedById",
                table: "homePageVideos");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "homePageVideos");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "homePageVideos");

            migrationBuilder.DropColumn(
                name: "ModificationDate",
                table: "homePageVideos");

            migrationBuilder.DropColumn(
                name: "ModifiedById",
                table: "homePageVideos");
        }
    }
}
