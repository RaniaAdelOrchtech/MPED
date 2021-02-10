using Microsoft.AspNetCore.Http;
using MPMAR.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MPMAR.Data.HomePageModels.ViewModels
{
    public class HP_LogoLinkViewModel : ActionInfo
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        [Required]
        public IFormFile ImageFile { get; set; }
        [Required]
        [MaxLength(30)]
        public string ArTitle { get; set; }
        [Required]
        [MaxLength(30)]
        public string EnTitle { get; set; }
        [Required]
        [Url]
        public string Url { get; set; }
        public ChangeActionEnum? ChangeActionEnum { get; set; }
        public VersionStatusEnum? VersionStatusEnum { get; set; }
        public int? LogoLinkId { get; set; }
    }
}