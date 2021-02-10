using Microsoft.AspNetCore.Http;
using MPMAR.Data;
using MPMAR.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MPMAR.Web.Admin.ViewModels
{
    public class MinistriesViewModel: PageSeoVersion
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        [Display(Name = "Name En")]
        public string EnName { get; set; }
        [Required]
        [MaxLength(100)]
        [Display(Name = "Name Ar")]
        public string ArName { get; set; }
        [Required]
        [MaxLength(800)]
        [Display(Name = "Description En")]
        public string EnDescription { get; set; }
        [Required]
        [MaxLength(800)]
        [Display(Name = "Description Ar")]
        public string ArDescription { get; set; }
        [Required]
        [MaxLength(30)]
        [Display(Name = "Period Ar")]
        public string PeriodAr { get; set; }
        [Required]
        [MaxLength(30)]
        [Display(Name = "Period En")]
        public string PeriodEn { get; set; }
        [Display(Name = "Image URL")]
        public string ImageURL { get; set; }
        [Display(Name = "Image File")]
        public IFormFile ImageFile { get; set; }
        [Required]
        public int Order { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public string CreatedById { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public string ApprovedById { get; set; }
        public string Facebook { get; set; }
        public string Twitter { get; set; }
        public string Email { get; set; }
        public ChangeActionEnum? ChangeActionEnum { get; set; }
        public VersionStatusEnum? VersionStatusEnum { get; set; }
        public int? MinistryTimeLineId { get; set; }
        public int? FormerMinistriesPageInfoVersionsId { get; set; }
    }
}
