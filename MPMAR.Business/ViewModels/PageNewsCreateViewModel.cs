using Microsoft.AspNetCore.Mvc.Rendering;
using MPMAR.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MPMAR.Business.ViewModels
{
    public class PageNewsCreateViewModel
    {
        public int PageRouteId { get; set; }

        public PageNewsCreateViewModel(List<PageNewsType> newsTypes)
        {
            NewsTypes = newsTypes;
            News = new NewsViewModel();
        }
        public PageNewsCreateViewModel()
        {

        }
        [Required]
        [Display(Name = "News Type")]
        public int? NewsTypeId { get; set; }

        public string NewsTypeIds { get; set; }
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

    }
}
