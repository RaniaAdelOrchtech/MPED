using Microsoft.AspNetCore.Http;
using MPMAR.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MPMAR.Web.Admin.ViewModels
{
    public class MinistryTimeLineViewModel : PageSeo
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "English Name ")]
        public string EnName { get; set; }

        [Required]
        [Display(Name = "Arabic Name ")]
        public string ArName { get; set; }

        [Required]
        [AllowHtml]
        [Display(Name = "English Description")]
        public string EnDescription { get; set; }

        [Required]
        [AllowHtml]
        [Display(Name = "Arabic Description")]
        public string ArDescription { get; set; }

        public IFormFile Photo { get; set; }
        public string ProfileImageUrl { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter valid integer number")]
        public int? Order { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }
      
        [Required]
        [Display(Name = "Event Date Range")]
        public string EventDateRange { get; set; }
        public string EventSocialLinks { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int StatusId { get; set; }

    }
}
