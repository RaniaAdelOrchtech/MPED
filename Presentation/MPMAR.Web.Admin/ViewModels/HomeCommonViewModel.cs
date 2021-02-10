using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPMAR.Web.Admin.ViewModels
{
    public class HomeCommonViewModel
    {
        public int Id { get; set; }
        public string ArTitle { get; set; }
        public string EnTitle { get; set; }
        public string ArDescription { get; set; }
        public string EnDescription { get; set; }
        public HomeTypeEnum Type { get; set; }
        public int Order { get; set; }
    }
    public enum HomeTypeEnum
    {
        Title = 1,
        Detail = 2
    }
}
