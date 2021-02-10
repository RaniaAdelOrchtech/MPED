using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPMAR.Web.Admin.ViewModels
{
    public class PageNewsTypeListViewModel
    {
        public int Id { get; set; }
        public string EnName { get; set; }
        public string ArName { get; set; }
        public bool IsDeleted { get; set; }
    }
}
