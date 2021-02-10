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
    public class VideoViewComponent : ViewComponent
    {
        private readonly IHP_VideoReopsitory _hP_VideoReopsitory;
        private readonly IConfiguration _configuration;

        public VideoViewComponent(IHP_VideoReopsitory hP_VideoReopsitory,IConfiguration configuration)
        {
            _hP_VideoReopsitory = hP_VideoReopsitory;
        }

        public IViewComponentResult Invoke()
        {
            var items = _hP_VideoReopsitory.GetAll();
            return View(items);
        }
    }
}
