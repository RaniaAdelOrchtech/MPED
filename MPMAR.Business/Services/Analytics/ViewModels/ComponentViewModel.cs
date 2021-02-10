using MPMAR.Analytics.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MPMAR.Business.Services.Analytics.ViewModels
{
    public class ComponentViewModel : ComponentCommonViewModel
    {
        public string Indicator { get; set; }
        public string Source { get; set; }
        public string Unit { get; set; }
        public string Quarter { get; set; }
        public string YearBase { get; set; }
        public string YearFiscal { get; set; }
        public bool IsDeleted { get; set; }
        public int? ComponentConstantId { get; set; }
    }
}
