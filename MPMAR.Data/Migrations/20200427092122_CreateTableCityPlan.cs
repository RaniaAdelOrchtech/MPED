using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace MPMAR.Data.Migrations
{
    public partial class CreateTableCityPlan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

         
            migrationBuilder.CreateTable(
                name: "CityPlan",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false).Annotation("SqlServer:Identity", "1, 1"),
                    
                    CreationDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(maxLength: 450, nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    ApprovalDate = table.Column<DateTime>(nullable: true),
                    ApprovedById = table.Column<string>(maxLength: 450, nullable: true),
                    ApprovedBy = table.Column<string>(maxLength: 450, nullable: true),

                    ModificationDate = table.Column<DateTime>(nullable: true),
                    ModifiedById = table.Column<string>(maxLength: 450, nullable: true),
                    ModifiedBy = table.Column<string>(maxLength: 450, nullable: true),

                    IsActive = table.Column<bool>(nullable: true),
                    StatusId = table.Column<int>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),

                    EnPageDescription = table.Column<string>(nullable: true),
                    ArPageDescription = table.Column<string>(nullable: true),
                    EnAlexandria = table.Column<string>(nullable: true),
                    ArAlexandria = table.Column<string>(nullable: true),
                    EnAswan =table.Column<string>(nullable: true),
                    ArAswan = table.Column<string>(nullable: true),
                    EnAsyut = table.Column<string>(nullable: true),
                    ArAsyut = table.Column<string>(nullable: true),
                    EnBeheira = table.Column<string>(nullable: true),
                    ArBeheira = table.Column<string>(nullable: true),
                    EnBeniSuef = table.Column<string>(nullable: true),
                    ArBeniSuef = table.Column<string>(nullable: true),
                    EnCairo = table.Column<string>(nullable: true),
                    ArCairo = table.Column<string>(nullable: true),
                    EnDakahlia = table.Column<string>(nullable: true),
                    ArDakahlia = table.Column<string>(nullable: true),
                    EnDamietta = table.Column<string>(nullable: true),
                    ArDamietta = table.Column<string>(nullable: true),
                    EnFaiyum = table.Column<string>(nullable: true),
                    ArFaiyum = table.Column<string>(nullable: true),
                    EnGharbia= table.Column<string>(nullable: true),
                    ArGharbia = table.Column<string>(nullable: true),
                    EnGiza= table.Column<string>(nullable: true),
                    ArGiza = table.Column<string>(nullable: true),
                    EnIsmailia= table.Column<string>(nullable: true),
                    ArIsmailia = table.Column<string>(nullable: true),
                    EnKafrElSheikh= table.Column<string>(nullable: true),
                    ArKafrElSheikh = table.Column<string>(nullable: true),
                    EnLuxor= table.Column<string>(nullable: true),
                    ArLuxor = table.Column<string>(nullable: true),
                    EnMatruh= table.Column<string>(nullable: true),
                    ArMatruh = table.Column<string>(nullable: true),
                    EnMinya= table.Column<string>(nullable: true),
                    ArMinya = table.Column<string>(nullable: true),
                    EnMonufia= table.Column<string>(nullable: true),
                    ArMonufia = table.Column<string>(nullable: true),
                    EnNewValley= table.Column<string>(nullable: true),
                    ArNewValley = table.Column<string>(nullable: true),
                    EnNorthSinai= table.Column<string>(nullable: true),
                    ArNorthSinai = table.Column<string>(nullable: true),
                    EnPortSaid = table.Column<string>(nullable: true),
                    ArPortSaid = table.Column<string>(nullable: true),
                    EnQalyubia = table.Column<string>(nullable: true),
                    ArQalyubia = table.Column<string>(nullable: true),
                    EnQena = table.Column<string>(nullable: true),
                    ArQena = table.Column<string>(nullable: true),
                    EnRedSea = table.Column<string>(nullable: true),
                    ArRedSea = table.Column<string>(nullable: true),
                    EnSharqia= table.Column<string>(nullable: true),
                    ArSharqia = table.Column<string>(nullable: true),
                    EnSohag = table.Column<string>(nullable: true),
                    ArSohag = table.Column<string>(nullable: true),
                    EnSouthSinai = table.Column<string>(nullable: true),
                    ArSouthSinai = table.Column<string>(nullable: true),
                    EnSuez = table.Column<string>(nullable: true),
                    ArSuez = table.Column<string>(nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CityPlan", x => x.Id);
                });


            migrationBuilder.CreateTable(
             name: "CityPlanYear",
             columns: table => new
             {
                 Id = table.Column<int>(nullable: false).Annotation("SqlServer:Identity", "1, 1"),
                 CityPlanId = table.Column<int>(nullable: false),
                 GovName = table.Column<string>(nullable: false),
                 GovYear = table.Column<string>(nullable: true),
                 IsMapActive = table.Column<bool>(nullable: true),
                 EnFileUrl = table.Column<string>(nullable: true),
                 ArFileUrl = table.Column<string>(nullable: true),

                 CreationDate = table.Column<DateTime>(nullable: true),
                 CreatedById = table.Column<string>(maxLength: 450, nullable: true),
                 CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                 ApprovalDate = table.Column<DateTime>(nullable: true),
                 ApprovedById = table.Column<string>(maxLength: 450, nullable: true),
                 ApprovedBy = table.Column<string>(maxLength: 450, nullable: true),

                 ModificationDate = table.Column<DateTime>(nullable: true),
                 ModifiedById = table.Column<string>(maxLength: 450, nullable: true),
                 ModifiedBy = table.Column<string>(maxLength: 450, nullable: true),

                 IsActive = table.Column<bool>(nullable: true),
                 StatusId = table.Column<int>(nullable: true),
                 IsDeleted = table.Column<bool>(nullable: true),

             },
             constraints: table =>
             {
                 table.PrimaryKey("PK_CityPlanYear", x => x.Id);
                 table.ForeignKey(
                     name: "FK_CityPlan_CityPlanYear_Id",
                     column: x => x.CityPlanId,
                     principalTable: "CityPlan",
                     principalColumn: "Id",
                     onDelete: ReferentialAction.Cascade);
             });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
               name: "FK_CityPlan_CityPlanYear_Id",
               table: "CityPlanYear");
            migrationBuilder.DropTable(
              name: "CityPlanYear");
            migrationBuilder.DropTable(
               name: "CityPlan");
        }
    }
}
