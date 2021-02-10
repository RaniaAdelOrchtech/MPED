using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MPMAR.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPMAR.Web.Site.ViewComponents
{
    public class CitizenPlanViewComponent : ViewComponent
    {
        private readonly ICitizenPlanRepository _citizenPlanRepository;
        private readonly IConfiguration _configuration;

        public CitizenPlanViewComponent(ICitizenPlanRepository citizenPlanRepository, IConfiguration configuration)
        {
            _citizenPlanRepository = citizenPlanRepository;
            _configuration = configuration;
        }

        public IViewComponentResult Invoke()
        {
            var model = _citizenPlanRepository.Get();
            //get image base url to add it to the relative url
            var imageBaseURL = _configuration.GetValue<string>("BackEndDomain");
            if (model.Image != null)
            {
                model.Image = imageBaseURL + model.Image.Replace(" ", "%20");

            }
            if (model.EnImage != null)
            {

                model.EnImage = imageBaseURL + model.EnImage.Replace(" ", "%20");
            }
            return View(model);
        }
    }
}
