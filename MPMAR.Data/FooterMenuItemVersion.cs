using MPMAR.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MPMAR.Data
{
    /// <summary>
    /// Class for FooterMenuItemVersion table which form FooterMenuItemVersion object used in FooterMenuItem screens
    /// </summary>
    public class FooterMenuItemVersion : ActionInfoVersion
    {
        [System.ComponentModel.DataAnnotations.Key]
        public int Id { get; set; }
        public string EnTitle { get; set; }
        public string ArTitle { get; set; }
        public string Link { get; set; }
        public int? Order { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public string EnColumnPostion { get; set; }
        public string ArColumnPostion { get; set; }
        [Required]
        public int? FooterMenuTitleId { get; set; }
        [ForeignKey(nameof(FooterMenuTitleId))]
        public FooterMenuTitle FooterMenuTitle { get; set; }
        public ChangeActionEnum? ChangeActionEnum { get; set; }
        public VersionStatusEnum? VersionStatusEnum { get; set; }
        public int? FooterMenuItemId { get; set; }
        public FooterMenuItem FooterMenuItem { get; set; }

    }
}
