using Microsoft.EntityFrameworkCore.Migrations;

namespace MPMAR.Data.Migrations
{
    public partial class addMonitoring : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Monitoring",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArMainTitle = table.Column<string>(maxLength: 500, nullable: false),
                    EnMainTitle = table.Column<string>(maxLength: 500, nullable: false),
                    BackGroundImage = table.Column<string>(nullable: true),
                    Image1 = table.Column<string>(nullable: false),
                    ArDescription1 = table.Column<string>(maxLength: 800, nullable: false),
                    EnDescription1 = table.Column<string>(maxLength: 800, nullable: false),
                    Link1 = table.Column<string>(nullable: false),
                    ArTitle2 = table.Column<string>(maxLength: 200, nullable: false),
                    EnTitle2 = table.Column<string>(maxLength: 200, nullable: false),
                    ArDescription2 = table.Column<string>(maxLength: 800, nullable: false),
                    EnDescription2 = table.Column<string>(maxLength: 800, nullable: false),
                    Link2 = table.Column<string>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Monitoring", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Monitoring");
        }
    }
}
