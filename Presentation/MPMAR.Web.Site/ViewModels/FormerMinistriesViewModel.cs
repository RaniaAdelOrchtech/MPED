using MPMAR.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPMAR.Web.Site.ViewModels
{
    public class FormerMinistriesViewModel
    {
        public FormerMinistriesViewModel()
        {
            MinistryTimeLine = new List<MinistryViewModel>();
        }
        public string Title1 { get; set; }
        public string Title2 { get; set; }
        public string Description { get; set; }

        public List<MinistryViewModel> MinistryTimeLine { get; set; }
    }

    public class MinistryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ProfileImageUrl { get; set; }
        public string Period { get; set; }
        public int Order { get; set; }
        public string Facebook { get; set; }
        public string Twitter { get; set; }
        public string Email { get; set; }
    }
}
