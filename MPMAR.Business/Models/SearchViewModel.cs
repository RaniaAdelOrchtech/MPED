using System;
using System.Collections.Generic;
using System.Text;

namespace MPMAR.Business.Models
{
    public class SearchViewModel
    {
        public ICollection<GlobalSearchModel> GlobalSearchModels { get; set; }
        public long  Count { get; set; }

    }
}
