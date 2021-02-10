using Microsoft.EntityFrameworkCore.Migrations;

namespace MPMAR.Data.Migrations
{
    public partial class updateNavItems : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NavItems_NavItemVersions_NavItemVersionId",
                table: "NavItems");

            migrationBuilder.DropIndex(
                name: "IX_NavItems_NavItemVersionId",
                table: "NavItems");

            migrationBuilder.DropColumn(
                name: "NavItemVersionId",
                table: "NavItems");

            migrationBuilder.AlterColumn<int>(
                name: "Order",
                table: "NavItemVersions",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EnName",
                table: "NavItemVersions",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ArName",
                table: "NavItemVersions",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ChangeActionEnum",
                table: "NavItemVersions",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NavItemId",
                table: "NavItemVersions",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VersionStatusEnum",
                table: "NavItemVersions",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_NavItemVersions_NavItemId",
                table: "NavItemVersions",
                column: "NavItemId");

            migrationBuilder.CreateIndex(
                name: "IX_NavItemVersions_ParentNavItemId",
                table: "NavItemVersions",
                column: "ParentNavItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_NavItemVersions_NavItems_NavItemId",
                table: "NavItemVersions",
                column: "NavItemId",
                principalTable: "NavItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_NavItemVersions_NavItems_ParentNavItemId",
                table: "NavItemVersions",
                column: "ParentNavItemId",
                principalTable: "NavItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NavItemVersions_NavItems_NavItemId",
                table: "NavItemVersions");

            migrationBuilder.DropForeignKey(
                name: "FK_NavItemVersions_NavItems_ParentNavItemId",
                table: "NavItemVersions");

            migrationBuilder.DropIndex(
                name: "IX_NavItemVersions_NavItemId",
                table: "NavItemVersions");

            migrationBuilder.DropIndex(
                name: "IX_NavItemVersions_ParentNavItemId",
                table: "NavItemVersions");

            migrationBuilder.DropColumn(
                name: "ChangeActionEnum",
                table: "NavItemVersions");

            migrationBuilder.DropColumn(
                name: "NavItemId",
                table: "NavItemVersions");

            migrationBuilder.DropColumn(
                name: "VersionStatusEnum",
                table: "NavItemVersions");

            migrationBuilder.AlterColumn<int>(
                name: "Order",
                table: "NavItemVersions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "EnName",
                table: "NavItemVersions",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "ArName",
                table: "NavItemVersions",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100);

            migrationBuilder.AddColumn<int>(
                name: "NavItemVersionId",
                table: "NavItems",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_NavItems_NavItemVersionId",
                table: "NavItems",
                column: "NavItemVersionId",
                unique: true,
                filter: "[NavItemVersionId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_NavItems_NavItemVersions_NavItemVersionId",
                table: "NavItems",
                column: "NavItemVersionId",
                principalTable: "NavItemVersions",
                principalColumn: "Id");
        }
    }
}
