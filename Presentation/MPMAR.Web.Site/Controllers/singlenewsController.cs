using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MPMAR.Web.Site.ViewModels;
using MPMAR.Business.Interfaces;
using Microsoft.AspNetCore.Hosting;
using MPMAR.Data;
using Microsoft.Extensions.Configuration;

namespace MPMAR.Web.Site.Controllers
{
    public class singlenewsController : Controller
    {

        private readonly IPageNewsRepository _PageNewsRepository;
        private readonly IWebHostEnvironment _IWebHostEnvironment;
        private readonly IConfiguration _configuration;
        public singlenewsController(IPageNewsRepository PageNewsRepository, IWebHostEnvironment WebHostEnvironment, IConfiguration configuration)
        {
            _PageNewsRepository = PageNewsRepository;
            _IWebHostEnvironment = WebHostEnvironment;
            _configuration = configuration;

        }
        /// <summary>
        /// get singlenews page index
        /// </summary>
        /// <param name="id">page news id</param>
        /// <param name="type">next or previous</param>
        /// <param name="lang"></param>
        /// <returns></returns>
        public IActionResult Index(int id, string type, string lang)
        {
            var imageBaseURL = _configuration.GetValue<string>("BackEndDomain");
            PageNewsListViewModel PageNewsListVModel = new PageNewsListViewModel();
            //current
            PageNewsListVModel.SinglePageNews = _PageNewsRepository.GetSinglePageNewsByPageNewsId(id);
            //all news
            var AllPageNews = GetPageNews();
            if (lang == "en")
            {
                AllPageNews = AllPageNews.Where(x => !string.IsNullOrWhiteSpace(x.EnDescription) || !string.IsNullOrWhiteSpace(x.EnTitle) || !string.IsNullOrWhiteSpace(x.EnShortDescription)).ToList();
            }
            if (type == "next") //to handle next news
            {
                PageNewsListVModel.SinglePageNews = AllPageNews[AllPageNews.FindIndex(a => a.Id == id) - 1];
                id = PageNewsListVModel.SinglePageNews.Id;
            }
            if (type == "previous") //to handle previous news
            {
                PageNewsListVModel.SinglePageNews = AllPageNews[AllPageNews.FindIndex(a => a.Id == id) + 1];
                id = PageNewsListVModel.SinglePageNews.Id;
            }
            PageNewsListVModel.FirstId = AllPageNews.FirstOrDefault().Id;
            PageNewsListVModel.LastId = AllPageNews.Last().Id;

            //top 3 news
            PageNewsListVModel.PageNews = AllPageNews.Where(a => a.Id != id).Take(3);
            foreach (var item in PageNewsListVModel.PageNews)
            {
                if (item.Url != null)
                    item.Url = imageBaseURL + item.Url.Replace(" ", "%20");
            }
            if (lang == "en")
            {
                PageNewsListVModel.Date = PageNewsListVModel.SinglePageNews.Date.Value.ToString("dd MMMM yyyy");
            }
            else
            {
                PageNewsListVModel.Date = PageNewsListVModel.SinglePageNews.Date.Value.ToString("dd MMMM yyyy", System.Globalization.CultureInfo.GetCultureInfo("ar-AE"));
            }

            PageNewsListVModel.WebHostEnvironment = _IWebHostEnvironment.WebRootPath;

            if (PageNewsListVModel.SinglePageNews.Url != null)
                PageNewsListVModel.SinglePageNews.Url = imageBaseURL + PageNewsListVModel.SinglePageNews.Url.Replace(" ", "%20");
            PageNewsListVModel.SinglePageNews = RemoveTagsFromDescription(PageNewsListVModel.SinglePageNews);

            PageNewsListVModel.SinglePageNews.ArDescription= PageNewsListVModel.SinglePageNews.ArDescription.Replace("<div>", "<p>").Replace("</div>","</p>");
            PageNewsListVModel.SinglePageNews.EnDescription = PageNewsListVModel.SinglePageNews.EnDescription?.Replace("<div>", "<p>").Replace("</div>", "</p>");
            return View(PageNewsListVModel);
        }

        /// <summary>
        /// get list of page news
        /// </summary>
        /// <returns></returns>
        public List<PageNews> GetPageNews()
        {
            return _PageNewsRepository.GetPageNews().ToList();

        }

        /// <summary>
        /// remove <p>, </p> and replace : left and : right with center from description
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        private PageNews RemoveTagsFromDescription(PageNews viewModel)
        {
            if (viewModel.ArDescription != null)
            {
                viewModel.ArDescription = viewModel.ArDescription.Replace("<p>", "");
                viewModel.ArDescription = viewModel.ArDescription.Replace("</p>", "");
                viewModel.ArDescription = viewModel.ArDescription.Replace(": left", ": center");
                viewModel.ArDescription = viewModel.ArDescription.Replace(": right", ": center");
            }
            if (viewModel.EnDescription != null)
            {
                viewModel.EnDescription = viewModel.EnDescription.Replace("<p>", "");
                viewModel.EnDescription = viewModel.EnDescription.Replace("</p>", "");
                viewModel.EnDescription = viewModel.EnDescription.Replace(": left", ": center");
                viewModel.EnDescription = viewModel.EnDescription.Replace(": right", ": center");
            }
            if (viewModel.ArShortDescription != null)
            {
                viewModel.ArShortDescription = viewModel.ArShortDescription.Replace("<p>", "");
                viewModel.ArShortDescription = viewModel.ArShortDescription.Replace("</p>", "");
                viewModel.ArShortDescription = viewModel.ArShortDescription.Replace(": left", ": center");
                viewModel.ArShortDescription = viewModel.ArShortDescription.Replace(": right", ": center");
            }
            if (viewModel.EnShortDescription != null)
            {
                viewModel.EnShortDescription = viewModel.EnShortDescription.Replace("<p>", "");
                viewModel.EnShortDescription = viewModel.EnShortDescription.Replace("</p>", "");
                viewModel.EnShortDescription = viewModel.EnShortDescription.Replace(": left", ": center");
                viewModel.EnShortDescription = viewModel.EnShortDescription.Replace(": right", ": center");
            }

            return viewModel;
        }

    }
}