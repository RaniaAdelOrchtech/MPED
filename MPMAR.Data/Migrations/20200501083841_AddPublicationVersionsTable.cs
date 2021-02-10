using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MPMAR.Data.Migrations
{
    public partial class AddPublicationVersionsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovalDate",
                table: "Publications",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApprovedById",
                table: "Publications",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "Publications",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "Publications",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModificationDate",
                table: "Publications",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedById",
                table: "Publications",
                maxLength: 450,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PublicationVersions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    CreatedById = table.Column<string>(maxLength: 450, nullable: true),
                    ModificationDate = table.Column<DateTime>(nullable: true),
                    ModifiedById = table.Column<string>(maxLength: 450, nullable: true),
                    ApprovalDate = table.Column<DateTime>(nullable: true),
                    ApprovedById = table.Column<string>(maxLength: 450, nullable: true),
                    ArMainTitle = table.Column<string>(maxLength: 100, nullable: false),
                    EnMainTitle = table.Column<string>(maxLength: 100, nullable: false),
                    ArTitle1 = table.Column<string>(maxLength: 100, nullable: true),
                    EnTitle1 = table.Column<string>(maxLength: 100, nullable: true),
                    ArTitle2 = table.Column<string>(maxLength: 100, nullable: true),
                    EnTitle2 = table.Column<string>(maxLength: 100, nullable: true),
                    ArTitle3 = table.Column<string>(maxLength: 100, nullable: true),
                    EnTitle3 = table.Column<string>(maxLength: 100, nullable: true),
                    ArDescription1 = table.Column<string>(maxLength: 500, nullable: true),
                    EnDescription1 = table.Column<string>(maxLength: 500, nullable: true),
                    ArDescription2 = table.Column<string>(maxLength: 500, nullable: true),
                    EnDescription2 = table.Column<string>(maxLength: 500, nullable: true),
                    ArDescription3 = table.Column<string>(maxLength: 500, nullable: true),
                    EnDescription3 = table.Column<string>(maxLength: 500, nullable: true),
                    Image1 = table.Column<string>(nullable: false),
                    Image2 = table.Column<string>(nullable: true),
                    Image3 = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Link1 = table.Column<string>(nullable: false),
                    Link2 = table.Column<string>(nullable: false),
                    Link3 = table.Column<string>(nullable: false),
                    ChangeActionEnum = table.Column<int>(nullable: true),
                    VersionStatusEnum = table.Column<int>(nullable: true),
                    PublicationId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublicationVersions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PublicationVersions_AspNetUsers_ApprovedById",
                        column: x => x.ApprovedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PublicationVersions_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PublicationVersions_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PublicationVersions_Publications_PublicationId",
                        column: x => x.PublicationId,
                        principalTable: "Publications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Publications_ApprovedById",
                table: "Publications",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_Publications_CreatedById",
                table: "Publications",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Publications_ModifiedById",
                table: "Publications",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_PublicationVersions_ApprovedById",
                table: "PublicationVersions",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_PublicationVersions_CreatedById",
                table: "PublicationVersions",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_PublicationVersions_ModifiedById",
                table: "PublicationVersions",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_PublicationVersions_PublicationId",
                table: "PublicationVersions",
                column: "PublicationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Publications_AspNetUsers_ApprovedById",
                table: "Publications",
                column: "ApprovedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Publications_AspNetUsers_CreatedById",
                table: "Publications",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Publications_AspNetUsers_ModifiedById",
                table: "Publications",
                column: "ModifiedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Publications_AspNetUsers_ApprovedById",
                table: "Publications");

            migrationBuilder.DropForeignKey(
                name: "FK_Publications_AspNetUsers_CreatedById",
                table: "Publications");

            migrationBuilder.DropForeignKey(
                name: "FK_Publications_AspNetUsers_ModifiedById",
                table: "Publications");

            migrationBuilder.DropTable(
                name: "PublicationVersions");

            migrationBuilder.DropIndex(
                name: "IX_Publications_ApprovedById",
                table: "Publications");

            migrationBuilder.DropIndex(
                name: "IX_Publications_CreatedById",
                table: "Publications");

            migrationBuilder.DropIndex(
                name: "IX_Publications_ModifiedById",
                table: "Publications");

            migrationBuilder.DropColumn(
                name: "ApprovalDate",
                table: "Publications");

            migrationBuilder.DropColumn(
                name: "ApprovedById",
                table: "Publications");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Publications");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "Publications");

            migrationBuilder.DropColumn(
                name: "ModificationDate",
                table: "Publications");

            migrationBuilder.DropColumn(
                name: "ModifiedById",
                table: "Publications");
        }
    }
}
