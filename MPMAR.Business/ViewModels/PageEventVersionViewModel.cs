using Microsoft.AspNetCore.Http;
using MPMAR.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MPMAR.Business.ViewModels
{
    public class PageEventVersionViewModel : PageSeo
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "English Title")]
        public string EnTitle { get; set; }

        [Required]
        [Display(Name = "Arabic Title")]
        public string ArTitle { get; set; }

        //[Required]
        [Display(Name = "English Description")]
        public string EnDescription { get; set; }

        //[Required]
        [Display(Name = "Arabic Description")]
        public string ArDescription { get; set; }


        [Display(Name = "English Image Alt")]
        public string EnImageAlt { get; set; }


        [Display(Name = "Arabic Image Alt")]
        public string ArImageAlt { get; set; }


        public string Url { get; set; }

        public string DetailUrl { get; set; }

        public IFormFile Photo { get; set; }
        public IFormFile DetailPhoto { get; set; }

        //[Required]
        //[Range(1, int.MaxValue, ErrorMessage = "Please enter valid integer number")]
        public int? Order { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }
        // public DateTime? CreationDate { get; set; }
        // public string CreatedById { get; set; }

        //[Required]
        [Display(Name = "Event Date Range")]
        public string EventDateRange { get; set; }

        public string EnAddress { get; set; }
        public string ArAddress { get; set; }
        public string EventLocation { get; set; }

        public string EventLocationUrl { get; set; }

        public string EventDateColor { get; set; }

        public string EventCaption { get; set; }
        public string EventSocialLinks { get; set; }


        public DateTime? EventStartDate { get; set; }
        public DateTime? EventEndDate { get; set; }

        public int PageRouteId { get; set; }
        public int? PageEventId { get; set; }
    }
}
