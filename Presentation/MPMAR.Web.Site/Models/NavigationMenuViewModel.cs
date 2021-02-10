using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPMAR.Web.Site.Models
{
    public class NavigationMenuViewModel
    {
        public int Id { get; set; }
        public string EnName { get; set; }
        public string ArName { get; set; }
        public int? Order { get; set; }
        public bool IsActive { get; set; }
        public int StatusId { get; set; }
        public string StatusName { get; set; }

        public int? ParentNavItemId { get; set; }
    }
}
