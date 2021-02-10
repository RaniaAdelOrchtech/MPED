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
    public class MinistryVisionViewComponent : ViewComponent
    {
        private readonly IMinistryVisionRepository _ministryVissionRepository;
        private readonly IConfiguration _configuration;

        public MinistryVisionViewComponent(IMinistryVisionRepository ministryVissionRepository, IConfiguration configuration)
        {
            _ministryVissionRepository = ministryVissionRepository;
            _configuration = configuration;
        }

        public IViewComponentResult Invoke()
        {
            var data = _ministryVissionRepository.Get();
            //remove tags from description
            data.ArDescription = Regex.Replace(data.ArDescription, @"\t|\n|\r|<div>|</div>", "");
            data.EnDescription = Regex.Replace(data.EnDescription, @"\t|\n|\r|<div>|</div>", "");
            //get image base url to add it to the relative url
            var imageBaseURL = _configuration.GetValue<string>("BackEndDomain");
            data.BackGroundImage = imageBaseURL + data.BackGroundImage.Replace(" ", "%20");
            return View(data);
        }
    }
}
