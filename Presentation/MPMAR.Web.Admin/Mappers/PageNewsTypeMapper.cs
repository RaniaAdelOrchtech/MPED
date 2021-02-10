using MPMAR.Data;
using MPMAR.Web.Admin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPMAR.Web.Admin.Mappers
{
    public static class PageNewsTypeMapper
    {
        //index
        public static List<PageNewsTypeListViewModel> MapToPageNewsTypeListViewModel(this IEnumerable<PageNewsType> pageNewsType)
        {
            return pageNewsType.Select(NewsType => new PageNewsTypeListViewModel
            {
                Id= NewsType.Id,
                EnName = NewsType.EnName,
                ArName= NewsType.ArName,
                IsDeleted=NewsType.IsDeleted
            }).ToList();
        }
      
        //create
        public static PageNewsType MapToPageNewsTypeViewModel(this PageNewsTypeCreateViewModel PageNewsTypeCreateViewModel)
        {
            PageNewsType viewModel = new PageNewsType()
            {
                EnName = PageNewsTypeCreateViewModel.NewsType.EnName,
                ArName = PageNewsTypeCreateViewModel.NewsType.ArName,

            };

               return viewModel;
        }


            //edit get
            public static NewsTypeViewModel MapToPageNewsTypeViewModelInEdit(this PageNewsType PageNewsType)
            {
            NewsTypeViewModel newsTypeViewModel = new NewsTypeViewModel
            {
                Id = PageNewsType.Id,
                EnName = PageNewsType.EnName,
                ArName = PageNewsType.ArName,
            };

               return newsTypeViewModel;
        }

        //edit post
        public static PageNewsType MapToPageNewsTypeVersion(this PageNewsTypeEditViewModel PageNewsTypeViewModel)
        {
            PageNewsType PageNewsType = new PageNewsType();
            PageNewsType.Id = PageNewsTypeViewModel.NewsType.Id.Value;
            PageNewsType.EnName = PageNewsTypeViewModel.NewsType.EnName;
            PageNewsType.ArName = PageNewsTypeViewModel.NewsType.ArName;
            return PageNewsType;
        }

    }
}

