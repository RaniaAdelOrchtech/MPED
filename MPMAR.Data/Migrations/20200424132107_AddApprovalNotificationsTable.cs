using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MPMAR.Data.Migrations
{
    public partial class AddApprovalNotificationsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApprovalNotifications",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContentManagerId = table.Column<string>(nullable: false),
                    ChangesDateTime = table.Column<DateTime>(nullable: false),
                    PageName = table.Column<string>(nullable: false),
                    PageLink = table.Column<string>(nullable: false),
                    ChangeAction = table.Column<int>(nullable: false),
                    ChangeType = table.Column<int>(nullable: false),
                    PageType = table.Column<int>(nullable: false),
                    VersionStatusEnum = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApprovalNotifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApprovalNotifications_AspNetUsers_ContentManagerId",
                        column: x => x.ContentManagerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApprovalNotifications_ContentManagerId",
                table: "ApprovalNotifications",
                column: "ContentManagerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApprovalNotifications");
        }
    }
}
