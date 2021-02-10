using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MPMAR.Data.Migrations
{
    public partial class AddEcnomicIndicatorAndEcnomicIndicatorsVersionTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EconomicIndicators",
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
                    MainDiscriptionAr = table.Column<string>(maxLength: 2000, nullable: false),
                    MainDiscriptionEn = table.Column<string>(maxLength: 2000, nullable: false),
                    ImageUrl1 = table.Column<string>(nullable: false),
                    ImageTitleAr1 = table.Column<string>(maxLength: 50, nullable: false),
                    ImageTitleEn1 = table.Column<string>(maxLength: 50, nullable: false),
                    ImageDiscriptionAr1 = table.Column<string>(maxLength: 300, nullable: false),
                    ImageDiscriptionEn1 = table.Column<string>(maxLength: 300, nullable: false),
                    Link1 = table.Column<string>(nullable: false),
                    ImageUrl2 = table.Column<string>(nullable: false),
                    ImageTitleAr2 = table.Column<string>(maxLength: 50, nullable: false),
                    ImageTitleEn2 = table.Column<string>(maxLength: 50, nullable: false),
                    ImageDiscriptionAr2 = table.Column<string>(maxLength: 300, nullable: false),
                    ImageDiscriptionEn2 = table.Column<string>(maxLength: 300, nullable: false),
                    Link2 = table.Column<string>(nullable: false),
                    ImageUrl3 = table.Column<string>(nullable: false),
                    ImageTitleAr3 = table.Column<string>(maxLength: 50, nullable: false),
                    ImageTitleEn3 = table.Column<string>(maxLength: 50, nullable: false),
                    ImageDiscriptionAr3 = table.Column<string>(maxLength: 300, nullable: false),
                    ImageDiscriptionEn3 = table.Column<string>(maxLength: 300, nullable: false),
                    Link3 = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EconomicIndicators", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EconomicIndicators_AspNetUsers_ApprovedById",
                        column: x => x.ApprovedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EconomicIndicators_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EconomicIndicators_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EconomicIndicatorsVersion",
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
                    MainDiscriptionAr = table.Column<string>(maxLength: 2000, nullable: false),
                    MainDiscriptionEn = table.Column<string>(maxLength: 2000, nullable: false),
                    ImageUrl1 = table.Column<string>(nullable: false),
                    ImageTitleAr1 = table.Column<string>(maxLength: 50, nullable: false),
                    ImageTitleEn1 = table.Column<string>(maxLength: 50, nullable: false),
                    ImageDiscriptionAr1 = table.Column<string>(maxLength: 300, nullable: false),
                    ImageDiscriptionEn1 = table.Column<string>(maxLength: 300, nullable: false),
                    Link1 = table.Column<string>(nullable: false),
                    ImageUrl2 = table.Column<string>(nullable: false),
                    ImageTitleAr2 = table.Column<string>(maxLength: 50, nullable: false),
                    ImageTitleEn2 = table.Column<string>(maxLength: 50, nullable: false),
                    ImageDiscriptionAr2 = table.Column<string>(maxLength: 300, nullable: false),
                    ImageDiscriptionEn2 = table.Column<string>(maxLength: 300, nullable: false),
                    Link2 = table.Column<string>(nullable: false),
                    ImageUrl3 = table.Column<string>(nullable: false),
                    ImageTitleAr3 = table.Column<string>(maxLength: 50, nullable: false),
                    ImageTitleEn3 = table.Column<string>(maxLength: 50, nullable: false),
                    ImageDiscriptionAr3 = table.Column<string>(maxLength: 300, nullable: false),
                    ImageDiscriptionEn3 = table.Column<string>(maxLength: 300, nullable: false),
                    Link3 = table.Column<string>(nullable: false),
                    ChangeActionEnum = table.Column<int>(nullable: true),
                    VersionStatusEnum = table.Column<int>(nullable: true),
                    EconomicIndicatorsId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EconomicIndicatorsVersion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EconomicIndicatorsVersion_AspNetUsers_ApprovedById",
                        column: x => x.ApprovedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EconomicIndicatorsVersion_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EconomicIndicatorsVersion_EconomicIndicators_EconomicIndicatorsId",
                        column: x => x.EconomicIndicatorsId,
                        principalTable: "EconomicIndicators",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EconomicIndicatorsVersion_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EconomicIndicators_ApprovedById",
                table: "EconomicIndicators",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_EconomicIndicators_CreatedById",
                table: "EconomicIndicators",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_EconomicIndicators_ModifiedById",
                table: "EconomicIndicators",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_EconomicIndicatorsVersion_ApprovedById",
                table: "EconomicIndicatorsVersion",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_EconomicIndicatorsVersion_CreatedById",
                table: "EconomicIndicatorsVersion",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_EconomicIndicatorsVersion_EconomicIndicatorsId",
                table: "EconomicIndicatorsVersion",
                column: "EconomicIndicatorsId");

            migrationBuilder.CreateIndex(
                name: "IX_EconomicIndicatorsVersion_ModifiedById",
                table: "EconomicIndicatorsVersion",
                column: "ModifiedById");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EconomicIndicatorsVersion");

            migrationBuilder.DropTable(
                name: "EconomicIndicators");
        }
    }
}
