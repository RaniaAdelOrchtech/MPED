using Microsoft.AspNetCore.Http;
using MPMAR.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPMAR.Web.Admin.ViewModels
{
    public class HomePageBasicInfoViewModel : PageSeo
    {
        public int Id { get; set; }
        public string LogoUrl { get; set; }
        public IFormFile LogoFile { get; set; }
        public string FavIconUrl { get; set; }
        public IFormFile FavIconFile { get; set; }
    }
}
