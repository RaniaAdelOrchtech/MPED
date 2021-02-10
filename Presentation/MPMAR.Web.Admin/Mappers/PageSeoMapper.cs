using MPMAR.Data;
using MPMAR.Web.Admin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPMAR.Web.Admin.Mappers
{
    public static class PageSeoMapper
    {
        public static PageSeo MapToPageSeo(this PageRouteCreateViewModel pageRouteViewModel)
        {
            PageSeo pageSeo = new PageSeo();
            pageSeo.SeoTitleEN = pageRouteViewModel.SeoTitleEN;
            pageSeo.SeoTitleAR = pageRouteViewModel.SeoTitleAR;
            pageSeo.SeoDescriptionEN = pageRouteViewModel.SeoDescriptionEN;
            pageSeo.SeoDescriptionAR = pageRouteViewModel.SeoDescriptionAR;
            pageSeo.SeoOgTitleEN = pageRouteViewModel.SeoOgTitleEN;
            pageSeo.SeoOgTitleAR = pageRouteViewModel.SeoOgTitleAR;
            pageSeo.SeoTwitterCardEN = pageRouteViewModel.SeoTwitterCardEN;
            pageSeo.SeoTwitterCardAR = pageRouteViewModel.SeoTwitterCardAR;

            return pageSeo;
        }
    }
}
