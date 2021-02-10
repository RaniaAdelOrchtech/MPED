using Microsoft.EntityFrameworkCore.Migrations;

namespace MPMAR.Data.Migrations
{
    public partial class AddPhotosAndPhotoSliderAndVideoTableToDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FooterMenuItem_FooterMenuTitles_FooterMenuTitleId",
                table: "FooterMenuItem");

            migrationBuilder.AlterColumn<int>(
                name: "FooterMenuTitleId",
                table: "FooterMenuItem",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "homePagePhotos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageUrl = table.Column<string>(nullable: false),
                    ArTitle = table.Column<string>(nullable: true),
                    ArDescription = table.Column<string>(nullable: true),
                    EnTitle = table.Column<string>(nullable: true),
                    EnDescription = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_homePagePhotos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "homePagePhotoSlider",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArDescription = table.Column<string>(nullable: true),
                    EnDescription = table.Column<string>(nullable: true),
                    ImageUrl = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_homePagePhotoSlider", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "homePageVideos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VideoUrl = table.Column<string>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_homePageVideos", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_FooterMenuItem_FooterMenuTitles_FooterMenuTitleId",
                table: "FooterMenuItem",
                column: "FooterMenuTitleId",
                principalTable: "FooterMenuTitles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FooterMenuItem_FooterMenuTitles_FooterMenuTitleId",
                table: "FooterMenuItem");

            migrationBuilder.DropTable(
                name: "homePagePhotos");

            migrationBuilder.DropTable(
                name: "homePagePhotoSlider");

            migrationBuilder.DropTable(
                name: "homePageVideos");

            migrationBuilder.AlterColumn<int>(
                name: "FooterMenuTitleId",
                table: "FooterMenuItem",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_FooterMenuItem_FooterMenuTitles_FooterMenuTitleId",
                table: "FooterMenuItem",
                column: "FooterMenuTitleId",
                principalTable: "FooterMenuTitles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
