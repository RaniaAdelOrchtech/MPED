using System;
using System.Collections.Generic;
using System.Text;

namespace MPMAR.Analytics.Data
{
    public class GovernorateViewModel
    {
        public List<string> HeaderColumns { get; set; }
        public List<List<object>> Result { get; set; }

        public GovernorateViewModel(List<string> headerColumns, List<List<object>> result)
        {
            HeaderColumns = headerColumns;
            Result = result;
        }
    }
    public class GovModel
    {
        public List<string> HeaderColumns { get; set; }
        public List<List<object>> Result { get; set; }

        public GovModel(List<string> headerColumns, List<List<object>> result)
        {
            HeaderColumns = headerColumns;
            Result = result;
        }
    }
    public class LatestGovModel
    {
        public string name { get; set; }
        public List<object> data { get; set; }
        public LatestGovModel()
        {

        }

        public LatestGovModel(string columnName, List<object> columnData)
        {
            name = columnName;
            data = columnData;
        }
    }
    public class LineGovModel
    {
        public string name { get; set; }
        public List<object> data { get; set; }
        public LineGovModel()
        {

        }
        public LineGovModel(string columnName, List<object> columnData)
        {
            name = columnName;
            data = columnData;
        }
    }
    public class LineGovModelParent
    {
        public List<LineGovModel> Data { get; set; }
        public List<string> Years { get; set; }
    }
    public class BarGovModelParent
    {
        public List<LatestGovModel> Data { get; set; }
        public List<string> Years { get; set; }
    }
    public class GovPieModel
    {
        public string Name { get; set; }
        public decimal Y { get; set; }

        public GovPieModel(string name, decimal y)
        {
            Name = name;
            Y = y;
        }
    }
}
