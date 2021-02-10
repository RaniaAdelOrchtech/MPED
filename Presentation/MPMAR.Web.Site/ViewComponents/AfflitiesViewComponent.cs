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
    public class AfflitiesViewComponent : ViewComponent
    {
        private readonly IHP_AffiliatesReopsitory _hP_AffiliatesReopsitory;
        private readonly IConfiguration _configuration;

        public AfflitiesViewComponent(IHP_AffiliatesReopsitory hP_AffiliatesReopsitory, IConfiguration configuration)
        {
            _hP_AffiliatesReopsitory = hP_AffiliatesReopsitory;
            _configuration = configuration;
        }

        public IViewComponentResult Invoke()
        {
            var items = _hP_AffiliatesReopsitory.GetAll();
            //get image base url to add it to the relative url
            var imageBaseURL = _configuration.GetValue<string>("BackEndDomain");
            foreach (var item in items)
            {
                if (item.ImageUrl != null)
                    item.ImageUrl = imageBaseURL + item.ImageUrl.Replace(" ", "%20");
            }
         
            return View(items);
        }
    }
}
