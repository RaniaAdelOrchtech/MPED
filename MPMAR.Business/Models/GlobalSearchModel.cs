using Nest;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPMAR.Business.Models
{
    public class GlobalSearchModel
    {
        public int Id { get; set; }
        public PageNameEnum PageEnum { get; set; }
        public string ImagePath { get; set; }
        public string ArTitle { get; set; }
        public string EnTitle { get; set; }
        [Nested(IncludeInParent = true)]
        public List<GlobalSearchContentModel> GlobalSearchContentModels { get; set; }
        public string URL { get; set; }
        public int Order { get; set; }
    }
}
