using Microsoft.AspNetCore.Mvc.Rendering;
using MPMAR.Business.ViewModels;
using MPMAR.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPMAR.Web.Admin.ViewModels
{
    public class PageSectionEditViewModel
    {
        public PageSectionEditViewModel()
        {

        }
        public PageSectionEditViewModel(List<PageSectionType> sectionTypes, SectionViewModel sectionVm)
        {
            SectionTypes = sectionTypes;
            Section = sectionVm;
            SectionTypeId = sectionVm.SectionTypeId;
        }
        public int Id { get; set; }
        public int pageRouteVersionId { get; set; }
        public SectionViewModel Section { get; set; }

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
