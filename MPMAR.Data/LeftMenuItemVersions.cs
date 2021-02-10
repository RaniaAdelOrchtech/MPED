using MPMAR.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPMAR.Data
{
    /// <summary>
    /// Class for LeftMenuItemVersions table which form LeftMenuItemVersions object used in LeftMenuItem screens
    /// </summary>
    public class LeftMenuItemVersions : ActionInfoVersion
    {
        [System.ComponentModel.DataAnnotations.Key]
        public int Id { get; set; }
        public string LeftMenuType { get; set; }
        public string EnTitle { get; set; }
        public string ArTitle { get; set; }
        public string Link { get; set; }
        public int? Order { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public string ImagePath { get; set; }

        public ChangeActionEnum? ChangeActionEnum { get; set; }
        public VersionStatusEnum? VersionStatusEnum { get; set; }
        public int? LeftMenuItemId { get; set; }

        public LeftMenuItem LeftMenuItem { get; set; }

    }
}
