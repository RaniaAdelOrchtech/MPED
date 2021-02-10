using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MPMAR.Data;

namespace MPMAR.Web.Site.Controllers
{
    public class BaseController : Controller
    {
        /// <summary>
        /// setup SEO for every page call it by passing page info
        /// </summary>
        /// <param name="lang"></param>
        /// <param name="pageRoute">PageRoute holds some SEO info</param>
        /// <param name="url"></param>
        /// <param name="ogImgPath"></param>
        public void SetUpSEO(string lang, PageRoute pageRoute,string url="",string ogImgPath="")
        {
            ViewBag.URL = url;
            ViewBag.OgImagePath = ogImgPath;

            if (lang == null || lang.ToLower() == "ar")
            {
                ViewBag.Description = pageRoute.SeoDescriptionAR;
                ViewBag.OgTitle = pageRoute.SeoOgTitleAR;
                ViewBag.TwitterTitle = pageRoute.SeoTwitterCardAR;
            }
            else
            {
                ViewBag.Description = pageRoute.SeoDescriptionEN;
                ViewBag.OgTitle = pageRoute.SeoOgTitleEN;
                ViewBag.TwitterTitle = pageRoute.SeoTwitterCardEN;
            }


        }
    }
}