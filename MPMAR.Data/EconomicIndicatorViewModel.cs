﻿using Microsoft.AspNetCore.Http;
using MPMAR.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MPMAR.Data
{
    /// <summary>
    /// Class for EconomicIndicatorsViewModel which form EconomicIndicatorViewModel object 
    /// used in any operation with EconomicIndecator Model
    /// </summary>
    public class EconomicIndicatorViewModel : ActionInfo
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(2000)]
        public string MainDiscriptionAr { get; set; }

        [Required]
        [MaxLength(2000)]
        public string MainDiscriptionEn { get; set; }

        [Required]
        public string ImageUrl1 { get; set; }

        [Required]
        [MaxLength(50)]
        public string ImageTitleAr1 { get; set; }

        [Required]
        [MaxLength(50)]
        public string ImageTitleEn1 { get; set; }

        [Required]
        [MaxLength(300)]
        public string ImageDiscriptionAr1 { get; set; }

        [Required]
        [MaxLength(300)]
        public string ImageDiscriptionEn1 { get; set; }

        [Required]
        public string Link1 { get; set; }

        [Required]
        public string ImageUrl2 { get; set; }

        [Required]
        [MaxLength(50)]
        public string ImageTitleAr2 { get; set; }

        [Required]
        [MaxLength(50)]
        public string ImageTitleEn2 { get; set; }

        [Required]
        [MaxLength(300)]
        public string ImageDiscriptionAr2 { get; set; }

        [Required]
        [MaxLength(300)]
        public string ImageDiscriptionEn2 { get; set; }

        [Required]
        public string Link2 { get; set; }

        [Required]
        public string ImageUrl3 { get; set; }

        [Required]
        [MaxLength(50)]
        public string ImageTitleAr3 { get; set; }

        [Required]
        [MaxLength(50)]
        public string ImageTitleEn3 { get; set; }

        [Required]
        [MaxLength(300)]
        public string ImageDiscriptionAr3 { get; set; }

        [Required]
        [MaxLength(300)]
        public string ImageDiscriptionEn3 { get; set; }

        [Required]
        public string Link3 { get; set; }

        public int order { get; set; }
        public IFormFile ImageFile { get; set; }

        public ChangeActionEnum? ChangeActionEnum { get; set; }
        public VersionStatusEnum? VersionStatusEnum { get; set; }
        public int? EconomicIndicatorId { get; set; }
    }
}
