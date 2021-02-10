using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPMAR.Business.ViewModels
{
    public class PageSectionListViewModel
    {
        public int Id { get; set; }
        public string SectionType { get; set; }
        public string SectionTitle { get; set; }
        public string SectionDescription { get; set; }
        public bool HasCards { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int PageSectionTypeId { get; set; }
        public int PageRouteVersionId { get; set; }
    }
}
