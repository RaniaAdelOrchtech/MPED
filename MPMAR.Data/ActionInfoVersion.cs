using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MPMAR.Data
{
    /// <summary>
    /// Class for ActionInfoVersion
    /// </summary>
    public class ActionInfoVersion

    { 

        public DateTime CreationDate { get; set; }

        [MaxLength(450)]
        public string CreatedById { get; set; }
        public ApplicationUser CreatedBy { get; set; }

        public DateTime? ApprovalDate { get; set; }

        [MaxLength(450)]
        public string ApprovedById { get; set; }
        public ApplicationUser ApprovedBy { get; set; }
    }
}
