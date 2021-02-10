using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MPMAR.Business.Interfaces;
using MPMAR.Business.Services;
using MPMAR.Data;
using MPMAR.Data.HomePageModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPMAR.Web.Site.ViewComponents
{
    public class EconomicIndicatorsViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _db;
        private readonly IConfiguration _configuration;

        public EconomicIndicatorsViewComponent(ApplicationDbContext db, IConfiguration configuration)
        {
            _db = db;
            _configuration = configuration;
        }

        public IViewComponentResult Invoke()
        {
            var items = _db.EconomicIndicators;
            //get image base url to add it to the relative url
            var imageBaseURL = _configuration.GetValue<string>("BackEndDomain");
            foreach (var model in items)
            {
                model.ImageUrl1 = imageBaseURL + model.ImageUrl1.Replace(" ", "%20");
                model.ImageUrl2 = imageBaseURL + model.ImageUrl2.Replace(" ", "%20");
                model.ImageUrl3 = imageBaseURL + model.ImageUrl3.Replace(" ", "%20");
            }
            return View(items.FirstOrDefault());
        }
    }
}
