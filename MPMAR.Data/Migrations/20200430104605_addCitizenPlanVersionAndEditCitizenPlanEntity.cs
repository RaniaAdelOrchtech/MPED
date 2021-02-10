using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MPMAR.Data.Migrations
{
    public partial class addCitizenPlanVersionAndEditCitizenPlanEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovalDate",
                table: "CitizenPlan",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApprovedById",
                table: "CitizenPlan",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "CitizenPlan",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "CitizenPlan",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModificationDate",
                table: "CitizenPlan",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedById",
                table: "CitizenPlan",
                maxLength: 450,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CitizenPlanVersions",
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
                    ArTitle = table.Column<string>(maxLength: 100, nullable: true),
                    EnTitle = table.Column<string>(maxLength: 100, nullable: true),
                    ArDescription = table.Column<string>(maxLength: 1000, nullable: false),
                    EnDescription = table.Column<string>(maxLength: 1000, nullable: false),
                    Link = table.Column<string>(nullable: true),
                    Image = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ChangeActionEnum = table.Column<int>(nullable: true),
                    VersionStatusEnum = table.Column<int>(nullable: true),
                    CitizenPlanId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CitizenPlanVersions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CitizenPlanVersions_AspNetUsers_ApprovedById",
                        column: x => x.ApprovedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CitizenPlanVersions_CitizenPlan_CitizenPlanId",
                        column: x => x.CitizenPlanId,
                        principalTable: "CitizenPlan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CitizenPlanVersions_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CitizenPlanVersions_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CitizenPlan_ApprovedById",
                table: "CitizenPlan",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_CitizenPlan_CreatedById",
                table: "CitizenPlan",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_CitizenPlan_ModifiedById",
                table: "CitizenPlan",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_CitizenPlanVersions_ApprovedById",
                table: "CitizenPlanVersions",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_CitizenPlanVersions_CitizenPlanId",
                table: "CitizenPlanVersions",
                column: "CitizenPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_CitizenPlanVersions_CreatedById",
                table: "CitizenPlanVersions",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_CitizenPlanVersions_ModifiedById",
                table: "CitizenPlanVersions",
                column: "ModifiedById");

            migrationBuilder.AddForeignKey(
                name: "FK_CitizenPlan_AspNetUsers_ApprovedById",
                table: "CitizenPlan",
                column: "ApprovedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CitizenPlan_AspNetUsers_CreatedById",
                table: "CitizenPlan",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CitizenPlan_AspNetUsers_ModifiedById",
                table: "CitizenPlan",
                column: "ModifiedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CitizenPlan_AspNetUsers_ApprovedById",
                table: "CitizenPlan");

            migrationBuilder.DropForeignKey(
                name: "FK_CitizenPlan_AspNetUsers_CreatedById",
                table: "CitizenPlan");

            migrationBuilder.DropForeignKey(
                name: "FK_CitizenPlan_AspNetUsers_ModifiedById",
                table: "CitizenPlan");

            migrationBuilder.DropTable(
                name: "CitizenPlanVersions");

            migrationBuilder.DropIndex(
                name: "IX_CitizenPlan_ApprovedById",
                table: "CitizenPlan");

            migrationBuilder.DropIndex(
                name: "IX_CitizenPlan_CreatedById",
                table: "CitizenPlan");

            migrationBuilder.DropIndex(
                name: "IX_CitizenPlan_ModifiedById",
                table: "CitizenPlan");

            migrationBuilder.DropColumn(
                name: "ApprovalDate",
                table: "CitizenPlan");

            migrationBuilder.DropColumn(
                name: "ApprovedById",
                table: "CitizenPlan");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "CitizenPlan");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "CitizenPlan");

            migrationBuilder.DropColumn(
                name: "ModificationDate",
                table: "CitizenPlan");

            migrationBuilder.DropColumn(
                name: "ModifiedById",
                table: "CitizenPlan");
        }
    }
}
