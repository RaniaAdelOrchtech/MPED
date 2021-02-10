using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MPMAR.Data.Migrations
{
    public partial class AddMonitringVersionsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovalDate",
                table: "Monitoring",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApprovedById",
                table: "Monitoring",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "Monitoring",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "Monitoring",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModificationDate",
                table: "Monitoring",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedById",
                table: "Monitoring",
                maxLength: 450,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MonitoringVersions",
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
                    ArMainTitle = table.Column<string>(maxLength: 500, nullable: false),
                    EnMainTitle = table.Column<string>(maxLength: 500, nullable: false),
                    BackGroundImage = table.Column<string>(nullable: true),
                    Image1 = table.Column<string>(nullable: false),
                    ArDescription1 = table.Column<string>(maxLength: 800, nullable: false),
                    EnDescription1 = table.Column<string>(maxLength: 800, nullable: false),
                    Link1 = table.Column<string>(nullable: false),
                    ArTitle2 = table.Column<string>(maxLength: 200, nullable: false),
                    EnTitle2 = table.Column<string>(maxLength: 200, nullable: false),
                    ArDescription2 = table.Column<string>(maxLength: 800, nullable: false),
                    EnDescription2 = table.Column<string>(maxLength: 800, nullable: false),
                    Link2 = table.Column<string>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ChangeActionEnum = table.Column<int>(nullable: true),
                    VersionStatusEnum = table.Column<int>(nullable: true),
                    MonitoringId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonitoringVersions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MonitoringVersions_AspNetUsers_ApprovedById",
                        column: x => x.ApprovedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MonitoringVersions_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MonitoringVersions_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MonitoringVersions_Monitoring_MonitoringId",
                        column: x => x.MonitoringId,
                        principalTable: "Monitoring",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Monitoring_ApprovedById",
                table: "Monitoring",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_Monitoring_CreatedById",
                table: "Monitoring",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Monitoring_ModifiedById",
                table: "Monitoring",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_MonitoringVersions_ApprovedById",
                table: "MonitoringVersions",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_MonitoringVersions_CreatedById",
                table: "MonitoringVersions",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_MonitoringVersions_ModifiedById",
                table: "MonitoringVersions",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_MonitoringVersions_MonitoringId",
                table: "MonitoringVersions",
                column: "MonitoringId");

            migrationBuilder.AddForeignKey(
                name: "FK_Monitoring_AspNetUsers_ApprovedById",
                table: "Monitoring",
                column: "ApprovedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Monitoring_AspNetUsers_CreatedById",
                table: "Monitoring",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Monitoring_AspNetUsers_ModifiedById",
                table: "Monitoring",
                column: "ModifiedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Monitoring_AspNetUsers_ApprovedById",
                table: "Monitoring");

            migrationBuilder.DropForeignKey(
                name: "FK_Monitoring_AspNetUsers_CreatedById",
                table: "Monitoring");

            migrationBuilder.DropForeignKey(
                name: "FK_Monitoring_AspNetUsers_ModifiedById",
                table: "Monitoring");

            migrationBuilder.DropTable(
                name: "MonitoringVersions");

            migrationBuilder.DropIndex(
                name: "IX_Monitoring_ApprovedById",
                table: "Monitoring");

            migrationBuilder.DropIndex(
                name: "IX_Monitoring_CreatedById",
                table: "Monitoring");

            migrationBuilder.DropIndex(
                name: "IX_Monitoring_ModifiedById",
                table: "Monitoring");

            migrationBuilder.DropColumn(
                name: "ApprovalDate",
                table: "Monitoring");

            migrationBuilder.DropColumn(
                name: "ApprovedById",
                table: "Monitoring");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Monitoring");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "Monitoring");

            migrationBuilder.DropColumn(
                name: "ModificationDate",
                table: "Monitoring");

            migrationBuilder.DropColumn(
                name: "ModifiedById",
                table: "Monitoring");
        }
    }
}
