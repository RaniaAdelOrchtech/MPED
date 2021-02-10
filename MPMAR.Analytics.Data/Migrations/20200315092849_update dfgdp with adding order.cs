using Microsoft.EntityFrameworkCore.Migrations;

namespace MPMAR.Analytics.Data.Migrations
{
    public partial class updatedfgdpwithaddingorder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "order",
                table: "DFGDP",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "order",
                table: "DFGDP");
        }
    }
}
