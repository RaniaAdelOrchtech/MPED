using Microsoft.EntityFrameworkCore.Migrations;

namespace MPMAR.Data.Migrations
{
    public partial class editEconomicTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ArDescription",
                table: "EconomicDevelopments");

            migrationBuilder.DropColumn(
                name: "ArTitle",
                table: "EconomicDevelopments");

            migrationBuilder.DropColumn(
                name: "EnDescription",
                table: "EconomicDevelopments");

            migrationBuilder.DropColumn(
                name: "EnTitle",
                table: "EconomicDevelopments");

            migrationBuilder.DropColumn(
                name: "Url",
                table: "EconomicDevelopments");

            migrationBuilder.AddColumn<string>(
                name: "ArDescription1",
                table: "EconomicDevelopments",
                maxLength: 800,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ArDescription2",
                table: "EconomicDevelopments",
                maxLength: 800,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ArDescription3",
                table: "EconomicDevelopments",
                maxLength: 800,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ArMainTitle",
                table: "EconomicDevelopments",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ArTitle1",
                table: "EconomicDevelopments",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ArTitle2",
                table: "EconomicDevelopments",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ArTitle3",
                table: "EconomicDevelopments",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EnDescription1",
                table: "EconomicDevelopments",
                maxLength: 800,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EnDescription2",
                table: "EconomicDevelopments",
                maxLength: 800,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EnDescription3",
                table: "EconomicDevelopments",
                maxLength: 800,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EnMainTitle",
                table: "EconomicDevelopments",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EnTitle1",
                table: "EconomicDevelopments",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EnTitle2",
                table: "EconomicDevelopments",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EnTitle3",
                table: "EconomicDevelopments",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Url1",
                table: "EconomicDevelopments",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Url2",
                table: "EconomicDevelopments",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Url3",
                table: "EconomicDevelopments",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ArDescription1",
                table: "EconomicDevelopments");

            migrationBuilder.DropColumn(
                name: "ArDescription2",
                table: "EconomicDevelopments");

            migrationBuilder.DropColumn(
                name: "ArDescription3",
                table: "EconomicDevelopments");

            migrationBuilder.DropColumn(
                name: "ArMainTitle",
                table: "EconomicDevelopments");

            migrationBuilder.DropColumn(
                name: "ArTitle1",
                table: "EconomicDevelopments");

            migrationBuilder.DropColumn(
                name: "ArTitle2",
                table: "EconomicDevelopments");

            migrationBuilder.DropColumn(
                name: "ArTitle3",
                table: "EconomicDevelopments");

            migrationBuilder.DropColumn(
                name: "EnDescription1",
                table: "EconomicDevelopments");

            migrationBuilder.DropColumn(
                name: "EnDescription2",
                table: "EconomicDevelopments");

            migrationBuilder.DropColumn(
                name: "EnDescription3",
                table: "EconomicDevelopments");

            migrationBuilder.DropColumn(
                name: "EnMainTitle",
                table: "EconomicDevelopments");

            migrationBuilder.DropColumn(
                name: "EnTitle1",
                table: "EconomicDevelopments");

            migrationBuilder.DropColumn(
                name: "EnTitle2",
                table: "EconomicDevelopments");

            migrationBuilder.DropColumn(
                name: "EnTitle3",
                table: "EconomicDevelopments");

            migrationBuilder.DropColumn(
                name: "Url1",
                table: "EconomicDevelopments");

            migrationBuilder.DropColumn(
                name: "Url2",
                table: "EconomicDevelopments");

            migrationBuilder.DropColumn(
                name: "Url3",
                table: "EconomicDevelopments");

            migrationBuilder.AddColumn<string>(
                name: "ArDescription",
                table: "EconomicDevelopments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ArTitle",
                table: "EconomicDevelopments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EnDescription",
                table: "EconomicDevelopments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EnTitle",
                table: "EconomicDevelopments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "EconomicDevelopments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
