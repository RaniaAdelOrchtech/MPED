using MPMAR.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MPMAR.Data
{
    /// <summary>
    /// Class for ApprovalNotification table which form notification object used in Approval and history screens
    /// </summary>
    public class ApprovalNotification
    {
        public int Id { get; set; }
        [Required]
        public string ContentManagerId { get; set; }
        public ApplicationUser ContentManager { get; set; }
        [Required]
        public DateTime ChangesDateTime { get; set; }
        [Required]
        public string PageName { get; set; }
        [Required]
        public string PageLink { get; set; }
        [Required]
        public ChangeActionEnum ChangeAction { get; set; }
        [Required]
        public ChangeType ChangeType { get; set; }
        [Required]
        public PageType PageType { get; set; }
        [Required]
        public VersionStatusEnum VersionStatusEnum { get; set; }
        public int? RelatedVersionId { get; set; }
        public RelatedPageEnum RelatedPageEnum { get; set; }
    }
}