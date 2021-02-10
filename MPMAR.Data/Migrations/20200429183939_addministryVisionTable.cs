using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MPMAR.Data.Migrations
{
    public partial class addministryVisionTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovalDate",
                table: "MinistryVissions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApprovedById",
                table: "MinistryVissions",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "MinistryVissions",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "MinistryVissions",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModificationDate",
                table: "MinistryVissions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedById",
                table: "MinistryVissions",
                maxLength: 450,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MinistryVissionVersions",
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
                    ArTitle = table.Column<string>(maxLength: 100, nullable: false),
                    EnTitle = table.Column<string>(maxLength: 100, nullable: false),
                    ArDescription = table.Column<string>(maxLength: 1000, nullable: false),
                    EnDescription = table.Column<string>(maxLength: 1000, nullable: false),
                    Link = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ChangeActionEnum = table.Column<int>(nullable: true),
                    VersionStatusEnum = table.Column<int>(nullable: true),
                    MinistryVissionId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MinistryVissionVersions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MinistryVissionVersions_AspNetUsers_ApprovedById",
                        column: x => x.ApprovedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MinistryVissionVersions_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MinistryVissionVersions_MinistryVissions_MinistryVissionId",
                        column: x => x.MinistryVissionId,
                        principalTable: "MinistryVissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MinistryVissionVersions_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MinistryVissions_ApprovedById",
                table: "MinistryVissions",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_MinistryVissions_CreatedById",
                table: "MinistryVissions",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_MinistryVissions_ModifiedById",
                table: "MinistryVissions",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_MinistryVissionVersions_ApprovedById",
                table: "MinistryVissionVersions",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_MinistryVissionVersions_CreatedById",
                table: "MinistryVissionVersions",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_MinistryVissionVersions_MinistryVissionId",
                table: "MinistryVissionVersions",
                column: "MinistryVissionId");

            migrationBuilder.CreateIndex(
                name: "IX_MinistryVissionVersions_ModifiedById",
                table: "MinistryVissionVersions",
                column: "ModifiedById");

            migrationBuilder.AddForeignKey(
                name: "FK_MinistryVissions_AspNetUsers_ApprovedById",
                table: "MinistryVissions",
                column: "ApprovedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MinistryVissions_AspNetUsers_CreatedById",
                table: "MinistryVissions",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MinistryVissions_AspNetUsers_ModifiedById",
                table: "MinistryVissions",
                column: "ModifiedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MinistryVissions_AspNetUsers_ApprovedById",
                table: "MinistryVissions");

            migrationBuilder.DropForeignKey(
                name: "FK_MinistryVissions_AspNetUsers_CreatedById",
                table: "MinistryVissions");

            migrationBuilder.DropForeignKey(
                name: "FK_MinistryVissions_AspNetUsers_ModifiedById",
                table: "MinistryVissions");

            migrationBuilder.DropTable(
                name: "MinistryVissionVersions");

            migrationBuilder.DropIndex(
                name: "IX_MinistryVissions_ApprovedById",
                table: "MinistryVissions");

            migrationBuilder.DropIndex(
                name: "IX_MinistryVissions_CreatedById",
                table: "MinistryVissions");

            migrationBuilder.DropIndex(
                name: "IX_MinistryVissions_ModifiedById",
                table: "MinistryVissions");

            migrationBuilder.DropColumn(
                name: "ApprovalDate",
                table: "MinistryVissions");

            migrationBuilder.DropColumn(
                name: "ApprovedById",
                table: "MinistryVissions");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "MinistryVissions");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "MinistryVissions");

            migrationBuilder.DropColumn(
                name: "ModificationDate",
                table: "MinistryVissions");

            migrationBuilder.DropColumn(
                name: "ModifiedById",
                table: "MinistryVissions");
        }
    }
}
