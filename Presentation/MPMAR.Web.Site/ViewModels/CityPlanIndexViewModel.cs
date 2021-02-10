using MPMAR.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPMAR.Web.Site.ViewModels
{
    public class CityPlanIndexViewModel
    {
        public CityPlanYear CityPlanYearLargeGovYear { get; set; }
        public CityPlan CityPlansObj { get; set; }
        public List<CityPlanYear> CityPlanYearsWithSameGovYear { get; set; }
        public List<IGrouping<string, CityPlanYear>> CityPlanYearsWithoutSameGovYear { get; set; }
    }
}
