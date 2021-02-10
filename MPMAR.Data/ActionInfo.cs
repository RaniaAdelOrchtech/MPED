using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MPMAR.Data
{
    /// <summary>
    /// Class for ActionInfo which help classes to inhirit from this class to fill mendatory data
    /// </summary>
    public class ActionInfo
    {
        public DateTime CreationDate { get; set; }

        [MaxLength(450)]
        public string CreatedById { get; set; }
        public ApplicationUser CreatedBy { get; set; }

        public DateTime? ModificationDate { get; set; }

        [MaxLength(450)]
        public string ModifiedById { get; set; }
        public ApplicationUser ModifiedBy { get; set; }

        public DateTime? ApprovalDate { get; set; }

        [MaxLength(450)]
        public string ApprovedById { get; set; }
        public ApplicationUser ApprovedBy { get; set; }
    }
}
