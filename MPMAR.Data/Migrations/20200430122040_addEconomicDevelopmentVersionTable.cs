using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MPMAR.Data.Migrations
{
    public partial class addEconomicDevelopmentVersionTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EconomicDevelopmentVersions",
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
                    ArTitle1 = table.Column<string>(maxLength: 200, nullable: false),
                    EnTitle1 = table.Column<string>(maxLength: 200, nullable: false),
                    ArDescription1 = table.Column<string>(maxLength: 800, nullable: false),
                    EnDescription1 = table.Column<string>(maxLength: 800, nullable: false),
                    Url1 = table.Column<string>(nullable: false),
                    ArTitle2 = table.Column<string>(maxLength: 200, nullable: false),
                    EnTitle2 = table.Column<string>(maxLength: 200, nullable: false),
                    ArDescription2 = table.Column<string>(maxLength: 800, nullable: false),
                    EnDescription2 = table.Column<string>(maxLength: 800, nullable: false),
                    Url2 = table.Column<string>(nullable: false),
                    ArTitle3 = table.Column<string>(maxLength: 200, nullable: false),
                    EnTitle3 = table.Column<string>(maxLength: 200, nullable: false),
                    ArDescription3 = table.Column<string>(maxLength: 800, nullable: false),
                    EnDescription3 = table.Column<string>(maxLength: 800, nullable: false),
                    Url3 = table.Column<string>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ChangeActionEnum = table.Column<int>(nullable: true),
                    VersionStatusEnum = table.Column<int>(nullable: true),
                    EconomicDevelopmentId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EconomicDevelopmentVersions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EconomicDevelopmentVersions_AspNetUsers_ApprovedById",
                        column: x => x.ApprovedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EconomicDevelopmentVersions_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EconomicDevelopmentVersions_EconomicDevelopments_EconomicDevelopmentId",
                        column: x => x.EconomicDevelopmentId,
                        principalTable: "EconomicDevelopments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EconomicDevelopmentVersions_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EconomicDevelopmentVersions_ApprovedById",
                table: "EconomicDevelopmentVersions",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_EconomicDevelopmentVersions_CreatedById",
                table: "EconomicDevelopmentVersions",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_EconomicDevelopmentVersions_EconomicDevelopmentId",
                table: "EconomicDevelopmentVersions",
                column: "EconomicDevelopmentId");

            migrationBuilder.CreateIndex(
                name: "IX_EconomicDevelopmentVersions_ModifiedById",
                table: "EconomicDevelopmentVersions",
                column: "ModifiedById");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EconomicDevelopmentVersions");
        }
    }
}
