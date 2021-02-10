using Microsoft.AspNetCore.Http;
using MPMAR.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MPMAR.Business.ViewModels
{
    public class NewsViewModel
    {
        public int Id { get; set; }

        [Display(Name = "English Title")]
        public string EnTitle { get; set; }

        [Required]
        [Display(Name = "Arabic Title")]
        public string ArTitle { get; set; }



        [Display(Name = "English Short Description")]
        public string EnShortDescription { get; set; }

        [Required]
        [Display(Name = "Arabic Short Description")]
        public string ArShortDescription { get; set; }



        [Display(Name = "English Description")]
        public string EnDescription { get; set; }

        [Required]
        [Display(Name = "Arabic Description")]
        public string ArDescription { get; set; }

        
      

        public IFormFile Photo { get; set; }
        [Required]
        public DateTime? Date { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }
        //public int?  PageNewsTypeId { get; set; } 
        public DateTime? CreationDate { get; set; }
        public string CreatedById { get; set; }
        public string url { get; set; }
        public string NewsTypeIds { get; set; }
        public int PageRouteId { get; set; }
        public int? PageNewsId { get; set; }
        public ChangeActionEnum? ChangeActionEnum { get; set; }

        public VersionStatusEnum? VersionStatusEnum { get; set; }
    }
}
