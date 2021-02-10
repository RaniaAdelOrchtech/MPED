using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MPMAR.Data.Migrations
{
    public partial class AddStatusTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NavItemVersions_NavItems_NavItemId",
                table: "NavItemVersions");

            migrationBuilder.DropTable(
                name: "DynamicPageContentVersion");

            migrationBuilder.DropTable(
                name: "DynamicPageSectionCards");

            migrationBuilder.DropTable(
                name: "DynamicPageSectionVersions");

            migrationBuilder.DropTable(
                name: "DynamicPageSections");

            migrationBuilder.DropTable(
                name: "DynamicPageContent");

            migrationBuilder.DropTable(
                name: "DynamicPageSectionTypes");

            migrationBuilder.DropIndex(
                name: "IX_NavItemVersions_NavItemId",
                table: "NavItemVersions");

            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "PageRouteVersions");

            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "PageRoutes");

            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "NavItemVersions");

            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "NavItems");

            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "PageRouteVersions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "NavItemVersions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NavItemId",
                table: "NavItems",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PageSectionTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EnName = table.Column<string>(nullable: true),
                    ArName = table.Column<string>(nullable: true),
                    MediaType = table.Column<string>(nullable: true),
                    ThemeImage = table.Column<string>(nullable: true),
                    HasCards = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PageSectionTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Statuses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PageSections",
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
                    EnTitle = table.Column<string>(nullable: true),
                    ArTitle = table.Column<string>(nullable: true),
                    EnDescription = table.Column<string>(nullable: true),
                    ArDescription = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true),
                    EnImageAlt = table.Column<string>(nullable: true),
                    ArImageAlt = table.Column<string>(nullable: true),
                    Order = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    PageRouteId = table.Column<int>(nullable: false),
                    PageSectionTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PageSections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PageSections_AspNetUsers_ApprovedById",
                        column: x => x.ApprovedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PageSections_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PageSections_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PageSections_PageRoutes_PageRouteId",
                        column: x => x.PageRouteId,
                        principalTable: "PageRoutes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PageSections_PageSectionTypes_PageSectionTypeId",
                        column: x => x.PageSectionTypeId,
                        principalTable: "PageSectionTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PageSectionCards",
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
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ImageUrl = table.Column<string>(nullable: true),
                    ImageAlt = table.Column<string>(nullable: true),
                    FileUrl = table.Column<string>(nullable: true),
                    PageSectionId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PageSectionCards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PageSectionCards_AspNetUsers_ApprovedById",
                        column: x => x.ApprovedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PageSectionCards_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PageSectionCards_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PageSectionCards_PageSections_PageSectionId",
                        column: x => x.PageSectionId,
                        principalTable: "PageSections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PageSectionVersions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    CreatedById = table.Column<string>(maxLength: 450, nullable: true),
                    ApprovalDate = table.Column<DateTime>(nullable: true),
                    ApprovedById = table.Column<string>(maxLength: 450, nullable: true),
                    IsIgnored = table.Column<bool>(nullable: false),
                    EnTitle = table.Column<string>(nullable: true),
                    ArTitle = table.Column<string>(nullable: true),
                    EnDescription = table.Column<string>(nullable: true),
                    ArDescription = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true),
                    EnImageAlt = table.Column<string>(nullable: true),
                    ArImageAlt = table.Column<string>(nullable: true),
                    Order = table.Column<int>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    PageSectionId = table.Column<int>(nullable: false),
                    PageRouteVersionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PageSectionVersions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PageSectionVersions_AspNetUsers_ApprovedById",
                        column: x => x.ApprovedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PageSectionVersions_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PageSectionVersions_PageRouteVersions_PageRouteVersionId",
                        column: x => x.PageRouteVersionId,
                        principalTable: "PageRouteVersions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PageSectionVersions_PageSections_PageSectionId",
                        column: x => x.PageSectionId,
                        principalTable: "PageSections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PageRouteVersions_StatusId",
                table: "PageRouteVersions",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_NavItemVersions_StatusId",
                table: "NavItemVersions",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_NavItems_NavItemId",
                table: "NavItems",
                column: "NavItemId",
                unique: true,
                filter: "[NavItemId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PageSectionCards_ApprovedById",
                table: "PageSectionCards",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_PageSectionCards_CreatedById",
                table: "PageSectionCards",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_PageSectionCards_ModifiedById",
                table: "PageSectionCards",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_PageSectionCards_PageSectionId",
                table: "PageSectionCards",
                column: "PageSectionId");

            migrationBuilder.CreateIndex(
                name: "IX_PageSections_ApprovedById",
                table: "PageSections",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_PageSections_CreatedById",
                table: "PageSections",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_PageSections_ModifiedById",
                table: "PageSections",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_PageSections_PageRouteId",
                table: "PageSections",
                column: "PageRouteId");

            migrationBuilder.CreateIndex(
                name: "IX_PageSections_PageSectionTypeId",
                table: "PageSections",
                column: "PageSectionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PageSectionVersions_ApprovedById",
                table: "PageSectionVersions",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_PageSectionVersions_CreatedById",
                table: "PageSectionVersions",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_PageSectionVersions_PageRouteVersionId",
                table: "PageSectionVersions",
                column: "PageRouteVersionId");

            migrationBuilder.CreateIndex(
                name: "IX_PageSectionVersions_PageSectionId",
                table: "PageSectionVersions",
                column: "PageSectionId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_NavItems_NavItemVersions_NavItemId",
                table: "NavItems",
                column: "NavItemId",
                principalTable: "NavItemVersions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_NavItemVersions_Statuses_StatusId",
                table: "NavItemVersions",
                column: "StatusId",
                principalTable: "Statuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PageRouteVersions_Statuses_StatusId",
                table: "PageRouteVersions",
                column: "StatusId",
                principalTable: "Statuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NavItems_NavItemVersions_NavItemId",
                table: "NavItems");

            migrationBuilder.DropForeignKey(
                name: "FK_NavItemVersions_Statuses_StatusId",
                table: "NavItemVersions");

            migrationBuilder.DropForeignKey(
                name: "FK_PageRouteVersions_Statuses_StatusId",
                table: "PageRouteVersions");

            migrationBuilder.DropTable(
                name: "PageSectionCards");

            migrationBuilder.DropTable(
                name: "PageSectionVersions");

            migrationBuilder.DropTable(
                name: "Statuses");

            migrationBuilder.DropTable(
                name: "PageSections");

            migrationBuilder.DropTable(
                name: "PageSectionTypes");

            migrationBuilder.DropIndex(
                name: "IX_PageRouteVersions_StatusId",
                table: "PageRouteVersions");

            migrationBuilder.DropIndex(
                name: "IX_NavItemVersions_StatusId",
                table: "NavItemVersions");

            migrationBuilder.DropIndex(
                name: "IX_NavItems_NavItemId",
                table: "NavItems");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "PageRouteVersions");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "NavItemVersions");

            migrationBuilder.DropColumn(
                name: "NavItemId",
                table: "NavItems");

            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "PageRouteVersions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "PageRoutes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "NavItemVersions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "NavItems",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "DynamicPageContent",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApprovalDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ApprovedById = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    CreatedById = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModificationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedById = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DynamicPageContent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DynamicPageContent_AspNetUsers_ApprovedById",
                        column: x => x.ApprovedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DynamicPageContent_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DynamicPageContent_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DynamicPageSectionTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EnName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HasCards = table.Column<bool>(type: "bit", nullable: false),
                    MediaType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ThemeImage = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DynamicPageSectionTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DynamicPageContentVersion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApprovalDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ApprovedById = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    CreatedById = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DynamicPageContentId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsIgnored = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DynamicPageContentVersion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DynamicPageContentVersion_AspNetUsers_ApprovedById",
                        column: x => x.ApprovedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DynamicPageContentVersion_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DynamicPageContentVersion_DynamicPageContent_DynamicPageContentId",
                        column: x => x.DynamicPageContentId,
                        principalTable: "DynamicPageContent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DynamicPageSections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApprovalDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ApprovedById = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    ArDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ArImageAlt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ArTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedById = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DynamicPageContentId = table.Column<int>(type: "int", nullable: true),
                    DynamicPageSectionTypeId = table.Column<int>(type: "int", nullable: false),
                    EnDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EnImageAlt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EnTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModificationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedById = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    Order = table.Column<int>(type: "int", nullable: false),
                    PageRouteId = table.Column<int>(type: "int", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DynamicPageSections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DynamicPageSections_AspNetUsers_ApprovedById",
                        column: x => x.ApprovedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DynamicPageSections_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DynamicPageSections_DynamicPageContent_DynamicPageContentId",
                        column: x => x.DynamicPageContentId,
                        principalTable: "DynamicPageContent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DynamicPageSections_DynamicPageSectionTypes_DynamicPageSectionTypeId",
                        column: x => x.DynamicPageSectionTypeId,
                        principalTable: "DynamicPageSectionTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DynamicPageSections_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DynamicPageSections_PageRoutes_PageRouteId",
                        column: x => x.PageRouteId,
                        principalTable: "PageRoutes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DynamicPageSectionCards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApprovalDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ApprovedById = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    CreatedById = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DynamicPageSectionId = table.Column<int>(type: "int", nullable: true),
                    FileUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageAlt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModificationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedById = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DynamicPageSectionCards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DynamicPageSectionCards_AspNetUsers_ApprovedById",
                        column: x => x.ApprovedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DynamicPageSectionCards_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DynamicPageSectionCards_DynamicPageSections_DynamicPageSectionId",
                        column: x => x.DynamicPageSectionId,
                        principalTable: "DynamicPageSections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DynamicPageSectionCards_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DynamicPageSectionVersions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApprovalDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ApprovedById = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    ArDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ArImageAlt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ArTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedById = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DynamicPageSectionId = table.Column<int>(type: "int", nullable: false),
                    EnDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EnImageAlt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EnTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsIgnored = table.Column<bool>(type: "bit", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: true),
                    PageRouteVersionId = table.Column<int>(type: "int", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DynamicPageSectionVersions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DynamicPageSectionVersions_AspNetUsers_ApprovedById",
                        column: x => x.ApprovedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DynamicPageSectionVersions_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DynamicPageSectionVersions_DynamicPageSections_DynamicPageSectionId",
                        column: x => x.DynamicPageSectionId,
                        principalTable: "DynamicPageSections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DynamicPageSectionVersions_PageRouteVersions_PageRouteVersionId",
                        column: x => x.PageRouteVersionId,
                        principalTable: "PageRouteVersions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NavItemVersions_NavItemId",
                table: "NavItemVersions",
                column: "NavItemId");

            migrationBuilder.CreateIndex(
                name: "IX_DynamicPageContent_ApprovedById",
                table: "DynamicPageContent",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_DynamicPageContent_CreatedById",
                table: "DynamicPageContent",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_DynamicPageContent_ModifiedById",
                table: "DynamicPageContent",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_DynamicPageContentVersion_ApprovedById",
                table: "DynamicPageContentVersion",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_DynamicPageContentVersion_CreatedById",
                table: "DynamicPageContentVersion",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_DynamicPageContentVersion_DynamicPageContentId",
                table: "DynamicPageContentVersion",
                column: "DynamicPageContentId");

            migrationBuilder.CreateIndex(
                name: "IX_DynamicPageSectionCards_ApprovedById",
                table: "DynamicPageSectionCards",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_DynamicPageSectionCards_CreatedById",
                table: "DynamicPageSectionCards",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_DynamicPageSectionCards_DynamicPageSectionId",
                table: "DynamicPageSectionCards",
                column: "DynamicPageSectionId");

            migrationBuilder.CreateIndex(
                name: "IX_DynamicPageSectionCards_ModifiedById",
                table: "DynamicPageSectionCards",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_DynamicPageSections_ApprovedById",
                table: "DynamicPageSections",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_DynamicPageSections_CreatedById",
                table: "DynamicPageSections",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_DynamicPageSections_DynamicPageContentId",
                table: "DynamicPageSections",
                column: "DynamicPageContentId");

            migrationBuilder.CreateIndex(
                name: "IX_DynamicPageSections_DynamicPageSectionTypeId",
                table: "DynamicPageSections",
                column: "DynamicPageSectionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_DynamicPageSections_ModifiedById",
                table: "DynamicPageSections",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_DynamicPageSections_PageRouteId",
                table: "DynamicPageSections",
                column: "PageRouteId");

            migrationBuilder.CreateIndex(
                name: "IX_DynamicPageSectionVersions_ApprovedById",
                table: "DynamicPageSectionVersions",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_DynamicPageSectionVersions_CreatedById",
                table: "DynamicPageSectionVersions",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_DynamicPageSectionVersions_DynamicPageSectionId",
                table: "DynamicPageSectionVersions",
                column: "DynamicPageSectionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DynamicPageSectionVersions_PageRouteVersionId",
                table: "DynamicPageSectionVersions",
                column: "PageRouteVersionId");

            migrationBuilder.AddForeignKey(
                name: "FK_NavItemVersions_NavItems_NavItemId",
                table: "NavItemVersions",
                column: "NavItemId",
                principalTable: "NavItems",
                principalColumn: "Id");
        }
    }
}
