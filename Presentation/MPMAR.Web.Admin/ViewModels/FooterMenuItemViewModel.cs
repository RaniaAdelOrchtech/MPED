using MPMAR.Data;
using MPMAR.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MPMAR.Web.Admin.ViewModels
{
    public class FooterMenuItemViewModel : ActionInfoVersion
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
        public ChangeActionEnum? ChangeActionEnum { get; set; }
        public VersionStatusEnum? VersionStatusEnum { get; set; }
        public int? FooterMenuItemId { get; set; }
    }
}
