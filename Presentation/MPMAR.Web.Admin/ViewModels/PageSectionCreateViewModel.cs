using Microsoft.AspNetCore.Mvc.Rendering;
using MPMAR.Business.ViewModels;
using MPMAR.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MPMAR.Web.Admin.ViewModels
{
    public class PageSectionCreateViewModel
    {

        public PageSectionCreateViewModel()
        {

        }


        public PageSectionCreateViewModel(List<PageSectionType> sectionTypes)
        {
            SectionTypes = sectionTypes;
            Section = new SectionViewModel();
        }

        public int pageRouteVersionId { get; set; }
        public SectionViewModel Section { get; set; }

        public string submit { get; set; }

        [Required]
        [Display(Name = "Section Type")]
        public int? SectionTypeId { get; set; }
        public ICollection<PageSectionType> SectionTypes { get; set; }
        public List<SelectListItem> AllSectionTypes
        {
            get
            {
                if (SectionTypes == null)
                {
                    return null;
                }
                else
                {
                    return SectionTypes.Select(x => new SelectListItem()
                    {
                        Text = x.EnName,
                        Value = x.Id.ToString()
                    }).ToList();
                }
            }
        }
    }
}
