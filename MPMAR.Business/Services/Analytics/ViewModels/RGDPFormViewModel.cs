using MPMAR.Analytics.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPMAR.Business.Services.Analytics.ViewModels
{
    public class RGDPFormViewModel
    {
        public int Id { get; set; }
        public int Indicator { get; set; }
        public int Source { get; set; } = (int)DFSourceEnum.MinistryOfPlanning;
        public int Unit { get; set; }
        public int DFQuarterId { get; set; }
        public int DFYearFiscalId { get; set; }
        public double? Value { get; set; }
        public bool? IsDeleted { get; set; }
        public ChangeActionEIEnum? ChangeActionEnum { get; set; }
        public VersionStatusEIEnum? VersionStatusEnum { get; set; }
        public int? RGDPId { get; set; }
        public string CreatedById { get; set; }
    }
}
