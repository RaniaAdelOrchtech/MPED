using MPMAR.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MPMAR.Data
{
    /// <summary>
    /// Class for CityPlanVersions table which form city plan version object used in Citizenplan screens
    /// </summary>
    public class CityPlanVersion : ActionInfo
    {
        [Key]
        public int Id { get; set; }
        public string EnPageDescription { get; set; }
        public string ArPageDescription { get; set; }
        public string EnAlexandria { get; set; }
        public string ArAlexandria { get; set; }
        public string EnAswan { get; set; }
        public string ArAswan { get; set; }
        public string EnAsyut { get; set; }
        public string ArAsyut { get; set; }
        public string EnBeheira { get; set; }
        public string ArBeheira { get; set; }
        public string EnBeniSuef { get; set; }
        public string ArBeniSuef { get; set; }
        public string EnCairo { get; set; }
        public string ArCairo { get; set; }
        public string EnDakahlia { get; set; }
        public string ArDakahlia { get; set; }
        public string EnDamietta { get; set; }
        public string ArDamietta { get; set; }
        public string EnFaiyum { get; set; }
        public string ArFaiyum { get; set; }
        public string EnGharbia { get; set; }
        public string ArGharbia { get; set; }
        public string EnGiza { get; set; }
        public string ArGiza { get; set; }
        public string EnIsmailia { get; set; }
        public string ArIsmailia { get; set; }
        public string EnKafrElSheikh { get; set; }
        public string ArKafrElSheikh { get; set; }
        public string EnLuxor { get; set; }
        public string ArLuxor { get; set; }
        public string EnMatruh { get; set; }
        public string ArMatruh { get; set; }
        public string EnMinya { get; set; }
        public string ArMinya { get; set; }
        public string EnMonufia { get; set; }
        public string ArMonufia { get; set; }
        public string EnNewValley { get; set; }
        public string ArNewValley { get; set; }
        public string EnNorthSinai { get; set; }
        public string ArNorthSinai { get; set; }
        public string EnPortSaid { get; set; }
        public string ArPortSaid { get; set; }
        public string EnQalyubia { get; set; }
        public string ArQalyubia { get; set; }
        public string EnQena { get; set; }
        public string ArQena { get; set; }
        public string EnRedSea { get; set; }
        public string ArRedSea { get; set; }
        public string EnSharqia { get; set; }
        public string ArSharqia { get; set; }
        public string EnSohag { get; set; }
        public string ArSohag { get; set; }
        public string EnSouthSinai { get; set; }
        public string ArSouthSinai { get; set; }
        public string EnSuez { get; set; }
        public string ArSuez { get; set; }

        public int? StatusId { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        public ChangeActionEnum? ChangeActionEnum { get; set; }
        public VersionStatusEnum? VersionStatusEnum { get; set; }
        public int? CityPlanId { get; set; }

        public CityPlan CityPlan { get; set; }
    }
}
