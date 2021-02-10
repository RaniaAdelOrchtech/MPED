using MPMAR.Business.ViewModels;
using MPMAR.Data;
using MPMAR.Web.Admin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPMAR.Web.Admin.Mappers
{
    public static class PageNewsMapper
    {
       
      
        //create
        public static PageNewsVersion MapToPageNewsViewModel(this PageNewsCreateViewModel PageNewsCreateViewModel)
        {
            PageNewsVersion viewModel = new PageNewsVersion()
            {
                EnTitle = PageNewsCreateViewModel.News.EnTitle,
                ArTitle= PageNewsCreateViewModel.News.ArTitle,
                EnDescription = PageNewsCreateViewModel.News.EnDescription,
                ArDescription = PageNewsCreateViewModel.News.ArDescription,
                EnShortDescription = PageNewsCreateViewModel.News.EnShortDescription,
                ArShortDescription = PageNewsCreateViewModel.News.ArShortDescription,
                IsActive = PageNewsCreateViewModel.News.IsActive,
                Date = PageNewsCreateViewModel.News.Date,
                NewsTypesForNewsVersions= PageNewsCreateViewModel.MapToNewsTypeForNews()                
            }; 

            return viewModel;
        }

        //edit get
        public static NewsViewModel MapToPageNewsViewModelInEdit(this PageNews PageNews)
        {
            NewsViewModel NewsViewModel = new NewsViewModel
            {
                Id = PageNews.Id,
                EnTitle = PageNews.EnTitle,
                ArTitle = PageNews.ArTitle,
                EnDescription = PageNews.EnDescription,
                ArDescription = PageNews.ArDescription,
                EnShortDescription = PageNews.EnShortDescription,
                ArShortDescription = PageNews.ArShortDescription,
                IsActive = PageNews.IsActive,
                url = PageNews.Url,
                //PageNewsTypeId = PageNews.PageNewsTypeId,
                CreationDate = PageNews.CreationDate,
                CreatedById = PageNews.CreatedById,
                Date = PageNews.Date,
            };

            return NewsViewModel;
        }
        //edit post
        public static PageNewsVersion MapToPageNewsVersion(this PageNewsEditViewModel PageNewsViewModel)
        {
            PageNewsVersion pageNews = new PageNewsVersion();
            pageNews.Id = PageNewsViewModel.News.Id;
            pageNews.EnTitle = PageNewsViewModel.News.EnTitle;
            pageNews.ArTitle = PageNewsViewModel.News.ArTitle;
            pageNews.EnDescription = PageNewsViewModel.News.EnDescription;
            pageNews.ArDescription = PageNewsViewModel.News.ArDescription;
            pageNews.EnShortDescription = PageNewsViewModel.News.EnShortDescription;
            pageNews.ArShortDescription = PageNewsViewModel.News.ArShortDescription;
            pageNews.Url = PageNewsViewModel.News.url;
            pageNews.IsActive = PageNewsViewModel.News.IsActive;
            pageNews.CreationDate = PageNewsViewModel.News.CreationDate.Value;
            pageNews.CreatedById = PageNewsViewModel.News.CreatedById;
            pageNews.Date = PageNewsViewModel.News.Date;
            pageNews.PageNewsId = PageNewsViewModel.News.PageNewsId;
            pageNews.ChangeActionEnum = PageNewsViewModel.News.ChangeActionEnum; 
            pageNews.VersionStatusEnum = PageNewsViewModel.News.VersionStatusEnum; 
            return pageNews;
        }

        //view
        //public static PageNews MapToNewsVersion(this PageNews News)
        //{
        //    PageNews PageNews = new PageNews
        //    {
        //        Id = News.Id,
        //        EnTitle = News.EnTitle,
        //        ArTitle = News.ArTitle,
        //        EnDescription = News.EnDescription,
        //        ArDescription = News.ArDescription,
        //        EnShortDescription=News.EnShortDescription,
        //        ArShortDescription=News.ArShortDescription,
        //        IsActive = News.IsActive,
        //        Url = News.Url,
        //    };

        //    return PageNews;
        //}


        public static List<NewsTypesForNewsVersion> MapToNewsTypeForNews(this PageNewsCreateViewModel PageNewsViewModel)
        {
            List<NewsTypesForNewsVersion> NewsTypesForNewsList=new List<NewsTypesForNewsVersion>();
            string NewsTypeIds = PageNewsViewModel.NewsTypeIds;
            var NewsTypeIdsList = NewsTypeIds.Split(',');
            foreach (var NewsTypeId in NewsTypeIdsList)
            {
                NewsTypesForNewsVersion NewsTypesForNews = new NewsTypesForNewsVersion();
                NewsTypesForNews.NewsTypeId =int.Parse(NewsTypeId);
                NewsTypesForNewsList.Add(NewsTypesForNews);
            }
            return NewsTypesForNewsList;
        }

        public static List<NewsTypesForNewsVersion> MapToNewsTypeForNewsForEdit(this PageNewsEditViewModel PageNewsEditViewModel)
        {
            List<NewsTypesForNewsVersion> NewsTypesForNewsList = new List<NewsTypesForNewsVersion>();
            string NewsTypeIds = PageNewsEditViewModel.NewsTypesIds;
            var NewsTypeIdsList = NewsTypeIds.Split(',');
            foreach (var NewsTypeId in NewsTypeIdsList)
            {
                NewsTypesForNewsVersion NewsTypesForNews = new NewsTypesForNewsVersion();
                NewsTypesForNews.NewsTypeId = int.Parse(NewsTypeId);
                NewsTypesForNewsList.Add(NewsTypesForNews);
            }
            return NewsTypesForNewsList;
        }


    }
}

