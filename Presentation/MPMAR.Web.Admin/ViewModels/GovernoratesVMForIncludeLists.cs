using MPMAR.Analytics.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MPMAR.Web.Admin.ViewModels
{
    public class GovernoratesVMForIncludeLists
    {
        public Governorate governorate { get; set; }

        public IEnumerable<SelectListItem> DFYears { get; set; }
        public IEnumerable<SelectListItem> DFGovernorates { get; set; }
        public IEnumerable<SelectListItem> DFRegions { get; set; }

        public IEnumerable<SelectListItem> NormalTotalList { get; } = new List<SelectListItem>
    {
        new SelectListItem { Value = "normal", Text = "Normal" },
        new SelectListItem { Value = "total", Text = "Total" }
    };
    }
}
