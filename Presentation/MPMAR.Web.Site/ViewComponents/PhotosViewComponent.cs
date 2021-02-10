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
    public class PhotosViewComponent : ViewComponent
    {
        private readonly IHP_PhotosReopsitory _hP_PhotosReopsitory;
        private readonly IConfiguration _configuration;

        public PhotosViewComponent(IHP_PhotosReopsitory hP_PhotosReopsitory, IConfiguration configuration)
        {
            _hP_PhotosReopsitory = hP_PhotosReopsitory;
            _configuration = configuration;
        }

        public IViewComponentResult Invoke()
        {
            var items = _hP_PhotosReopsitory.GetAll();
            //get image base url to add it to the relative url
            var imageBaseURL = _configuration.GetValue<string>("BackEndDomain");
            foreach (var item in items)
            {
                item.ImageUrl = imageBaseURL + item.ImageUrl.Replace(" ", "%20");
            }
            return View(items);
        }
    }
}
