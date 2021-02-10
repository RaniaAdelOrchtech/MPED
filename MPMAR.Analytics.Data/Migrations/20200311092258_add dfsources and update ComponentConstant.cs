using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MPMAR.Analytics.Data.Migrations
{
    public partial class adddfsourcesandupdateComponentConstant : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComponentConstants_DFGovernorates_DFGovernorateId",
                table: "ComponentConstants");

            migrationBuilder.RenameColumn(
                name: "DFGovernorateId",
                table: "ComponentConstants",
                newName: "DFSourceId");

            migrationBuilder.RenameIndex(
                name: "IX_ComponentConstants_DFGovernorateId",
                table: "ComponentConstants",
                newName: "IX_ComponentConstants_DFSourceId");

            migrationBuilder.CreateTable(
                name: "DFSources",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    NameAr = table.Column<string>(nullable: true),
                    NameEn = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DFSources", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_ComponentConstants_DFSources_DFSourceId",
                table: "ComponentConstants",
                column: "DFSourceId",
                principalTable: "DFSources",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComponentConstants_DFSources_DFSourceId",
                table: "ComponentConstants");

            migrationBuilder.DropTable(
                name: "DFSources");

            migrationBuilder.RenameColumn(
                name: "DFSourceId",
                table: "ComponentConstants",
                newName: "DFGovernorateId");

            migrationBuilder.RenameIndex(
                name: "IX_ComponentConstants_DFSourceId",
                table: "ComponentConstants",
                newName: "IX_ComponentConstants_DFGovernorateId");

            migrationBuilder.AddForeignKey(
                name: "FK_ComponentConstants_DFGovernorates_DFGovernorateId",
                table: "ComponentConstants",
                column: "DFGovernorateId",
                principalTable: "DFGovernorates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
