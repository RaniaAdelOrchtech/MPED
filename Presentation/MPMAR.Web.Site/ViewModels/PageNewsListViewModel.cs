using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MPMAR.Data;

namespace MPMAR.Web.Site.ViewModels
{
    public class PageNewsListViewModel
    {
        public IEnumerable<PageNews> PageNews { get; set; }
        public IEnumerable<PageNewsType> PageNewsTypes { get; set; }
        public PageNews SinglePageNews { get; set; }
        public string WebHostEnvironment { get; set; }
        //public int Id { get; set; }
        public int FirstId { get; set; }
        public int LastId { get; set; }

        public List<News> NewsList { get; set; }
        public string Date { get; set; }
        public class News
        {
            public int Id { get; set; }
            public string EnTitle { get; set; }
            public string ArTitle { get; set; }
            public string EnShortDescription { get; set; }
            public string ArShortDescription { get; set; }
            public string EnDescription { get; set; }
            public string ArDescription { get; set; }
            public string Url { get; set; }
            public string Date { get; set; }
            public string NewsTypes { get; set; }
            public string NewsTypesClasses { get; set; }

        }
    }
}
