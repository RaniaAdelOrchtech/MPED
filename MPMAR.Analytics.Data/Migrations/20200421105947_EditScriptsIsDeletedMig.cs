using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.IO;
using System.Linq;

namespace MPMAR.Analytics.Data.Migrations
{
    public partial class EditScriptsIsDeletedMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sqlResourceName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Scripts/GrossDomesticActivityFun2Edit1.sql");
            var sqlResourceName1 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Scripts/GrossDomesticComponentFun2Edit1.sql");
            migrationBuilder.Sql(File.ReadAllText(sqlResourceName));
            migrationBuilder.Sql(File.ReadAllText(sqlResourceName1));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
