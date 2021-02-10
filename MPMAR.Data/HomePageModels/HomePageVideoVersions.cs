using MPMAR.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MPMAR.Data.HomePageModels
{
    /// <summary>
    /// Class for HomePageVideoVersions table which form HomePageVideoVersions model used in HomePageVideo screen
    /// </summary>
    public class HomePageVideoVersions : ActionInfo
    {
        public int Id { get; set; }
        [Required]
        public string VideoUrl { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public ChangeActionEnum? ChangeActionEnum { get; set; }
        public VersionStatusEnum? VersionStatusEnum { get; set; }
        public int? HomePageVideoId { get; set; }
        public HomePageVideo HomePageVideo { get; set; }
    }
}
