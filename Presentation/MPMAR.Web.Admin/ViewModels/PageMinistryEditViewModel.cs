using Microsoft.AspNetCore.Http;
using MPMAR.Data;
using MPMAR.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MPMAR.Web.Admin.ViewModels
{
    public class PageMinistryEditViewModel  : PageSeo
    {
        public int Id { get; set; }

        //[Required]
        public string ArName { get; set; }

        //[Required]
        public string EnName { get; set; }

        //[Required]
        [AllowHtml]
        public string EnContent { get; set; }

        //[Required]
        [AllowHtml]
        public string ArContent { get; set; }


        //[Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter valid integer number")]
        public int? Order { get; set; }

        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        //public DateTime? CreationDate { get; set; }
        //public string CreatedById { get; set; }


       // public int? NavItemId { get; set; }
        //public int StatusId { get; set; }
        public bool IsHeading { get; set; }
        public bool IsSection { get; set; }
        public bool IsDobulQuote { get; set; }

        public string ImageUrl { get; set; }
        public IFormFile ImageFile { get; set; } 
        public string EnImageUrl { get; set; }
        public IFormFile EnImageFile { get; set; }

        public string Twitter { get; set; }

        public string Instagram { get; set; }

        public string Globe { get; set; }

        public ChangeActionEnum? ChangeActionEnum { get; set; }
        public VersionStatusEnum? VersionStatusEnum { get; set; }
        public int? PageMinistryId { get; set; }
        public int? PageRouteId { get; set; }

    }


    public class PageMinistryListViewModel 
    {
        public int Id { get; set; }
        public string ArName { get; set; }
        public string EnName { get; set; }
        public string ArContent { get; set; }
        public string ImageUrl { get; set; }
        public string EnImageUrl { get; set; }
        public string EnContent { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
