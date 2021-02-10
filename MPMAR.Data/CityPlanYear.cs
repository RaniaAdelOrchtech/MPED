using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MPMAR.Data
{
    /// <summary>
    /// Class for CityPlanYear table which form city plan year object used in Citizenplan -> cityplanyears screens
    /// </summary>
    public class CityPlanYear : ActionInfo
    {
        [Key]
        public int Id { get; set; }
        public int CityPlanId { get; set; }
        public string GovName { get; set; }
        public string GovYear { get; set; }

        /// <summary>
        /// is this object will put in map or not
        /// </summary>
        public bool IsMapActive { get; set; }
        public string EnFileUrl { get; set; }
        public string ArFileUrl { get; set; }
        public int? DFGovId { get; set; }
        public int? StatusId { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public CityPlan CityPlan { get; set; }
        public DFGov DFGov { get; set; }
    }
}
