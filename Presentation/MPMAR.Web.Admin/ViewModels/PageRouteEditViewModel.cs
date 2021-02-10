using Microsoft.AspNetCore.Mvc.Rendering;
using MPMAR.Data;
using MPMAR.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MPMAR.Web.Admin.ViewModels
{
    public class PageRouteEditViewModel : PageSeo
    {
        public PageRouteEditViewModel()
        {

        }
        public PageRouteEditViewModel(ICollection<NavItem> navItems)
        {
            NavItems = navItems;
        }
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name = "English Name")]
        public string EnName { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name = "Arabic Name")]
        public string ArName { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter an integer number greater than ZERO")]
        public int? Order { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }
        public bool IsDynamicPage { get; set; }
        public bool HasNavItem { get; set; }

        [Required]
        public int? NavItemId { get; set; }

        public string ControllerName { get; set; }

        public string PageType { get; set; }
        public string ActionName { get; set; }
        public string SectionName { get; set; }
        public ICollection<NavItem> NavItems { get; set; }
        public List<SelectListItem> AllNavItems
        {
            get
            {
                if (NavItems == null)
                {
                    return null;
                }
                else
                {
                    return NavItems.Select(x => new SelectListItem()
                    {
                        Text = x.EnName,
                        Value = x.Id.ToString()
                    }).ToList();
                }
            }
        }
        public ChangeActionEnum? ChangeActionEnum { get; set; }
        public VersionStatusEnum? VersionStatusEnum { get; set; }
        public VersionStatusEnum? ContentVersionStatusEnum { get; set; }
        public int? PageRouteId { get; set; }
    }
}
