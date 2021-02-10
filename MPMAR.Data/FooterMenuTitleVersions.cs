using MPMAR.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MPMAR.Data
{
    /// <summary>
    /// Class for FooterMenuTitleVersion table which form FooterMenuTitleVersion object used in FooterMenuTitle screens
    /// </summary>
    public class FooterMenuTitleVersions : ActionInfoVersion
    {
        public int Id { get; set; }
        public string EnTitle { get; set; }
        public string ArTitle { get; set; }
        public int? Order { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<FooterMenuItem> FooterMenuItems { get; set; }

        public ChangeActionEnum? ChangeActionEnum { get; set; }
        public VersionStatusEnum? VersionStatusEnum { get; set; }
        public int? FooterMenuTitleId { get; set; }

        public FooterMenuTitle FooterMenuTitle { get; set; }

    }
}
