using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MPMAR.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPMAR.Web.Site.ViewComponents
{
    public class MonitoringViewComponent : ViewComponent
    {
        private readonly IMonitoringRepository _monitoringRepository;
        private readonly IConfiguration _configuration;

        public MonitoringViewComponent(IMonitoringRepository monitoringRepository, IConfiguration configuration)
        {

            _monitoringRepository = monitoringRepository;
            _configuration = configuration;
        }
        public IViewComponentResult Invoke()
        {
            var model = _monitoringRepository.Get();
            //get image base url to add it to the relative url
            var imageBaseURL = _configuration.GetValue<string>("BackEndDomain");
            model.Image1 = imageBaseURL + model.Image1.Replace(" ", "%20");
            model.BackGroundImage = imageBaseURL + model.BackGroundImage.Replace(" ", "%20");
            return View(model);
        }
    }
}
