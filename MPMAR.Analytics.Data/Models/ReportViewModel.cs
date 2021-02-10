using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPMAR.Analytics.Data
{
    public class ReportViewModel
    {
        public List<string> HeaderColumns { get; set; }
        public List<List<object>> Result { get; set; }

        public ReportViewModel(List<string> headerColumns, List<List<object>> result)
        {
            HeaderColumns = headerColumns;
            Result = result;
        }
    }
}
