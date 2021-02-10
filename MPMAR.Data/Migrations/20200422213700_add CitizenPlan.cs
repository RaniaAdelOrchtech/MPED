using Microsoft.EntityFrameworkCore.Migrations;

namespace MPMAR.Data.Migrations
{
    public partial class addCitizenPlan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CitizenPlan",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArMainTitle = table.Column<string>(maxLength: 100, nullable: false),
                    EnMainTitle = table.Column<string>(maxLength: 100, nullable: false),
                    ArTitle = table.Column<string>(maxLength: 100, nullable: false),
                    EnTitle = table.Column<string>(maxLength: 100, nullable: false),
                    ArDescription = table.Column<string>(maxLength: 1000, nullable: false),
                    EnDescription = table.Column<string>(maxLength: 1000, nullable: false),
                    Link = table.Column<string>(nullable: true),
                    Image = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CitizenPlan", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CitizenPlan");
        }
    }
}
