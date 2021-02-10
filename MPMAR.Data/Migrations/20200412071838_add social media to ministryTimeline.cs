using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MPMAR.Data.Migrations
{
    public partial class addsocialmediatoministryTimeline : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
    
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "MinistryTimeLine",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Facebook",
                table: "MinistryTimeLine",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Twitter",
                table: "MinistryTimeLine",
                nullable: true);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LeftMenuItem");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "MinistryTimeLine");

            migrationBuilder.DropColumn(
                name: "Facebook",
                table: "MinistryTimeLine");

            migrationBuilder.DropColumn(
                name: "Twitter",
                table: "MinistryTimeLine");

            migrationBuilder.AlterColumn<int>(
                name: "Order",
                table: "PhotosAlbum",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Order",
                table: "PhotoArchive",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Order",
                table: "EgyptVision",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
