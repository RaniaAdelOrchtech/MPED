using System;
using System.Collections.Generic;
using System.Text;

namespace MPMAR.Data.HomePageModels
{
    /// <summary>
    /// Class for HomePageBasicInfo table which form HomePageBasicInfo model used in HomePageBasicInfo screen
    /// </summary>
    public class HomePageBasicInfo : PageSeo
    {
        public int Id { get; set; }
        public string LogoUrl { get; set; }
        public string FavIconUrl { get; set; }
    }
}
