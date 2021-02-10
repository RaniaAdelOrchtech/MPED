using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MPMAR.Web.Admin.ViewModels
{
    public class NewsTypeViewModel
    {
        public int? Id { get; set; }
        [Required]
        [Display(Name = "English Title")]
        public string EnName { get; set; }

        [Required]
        [Display(Name = "Arabic Title")]
        public string ArName { get; set; }
        public DateTime? CreationDate { get; set; }
        public string CreatedById { get; set; }
    }
}
