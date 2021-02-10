using MPMAR.Analytics.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MPMAR.Analytics.Data.Models
{
    public class RGDPGrowthRateVersion
    {
        public int Id { get; set; }
        [ForeignKey("DFIndicator")]
        public int DFIndicatorId { get; set; }
        [ForeignKey("DFSource")]
        public int DFSourceId { get; set; }
        [ForeignKey("DFUnit")]
        public int DFUnitId { get; set; }
        [ForeignKey("DFQuarter")]
        public int DFQuarterId { get; set; }
        [ForeignKey("DFYear")]
        public int DFYearId { get; set; }
        public double? GrowthRate { get; set; }
        public bool? IsDeleted { get; set; }


        public virtual DFIndicator DFIndicator { get; set; }
        public virtual DFSource DFSource { get; set; }
        public virtual DFUnit DFUnit { get; set; }
        public virtual DFQuarter DFQuarter { get; set; }
        public virtual DFYear DFYear { get; set; }

        public ChangeActionEIEnum? ChangeActionEnum { get; set; }
        public VersionStatusEIEnum? VersionStatusEnum { get; set; }
        public int? RGDPGrowthRateId { get; set; }
        public RGDPGrowthRate RGDPGrowthRate { get; set; }
        public DateTime? CreationDate { get; set; } = DateTime.Now;
        public string CreatedById { get; set; }
    }
}
