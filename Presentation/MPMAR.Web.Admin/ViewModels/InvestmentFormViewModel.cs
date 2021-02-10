using MPMAR.Analytics.Data;
using MPMAR.Analytics.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MPMAR.Web.Admin.ViewModels
{
    public class InvestmentFormViewModel : InvestmentCommonViewModel
    {
        public int Id { get; set; }
        public int DFIndicatorId { get; set; } = (int)DFIndicatorEnum.PublicInvestments;
        public int DFSourceId { get; set; } = (int)DFSourceEnum.MinistryOfPlanning;
        public int DFUnitId { get; set; } = (int)DFUnitEnum.MillionEGP;
        [Required]
        public int DFQuarterId { get; set; }
        [Required]
        public int DFYearId { get; set; }
        public bool? IsDeleted { get; set; } = false;

        public string CreatedById { get; set; }
    }
}
