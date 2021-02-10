using Microsoft.EntityFrameworkCore.Migrations;

namespace MPMAR.Data.Migrations
{
    public partial class addPublicationtable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AddColumn<string>(
            //    name: "ImageUrl",
            //    table: "PageMinistry",
            //    nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EnTitle",
                table: "MinistryVissions",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "EnDescription",
                table: "MinistryVissions",
                maxLength: 1000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "ArTitle",
                table: "MinistryVissions",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "ArDescription",
                table: "MinistryVissions",
                maxLength: 1000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "Publications",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArMainTitle = table.Column<string>(maxLength: 100, nullable: false),
                    EnMainTitle = table.Column<string>(maxLength: 100, nullable: false),
                    ArTitle1 = table.Column<string>(maxLength: 100, nullable: true),
                    EnTitle1 = table.Column<string>(maxLength: 100, nullable: true),
                    ArTitle2 = table.Column<string>(maxLength: 100, nullable: true),
                    EnTitle2 = table.Column<string>(maxLength: 100, nullable: true),
                    ArTitle3 = table.Column<string>(maxLength: 100, nullable: true),
                    EnTitle3 = table.Column<string>(maxLength: 100, nullable: true),
                    ArDescription1 = table.Column<string>(maxLength: 500, nullable: true),
                    EnDescription1 = table.Column<string>(maxLength: 500, nullable: true),
                    ArDescription2 = table.Column<string>(maxLength: 500, nullable: true),
                    EnDescription2 = table.Column<string>(maxLength: 500, nullable: true),
                    ArDescription3 = table.Column<string>(maxLength: 500, nullable: true),
                    EnDescription3 = table.Column<string>(maxLength: 500, nullable: true),
                    Image1 = table.Column<string>(nullable: false),
                    Image2 = table.Column<string>(nullable: true),
                    Image3 = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publications", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Publications");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "PageMinistry");

            migrationBuilder.AlterColumn<string>(
                name: "EnTitle",
                table: "MinistryVissions",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "EnDescription",
                table: "MinistryVissions",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 1000);

            migrationBuilder.AlterColumn<string>(
                name: "ArTitle",
                table: "MinistryVissions",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "ArDescription",
                table: "MinistryVissions",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 1000);
        }
    }
}
