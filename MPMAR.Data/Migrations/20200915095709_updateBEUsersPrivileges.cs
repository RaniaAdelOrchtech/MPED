using Microsoft.EntityFrameworkCore.Migrations;

namespace MPMAR.Data.Migrations
{
    public partial class updateBEUsersPrivileges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BEUsersPrivileges_AspNetUsers_ApplicationUserId",
                table: "BEUsersPrivileges");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "BEUsersPrivileges",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "BEUsersPrivileges",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_BEUsersPrivileges_AspNetUsers_ApplicationUserId",
                table: "BEUsersPrivileges",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BEUsersPrivileges_AspNetUsers_ApplicationUserId",
                table: "BEUsersPrivileges");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "BEUsersPrivileges");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "BEUsersPrivileges",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddForeignKey(
                name: "FK_BEUsersPrivileges_AspNetUsers_ApplicationUserId",
                table: "BEUsersPrivileges",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
