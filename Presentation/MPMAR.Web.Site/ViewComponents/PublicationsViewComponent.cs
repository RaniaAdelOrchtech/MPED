using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MPMAR.Business.Interfaces;
using MPMAR.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPMAR.Web.Site.ViewComponents
{
    public class PublicationsViewComponent : ViewComponent
    {

        private readonly IPublicationRepository _publicationRepository;
        private readonly IConfiguration _configuration;

        public PublicationsViewComponent(IPublicationRepository publicationRepository, IConfiguration configuration)
        {

            _publicationRepository = publicationRepository;
            _configuration = configuration;
        }
        public IViewComponentResult Invoke()
        {
            var model = _publicationRepository.GetAll().FirstOrDefault(x => x.IsActive);
            //get image base url to add it to the relative url
            var imageBaseURL = _configuration.GetValue<string>("BackEndDomain");
            model.Image1 = imageBaseURL + model.Image1.Replace(" ", "%20");
            model.Image2 = imageBaseURL + model.Image2.Replace(" ", "%20");
            model.Image3 = imageBaseURL + model.Image3.Replace(" ", "%20");
            return View(model);
        }
    }
}
