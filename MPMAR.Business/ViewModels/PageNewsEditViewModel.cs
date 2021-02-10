using Microsoft.AspNetCore.Mvc.Rendering;
using MPMAR.Business.ViewModels;
using MPMAR.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MPMAR.Business.ViewModels
{
    public class PageNewsEditViewModel
    {
        public int PageRouteId { get; set; }

        public PageNewsEditViewModel(List<PageNewsType> PageNewsType, NewsViewModel NewsViewModel)
        {
            NewsTypes = PageNewsType;
            News = NewsViewModel;
        }
        public PageNewsEditViewModel(NewsViewModel NewsViewModel)
        {
            News = NewsViewModel;
        }
        public PageNewsEditViewModel(List<PageNewsType> newsTypes)
        {
            NewsTypes = newsTypes;
            News = new NewsViewModel();
        }
        public PageNewsEditViewModel()
        {

        }
        [Required]
        [Display(Name = "News Type")]
        public int? NewsTypeId { get; set; }
        public ICollection<PageNewsType> NewsTypes { get; set; }
        public List<SelectListItem> AllNewsTypes
        {
            get
            {
                if (NewsTypes == null)
                {
                    return null;
                }
                else
                {
                    return NewsTypes.Select(x => new SelectListItem()
                    {
                        Text = x.EnName,
                        Value = x.Id.ToString()
                    }).ToList();
                }
            }
        }

        public NewsViewModel News { get; set; }

        public string NewsTypesIds { get; set; }
    }
}
