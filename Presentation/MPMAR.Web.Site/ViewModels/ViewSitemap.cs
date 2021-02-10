using MPMAR.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MPMAR.Web.Site.ViewModels
{
    public class ViewSitemap
    {
        public SiteMap SiteMap { get; set; }
        public ICollection<NavItem> NavItemList { get; set; }
        public List<PageRoute> PageRoutes { get; set; }
        public List<PageRoute> PageRoutesList { get; set; }
    }
}