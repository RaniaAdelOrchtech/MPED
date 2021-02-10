using Microsoft.EntityFrameworkCore.Migrations;

namespace MPMAR.Data.Migrations
{
    public partial class addEnImageToHPCitizenPlan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EnImage",
                table: "CitizenPlanVersions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EnImage",
                table: "CitizenPlan",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EnImage",
                table: "CitizenPlanVersions");

            migrationBuilder.DropColumn(
                name: "EnImage",
                table: "CitizenPlan");
        }
    }
}
