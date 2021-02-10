using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MPMAR.Data;
using MPMAR.Web.Site.ViewModels;

namespace MPMAR.Web.Site.Controllers
{
    public class DynamicPageController : BaseController
    {
        private readonly ApplicationDbContext _dataAccessService;
        private readonly IConfiguration _configuration;
        public DynamicPageController(ApplicationDbContext dataAccessService, IConfiguration configuration)
        {
            _dataAccessService = dataAccessService;
            _configuration = configuration;
        }
        /// <summary>
        /// get DynamicPage index page
        /// </summary>
        /// <param name="id"></param>
        /// <param name="lang"></param>
        /// <returns></returns>
        public IActionResult Index(int id, string lang)
        {
            var model = new DynamicPageViewModel();
            var page = _dataAccessService.PageRoutes.Include(x => x.PageSections).FirstOrDefault(d => d.Id == id);

            if (page == null || !page.IsActive || page.IsDeleted || page.PageSections == null || !page.PageSections.Any(x => !x.IsDeleted && x.IsActive))
            {
                return View("Error");
            }
            SetUpSEO(lang, page);
            ViewBag.PageTitle = lang == "en" ? page.EnName : page.ArName;
            //get image base url to add it to the relative url
            var imageBaseURL = _configuration.GetValue<string>("BackEndDomain");
            //get the path that holds Dynamic Pages
            var dynamicPagesPath = _configuration.GetValue<string>("DynamicPagesPath");
            if (page != null)
            {
                var contentFile = new FileInfo(dynamicPagesPath + (lang == "en" ? page.PageFilePathEn : page.PageFilePathAr));
                string Template = contentFile.OpenText().ReadToEnd();
                model.HTMLPage = Template.Replace("#baseURL#", imageBaseURL);
            }
            return View(model);
        }
        /// <summary>
        /// get side bar component 
        /// </summary>
        /// <returns></returns>
        public IActionResult SideBarComponant()
        {
            return ViewComponent("Sidebar");
        }
    }
}