using Microsoft.EntityFrameworkCore.Migrations;

namespace MPMAR.Data.Migrations
{
    public partial class addBEUsersPrivileges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BEUsersPrivileges",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PageRouteId = table.Column<int>(nullable: true),
                    PageTypeId = table.Column<int>(nullable: false),
                    ApplicationUserId = table.Column<string>(nullable: true),
                    CanView = table.Column<bool>(nullable: true),
                    CanAdd = table.Column<bool>(nullable: true),
                    CanEdit = table.Column<bool>(nullable: true),
                    CanDelete = table.Column<bool>(nullable: true),
                    CanApprove = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BEUsersPrivileges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BEUsersPrivileges_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BEUsersPrivileges_PageRoutes_PageRouteId",
                        column: x => x.PageRouteId,
                        principalTable: "PageRoutes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BEUsersPrivileges_ApplicationUserId",
                table: "BEUsersPrivileges",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BEUsersPrivileges_PageRouteId",
                table: "BEUsersPrivileges",
                column: "PageRouteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BEUsersPrivileges");
        }
    }
}
