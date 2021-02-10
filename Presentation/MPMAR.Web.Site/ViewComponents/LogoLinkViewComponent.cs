using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MPMAR.Business.Interfaces;
using MPMAR.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MPMAR.Web.Site.ViewComponents
{
    public class LogoLinkViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _db;
        private readonly IConfiguration _configuration;

        public LogoLinkViewComponent(ApplicationDbContext db, IConfiguration configuration)
        {
            _db = db;
            _configuration = configuration;
        }

        public IViewComponentResult Invoke()
        {
            var data = _db.HomePageLogoLinks;
            //get image base url to add it to the relative url
            var imageBaseURL = _configuration.GetValue<string>("BackEndDomain");
            foreach (var item in data)
                item.ImageUrl = imageBaseURL + item.ImageUrl.Replace(" ", "%20");
            return View(data);
        }
    }
}
