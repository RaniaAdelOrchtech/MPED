using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPMAR.Analytics.Data
{
    public class RegionGovView
    {
        public int id { get; set; }
        public string title { get; set; }
        public List<RegionGovView> subs { get; set; }
    }
    public class RegionGovViewSubs
    {
        public int id { get; set; }
        public string title { get; set; }
    }
    public class ColumnView
    {
        public int id { get; set; }
        public string title { get; set; }
    }
    public class ColumnViewTree
    {
        public int id { get; set; }
        public string title { get; set; }
        public List<ColumnView> subs { get; set; }
    }
}
