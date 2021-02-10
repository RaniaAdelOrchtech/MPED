using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MPMAR.Analytics.Data.Migrations
{
    public partial class addCreatedById : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "SectorGrowthRateVersion",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "SectorGrowthRateVersion",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "RGDPGrowthRateVersions",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "RGDPGrowthRateVersions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "InvestmentVersions",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "InvestmentVersions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "GovernorateVersions",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "GovernorateVersions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "ComponentCurrentVersions",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "ComponentCurrentVersions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "ComponentConstantVersions",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "ComponentConstantVersions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "ActivityCurrentVersions",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "ActivityCurrentVersions",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "SectorGrowthRateVersion");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "SectorGrowthRateVersion");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "RGDPGrowthRateVersions");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "RGDPGrowthRateVersions");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "InvestmentVersions");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "InvestmentVersions");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "GovernorateVersions");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "GovernorateVersions");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "ComponentCurrentVersions");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "ComponentCurrentVersions");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "ComponentConstantVersions");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "ComponentConstantVersions");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "ActivityCurrentVersions");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "ActivityCurrentVersions");
        }
    }
}
