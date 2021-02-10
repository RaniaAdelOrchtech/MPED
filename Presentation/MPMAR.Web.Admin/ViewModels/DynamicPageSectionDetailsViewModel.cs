using MPMAR.Business.ViewModels;
using MPMAR.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPMAR.Web.Admin.ViewModels
{
    public class DynamicPageSectionDetailsViewModel
    {
        public SectionViewModel SectionViewModel { get; set; }
        public PageRouteVersion PageRouteVersion { get; set; }
        public int? ApprovalId { get;  set; }
    }
}
