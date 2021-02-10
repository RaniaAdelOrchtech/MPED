using MPMAR.Analytics.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPMAR.Business.Services.Analytics.ViewModels
{
    public class RGDPViewModel
    {
        public int Id { get; set; }
        public string Indicator { get; set; }
        public string Source { get; set; }
        public string Unit { get; set; }
        public string Quarter { get; set; }
        public string YearFiscal { get; set; }
        public double? Value { get; set; }
        public bool IsDeleted { get; set; }
        public ChangeActionEIEnum? ChangeActionEnum { get; set; }
        public VersionStatusEIEnum? VersionStatusEnum { get; set; }
        public int? RGDPId { get; set; }
        public bool IsVersion { get; set; }
        public string CreatedById { get; set; }

    }
}
