using Microsoft.AspNetCore.Mvc.Rendering;
using MPMAR.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MPMAR.Web.Admin.ViewModels
{
    public class PageRouteCreateViewModel : PageSeo
    {
        public PageRouteCreateViewModel()
        {

        }
        public PageRouteCreateViewModel(ICollection<NavItem> navItems)
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

        [Required]
        public int? NavItemId { get; set; }

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
    }
}
