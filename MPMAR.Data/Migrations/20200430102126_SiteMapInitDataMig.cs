using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.IO;

namespace MPMAR.Data.Migrations
{
    public partial class SiteMapInitDataMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sqlResourceName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Scripts/SiteMapInitData.sql");
            migrationBuilder.Sql(File.ReadAllText(sqlResourceName));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
