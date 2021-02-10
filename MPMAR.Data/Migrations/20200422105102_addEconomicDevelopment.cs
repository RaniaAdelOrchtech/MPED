using Microsoft.EntityFrameworkCore.Migrations;

namespace MPMAR.Data.Migrations
{
    public partial class addEconomicDevelopment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Link1",
                table: "Publications",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Link2",
                table: "Publications",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Link3",
                table: "Publications",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "EconomicDevelopments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArTitle = table.Column<string>(nullable: false),
                    ArDescription = table.Column<string>(nullable: false),
                    EnTitle = table.Column<string>(nullable: false),
                    EnDescription = table.Column<string>(nullable: false),
                    Url = table.Column<string>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EconomicDevelopments", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EconomicDevelopments");

            migrationBuilder.DropColumn(
                name: "Link1",
                table: "Publications");

            migrationBuilder.DropColumn(
                name: "Link2",
                table: "Publications");

            migrationBuilder.DropColumn(
                name: "Link3",
                table: "Publications");
        }
    }
}
