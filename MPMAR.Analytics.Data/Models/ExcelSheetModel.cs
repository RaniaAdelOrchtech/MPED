using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPMAR.Analytics.Data
{
    public class ExcelSheetModel
    {
        public string Name { get; set; }
        public List<string> Columns { get; set; }
        public List<List<object>> Data { get; set; }

        public ExcelSheetModel()
        {
            Data = new List<List<object>>();
        }
    }
}
