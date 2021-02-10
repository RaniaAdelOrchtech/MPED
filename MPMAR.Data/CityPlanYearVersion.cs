using MPMAR.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MPMAR.Data
{
    /// <summary>
    /// Class for CityPlanYearVersion table which form city plan year version object used in Citizenplan -> cityplanyears screens
    /// </summary>
    public class CityPlanYearVersion : ActionInfo
    {
        [Key]
        public int Id { get; set; }
        public int CityPlanVersionId { get; set; }
        public string GovName { get; set; }
        public string GovYear { get; set; }
        public bool IsMapActive { get; set; }
        public string EnFileUrl { get; set; }
        public string ArFileUrl { get; set; }

        public int? StatusId { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int? DFGovId { get; set; }

        public CityPlanVersion CityPlanVersion { get; set; }

        public ChangeActionEnum? ChangeActionEnum { get; set; }
        public VersionStatusEnum? VersionStatusEnum { get; set; }
        public int? CityPlanYearId { get; set; }

        public CityPlanYear CityPlanYear { get; set; }
        public DFGov DFGov { get; set; }

    }
}
