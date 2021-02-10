using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPMAR.Analytics.Data
{
    public class InvestmentViewModel: InvestmentCommonViewModel
    {
        public int Id { get; set; }
        public string Indicator { get; set; }
        public string _Source { get; set; }
        public string Unit { get; set; }
        public string _Quarter{ get; set; }
        public string _Year { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsVersion { get; set; }

    }
}
