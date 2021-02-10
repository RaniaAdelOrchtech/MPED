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
    public class EconomicViewComponent : ViewComponent
    {
        private readonly IHP_EconomicDevelopmentReopsitory _hP_EconomicDevelopmentReopsitory;
        private readonly IConfiguration _configuration;

        public EconomicViewComponent(IHP_EconomicDevelopmentReopsitory hP_EconomicDevelopmentReopsitory, IConfiguration configuration)
        {
            _hP_EconomicDevelopmentReopsitory = hP_EconomicDevelopmentReopsitory;
            _configuration = configuration;
        }

        public IViewComponentResult Invoke()
        {
            var items = _hP_EconomicDevelopmentReopsitory.GetAll();
            //get image base url to add it to the relative url
            var imageBaseURL = _configuration.GetValue<string>("BackEndDomain");
            foreach (var model in items)
                model.BackGroundImage = imageBaseURL + model.BackGroundImage.Replace(" ", "%20");
            return View(items);
        }
    }
}
