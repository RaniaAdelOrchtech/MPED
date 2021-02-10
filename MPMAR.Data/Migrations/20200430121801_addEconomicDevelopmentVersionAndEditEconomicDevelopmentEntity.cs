using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MPMAR.Data.Migrations
{
    public partial class addEconomicDevelopmentVersionAndEditEconomicDevelopmentEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovalDate",
                table: "EconomicDevelopments",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApprovedById",
                table: "EconomicDevelopments",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "EconomicDevelopments",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "EconomicDevelopments",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModificationDate",
                table: "EconomicDevelopments",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedById",
                table: "EconomicDevelopments",
                maxLength: 450,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EconomicDevelopments_ApprovedById",
                table: "EconomicDevelopments",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_EconomicDevelopments_CreatedById",
                table: "EconomicDevelopments",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_EconomicDevelopments_ModifiedById",
                table: "EconomicDevelopments",
                column: "ModifiedById");

            migrationBuilder.AddForeignKey(
                name: "FK_EconomicDevelopments_AspNetUsers_ApprovedById",
                table: "EconomicDevelopments",
                column: "ApprovedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EconomicDevelopments_AspNetUsers_CreatedById",
                table: "EconomicDevelopments",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EconomicDevelopments_AspNetUsers_ModifiedById",
                table: "EconomicDevelopments",
                column: "ModifiedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EconomicDevelopments_AspNetUsers_ApprovedById",
                table: "EconomicDevelopments");

            migrationBuilder.DropForeignKey(
                name: "FK_EconomicDevelopments_AspNetUsers_CreatedById",
                table: "EconomicDevelopments");

            migrationBuilder.DropForeignKey(
                name: "FK_EconomicDevelopments_AspNetUsers_ModifiedById",
                table: "EconomicDevelopments");

            migrationBuilder.DropIndex(
                name: "IX_EconomicDevelopments_ApprovedById",
                table: "EconomicDevelopments");

            migrationBuilder.DropIndex(
                name: "IX_EconomicDevelopments_CreatedById",
                table: "EconomicDevelopments");

            migrationBuilder.DropIndex(
                name: "IX_EconomicDevelopments_ModifiedById",
                table: "EconomicDevelopments");

            migrationBuilder.DropColumn(
                name: "ApprovalDate",
                table: "EconomicDevelopments");

            migrationBuilder.DropColumn(
                name: "ApprovedById",
                table: "EconomicDevelopments");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "EconomicDevelopments");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "EconomicDevelopments");

            migrationBuilder.DropColumn(
                name: "ModificationDate",
                table: "EconomicDevelopments");

            migrationBuilder.DropColumn(
                name: "ModifiedById",
                table: "EconomicDevelopments");
        }
    }
}
