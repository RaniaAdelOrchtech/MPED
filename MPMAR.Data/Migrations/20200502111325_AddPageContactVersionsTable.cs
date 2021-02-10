using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MPMAR.Data.Migrations
{
    public partial class AddPageContactVersionsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ModificationDate",
                table: "PageContact",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedById",
                table: "PageContact",
                maxLength: 450,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PageContactVersions",
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
                    SeoTitleEN = table.Column<string>(nullable: true),
                    SeoTitleAR = table.Column<string>(nullable: true),
                    SeoDescriptionEN = table.Column<string>(nullable: true),
                    SeoDescriptionAR = table.Column<string>(nullable: true),
                    SeoOgTitleEN = table.Column<string>(nullable: true),
                    SeoOgTitleAR = table.Column<string>(nullable: true),
                    SeoTwitterCardEN = table.Column<string>(nullable: true),
                    SeoTwitterCardAR = table.Column<string>(nullable: true),
                    Order = table.Column<int>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ArParticipateTitle = table.Column<string>(nullable: true),
                    EnParticipateTitle = table.Column<string>(nullable: true),
                    EnPageName = table.Column<string>(nullable: true),
                    ArPageName = table.Column<string>(nullable: true),
                    ArMapTitle = table.Column<string>(nullable: true),
                    EnMapTitle = table.Column<string>(nullable: true),
                    ArAddress = table.Column<string>(nullable: true),
                    EnAddress = table.Column<string>(nullable: true),
                    FormParticipateActive = table.Column<bool>(nullable: false),
                    MapUrl = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    FaxNumber = table.Column<string>(nullable: true),
                    EmailParticipateEmail = table.Column<string>(nullable: true),
                    ChangeActionEnum = table.Column<int>(nullable: true),
                    VersionStatusEnum = table.Column<int>(nullable: true),
                    PageContactId = table.Column<int>(nullable: true),
                    PageRouteVersionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PageContactVersions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PageContactVersions_AspNetUsers_ApprovedById",
                        column: x => x.ApprovedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PageContactVersions_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PageContactVersions_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PageContactVersions_PageContact_PageContactId",
                        column: x => x.PageContactId,
                        principalTable: "PageContact",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PageContactVersions_PageRouteVersions_PageRouteVersionId",
                        column: x => x.PageRouteVersionId,
                        principalTable: "PageRouteVersions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PageContact_ModifiedById",
                table: "PageContact",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_PageContactVersions_ApprovedById",
                table: "PageContactVersions",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_PageContactVersions_CreatedById",
                table: "PageContactVersions",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_PageContactVersions_ModifiedById",
                table: "PageContactVersions",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_PageContactVersions_PageContactId",
                table: "PageContactVersions",
                column: "PageContactId");

            migrationBuilder.CreateIndex(
                name: "IX_PageContactVersions_PageRouteVersionId",
                table: "PageContactVersions",
                column: "PageRouteVersionId");

            migrationBuilder.AddForeignKey(
                name: "FK_PageContact_AspNetUsers_ModifiedById",
                table: "PageContact",
                column: "ModifiedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PageContact_AspNetUsers_ModifiedById",
                table: "PageContact");

            migrationBuilder.DropTable(
                name: "PageContactVersions");

            migrationBuilder.DropIndex(
                name: "IX_PageContact_ModifiedById",
                table: "PageContact");

            migrationBuilder.DropColumn(
                name: "ModificationDate",
                table: "PageContact");

            migrationBuilder.DropColumn(
                name: "ModifiedById",
                table: "PageContact");
        }
    }
}
