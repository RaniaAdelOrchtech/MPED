using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MPMAR.Business.ViewModels
{
    public class SectionCardEditViewModel
    {
        public int Id { get; set; }
        public int SectionVersionId { get; set; }

        [Required]
        public string EnTitle { get; set; }

        [Required]
        public string ArTitle { get; set; }

        [Required]
        public string EnDescription { get; set; }

        [Required]
        public string ArDescription { get; set; }

        public string EnImageAlt { get; set; }

        [Required]
        public string ArImageAlt { get; set; }

        public IFormFile Photo { get; set; }

        public string ImageUrl { get; set; }

        public IFormFile File { get; set; }
        public string FileUrl { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter an integer number greater than ZERO")]
        public int? Order { get; set; }

        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? CreationDate { get; set; }
        public string CreatedById { get; set; }
        public int PageRouteVersionId { get; set; }
        public int? ApprovalId { get; set; }
    }
}
