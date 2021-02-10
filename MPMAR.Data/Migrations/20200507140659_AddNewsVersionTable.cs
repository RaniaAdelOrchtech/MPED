using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MPMAR.Data.Migrations
{
    public partial class AddNewsVersionTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PageNews_PageRouteVersions_PageRouteVersionId",
                table: "PageNews");

            //migrationBuilder.DropIndex(
            //    name: "IX_PageNews_PageRouteVersionId",
            //    table: "PageNews");

            migrationBuilder.DropColumn(
                name: "PageRouteVersionId",
                table: "PageNews");

            //migrationBuilder.AddColumn<DateTime>(
            //    name: "ApprovalDate",
            //    table: "PageNews",
            //    nullable: true);

            //migrationBuilder.AddColumn<string>(
            //    name: "ApprovedById",
            //    table: "PageNews",
            //    maxLength: 450,
            //    nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModificationDate",
                table: "PageNews",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedById",
                table: "PageNews",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PageRouteId",
                table: "PageNews",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PageNewsVersions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    CreatedById = table.Column<string>(maxLength: 450, nullable: true),
                    ApprovalDate = table.Column<DateTime>(nullable: true),
                    ApprovedById = table.Column<string>(maxLength: 450, nullable: true),
                    EnTitle = table.Column<string>(nullable: true),
                    ArTitle = table.Column<string>(nullable: true),
                    EnShortDescription = table.Column<string>(nullable: true),
                    ArShortDescription = table.Column<string>(nullable: true),
                    EnDescription = table.Column<string>(nullable: true),
                    ArDescription = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    PageRouteVersionId = table.Column<int>(nullable: false),
                    PageNewsId = table.Column<int>(nullable: true),
                    Date = table.Column<DateTime>(nullable: true),
                    ChangeActionEnum = table.Column<int>(nullable: true),
                    VersionStatusEnum = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PageNewsVersions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PageNewsVersions_AspNetUsers_ApprovedById",
                        column: x => x.ApprovedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PageNewsVersions_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PageNewsVersions_PageNews_PageNewsId",
                        column: x => x.PageNewsId,
                        principalTable: "PageNews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PageNewsVersions_PageRouteVersions_PageRouteVersionId",
                        column: x => x.PageRouteVersionId,
                        principalTable: "PageRouteVersions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            //migrationBuilder.CreateIndex(
            //    name: "IX_PageNews_ApprovedById",
            //    table: "PageNews",
            //    column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_PageNews_CreatedById",
                table: "PageNews",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_PageNews_ModifiedById",
                table: "PageNews",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_PageNews_PageRouteId",
                table: "PageNews",
                column: "PageRouteId");

            migrationBuilder.CreateIndex(
                name: "IX_PageNewsVersions_ApprovedById",
                table: "PageNewsVersions",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_PageNewsVersions_CreatedById",
                table: "PageNewsVersions",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_PageNewsVersions_PageNewsId",
                table: "PageNewsVersions",
                column: "PageNewsId");

            migrationBuilder.CreateIndex(
                name: "IX_PageNewsVersions_PageRouteVersionId",
                table: "PageNewsVersions",
                column: "PageRouteVersionId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_PageNews_AspNetUsers_ApprovedById",
            //    table: "PageNews",
            //    column: "ApprovedById",
            //    principalTable: "AspNetUsers",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PageNews_AspNetUsers_CreatedById",
                table: "PageNews",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PageNews_AspNetUsers_ModifiedById",
                table: "PageNews",
                column: "ModifiedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PageNews_PageRoutes_PageRouteId",
                table: "PageNews",
                column: "PageRouteId",
                principalTable: "PageRoutes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PageNews_AspNetUsers_ApprovedById",
                table: "PageNews");

            migrationBuilder.DropForeignKey(
                name: "FK_PageNews_AspNetUsers_CreatedById",
                table: "PageNews");

            migrationBuilder.DropForeignKey(
                name: "FK_PageNews_AspNetUsers_ModifiedById",
                table: "PageNews");

            migrationBuilder.DropForeignKey(
                name: "FK_PageNews_PageRoutes_PageRouteId",
                table: "PageNews");

            migrationBuilder.DropTable(
                name: "PageNewsVersions");

            migrationBuilder.DropIndex(
                name: "IX_PageNews_ApprovedById",
                table: "PageNews");

            migrationBuilder.DropIndex(
                name: "IX_PageNews_CreatedById",
                table: "PageNews");

            migrationBuilder.DropIndex(
                name: "IX_PageNews_ModifiedById",
                table: "PageNews");

            migrationBuilder.DropIndex(
                name: "IX_PageNews_PageRouteId",
                table: "PageNews");

            migrationBuilder.DropColumn(
                name: "ApprovalDate",
                table: "PageNews");

            migrationBuilder.DropColumn(
                name: "ApprovedById",
                table: "PageNews");

            migrationBuilder.DropColumn(
                name: "ModificationDate",
                table: "PageNews");

            migrationBuilder.DropColumn(
                name: "ModifiedById",
                table: "PageNews");

            migrationBuilder.DropColumn(
                name: "PageRouteId",
                table: "PageNews");

            migrationBuilder.AddColumn<int>(
                name: "PageRouteVersionId",
                table: "PageNews",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PageNews_PageRouteVersionId",
                table: "PageNews",
                column: "PageRouteVersionId");

            migrationBuilder.AddForeignKey(
                name: "FK_PageNews_PageRouteVersions_PageRouteVersionId",
                table: "PageNews",
                column: "PageRouteVersionId",
                principalTable: "PageRouteVersions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
