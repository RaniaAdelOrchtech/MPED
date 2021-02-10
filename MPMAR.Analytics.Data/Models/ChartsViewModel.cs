using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPMAR.Analytics.Data
{
    public class ChartsViewModel
    {
        public class PieViewModel
        {
            public string Name { get; set; }
            public double Y { get; set; }

            public PieViewModel(string name, double y)
            {
                Name = name;
                Y = y;
            }
        }

        public class ReportViewModel
        {
            public List<ReportModel> Data { get; set; }
            public List<string> Years { get; set; }
            public ReportViewModel()
            {
                Years = new List<string>();
            }
        }

        public class ReportModel
        {
            public string name { get; set; }
            public List<object> data { get; set; }
            public ReportModel()
            {
                data = new List<object>();
            }
            public ReportModel(string columnName, List<object> columnData)
            {
                name = columnName;
                data = columnData;
            }
        }

    
    }
}
