using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MPMAR.Data.Migrations
{
    public partial class FormerMinistriesMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PeriodAr",
                table: "MinistryTimeLine",
                nullable: true,
                maxLength:30);

            migrationBuilder.AddColumn<string>(
                name: "PeriodEn",
                table: "MinistryTimeLine",
                nullable: true,
                maxLength: 30);


            migrationBuilder.CreateTable(
                name: "FormerMinistriesPageInfos",
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
                    Title1Ar = table.Column<string>(maxLength: 100, nullable: false),
                    Title1En = table.Column<string>(maxLength: 100, nullable: false),
                    DescriptionAr = table.Column<string>(maxLength: 1500, nullable: false),
                    DescriptionEn = table.Column<string>(maxLength: 1500, nullable: false),
                    Title2Ar = table.Column<string>(maxLength: 100, nullable: false),
                    Title2En = table.Column<string>(maxLength: 100, nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormerMinistriesPageInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FormerMinistriesPageInfos_AspNetUsers_ApprovedById",
                        column: x => x.ApprovedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FormerMinistriesPageInfos_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FormerMinistriesPageInfos_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });


            migrationBuilder.CreateIndex(
                name: "IX_FormerMinistriesPageInfos_ApprovedById",
                table: "FormerMinistriesPageInfos",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_FormerMinistriesPageInfos_CreatedById",
                table: "FormerMinistriesPageInfos",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_FormerMinistriesPageInfos_ModifiedById",
                table: "FormerMinistriesPageInfos",
                column: "ModifiedById");

           
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
          

            migrationBuilder.DropTable(
                name: "FormerMinistriesPageInfos");
        }
    }
}
