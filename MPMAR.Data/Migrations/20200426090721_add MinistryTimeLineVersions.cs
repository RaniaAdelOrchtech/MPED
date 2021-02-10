using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MPMAR.Data.Migrations
{
    public partial class addMinistryTimeLineVersions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MinistryTimeLineVersions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    CreatedById = table.Column<string>(maxLength: 450, nullable: true),
                    ApprovalDate = table.Column<DateTime>(nullable: true),
                    ApprovedById = table.Column<string>(maxLength: 450, nullable: true),
                    SeoTitleEN = table.Column<string>(nullable: true),
                    SeoTitleAR = table.Column<string>(nullable: true),
                    SeoDescriptionEN = table.Column<string>(nullable: true),
                    SeoDescriptionAR = table.Column<string>(nullable: true),
                    SeoOgTitleEN = table.Column<string>(nullable: true),
                    SeoOgTitleAR = table.Column<string>(nullable: true),
                    SeoTwitterCardEN = table.Column<string>(nullable: true),
                    SeoTwitterCardAR = table.Column<string>(nullable: true),
                    EnName = table.Column<string>(nullable: true),
                    ArName = table.Column<string>(nullable: true),
                    EnDescription = table.Column<string>(nullable: true),
                    ArDescription = table.Column<string>(nullable: true),
                    ProfileImageUrl = table.Column<string>(nullable: true),
                    Order = table.Column<int>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    EventSocialLinks = table.Column<string>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: true),
                    EndDate = table.Column<DateTime>(nullable: true),
                    PeriodAr = table.Column<string>(maxLength: 30, nullable: true),
                    PeriodEn = table.Column<string>(maxLength: 30, nullable: true),
                    Facebook = table.Column<string>(nullable: true),
                    Twitter = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    ChangeActionEnum = table.Column<int>(nullable: true),
                    VersionStatusEnum = table.Column<int>(nullable: true),
                    MinistryTimeLineId = table.Column<int>(nullable: true),
                    StatusId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MinistryTimeLineVersions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MinistryTimeLineVersions_AspNetUsers_ApprovedById",
                        column: x => x.ApprovedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MinistryTimeLineVersions_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MinistryTimeLineVersions_MinistryTimeLine_MinistryTimeLineId",
                        column: x => x.MinistryTimeLineId,
                        principalTable: "MinistryTimeLine",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MinistryTimeLineVersions_Statuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Statuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MinistryTimeLineVersions_ApprovedById",
                table: "MinistryTimeLineVersions",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_MinistryTimeLineVersions_CreatedById",
                table: "MinistryTimeLineVersions",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_MinistryTimeLineVersions_MinistryTimeLineId",
                table: "MinistryTimeLineVersions",
                column: "MinistryTimeLineId");

            migrationBuilder.CreateIndex(
                name: "IX_MinistryTimeLineVersions_StatusId",
                table: "MinistryTimeLineVersions",
                column: "StatusId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MinistryTimeLineVersions");
        }
    }
}
