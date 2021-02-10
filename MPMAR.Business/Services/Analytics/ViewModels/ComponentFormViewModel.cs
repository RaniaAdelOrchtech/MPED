using MPMAR.Analytics.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPMAR.Business.Services.Analytics.ViewModels
{
   public class ComponentFormViewModel: ComponentCommonViewModel
    {
        public int DFIndicatorId { get; set; }
        public int DFSourceId { get; set; } = (int)DFSourceEnum.MinistryOfPlanning;
        public int DFUnitId { get; set; } = (int)DFUnitEnum.BillionEGP;
        public int DFQuarterId { get; set; }
        public int DFYearBaseId { get; set; }
        public int DFYearFiscalId { get; set; }
        public bool? IsDeleted { get; set; } = false;
        public int? ComponentId { get; set; }



    }
}
