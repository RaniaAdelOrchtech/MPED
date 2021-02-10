using Microsoft.AspNetCore.Http;
using MPMAR.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MPMAR.Data.HomePageModels.ViewModels
{
    public class HP_AffiliatesViewModel : ActionInfo
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        [Required]
        public string ArDescription { get; set; }
        [Required]
        public string EnDescription { get; set; }
        [Required]
        [Url]
        public string Url { get; set; }
        public AffiliatesType Type { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        [Required]
        public IFormFile ImageFile { get; set; }

        public ChangeActionEnum? ChangeActionEnum { get; set; }
        public VersionStatusEnum? VersionStatusEnum { get; set; }
        public int? HomePageAffiliatesId { get; set; }
    }
}
