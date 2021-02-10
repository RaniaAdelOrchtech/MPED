using Microsoft.EntityFrameworkCore.Migrations;

namespace MPMAR.Data.Migrations
{
    public partial class EditPageSectionTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PageSectionCards_PageSections_PageSectionId",
                table: "PageSectionCards");

            migrationBuilder.DropColumn(
                name: "ImageAlt",
                table: "PageSectionCards");

            migrationBuilder.AlterColumn<int>(
                name: "PageSectionId",
                table: "PageSectionCards",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ArImageAlt",
                table: "PageSectionCards",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EnImageAlt",
                table: "PageSectionCards",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PageSectionCards_PageSections_PageSectionId",
                table: "PageSectionCards",
                column: "PageSectionId",
                principalTable: "PageSections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PageSectionCards_PageSections_PageSectionId",
                table: "PageSectionCards");

            migrationBuilder.DropColumn(
                name: "ArImageAlt",
                table: "PageSectionCards");

            migrationBuilder.DropColumn(
                name: "EnImageAlt",
                table: "PageSectionCards");

            migrationBuilder.AlterColumn<int>(
                name: "PageSectionId",
                table: "PageSectionCards",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<string>(
                name: "ImageAlt",
                table: "PageSectionCards",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PageSectionCards_PageSections_PageSectionId",
                table: "PageSectionCards",
                column: "PageSectionId",
                principalTable: "PageSections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
