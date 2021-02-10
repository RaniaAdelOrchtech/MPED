using MPMAR.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPMAR.Business.ViewModels
{
    public class PageRouteListViewModel
    {
        public int? Id { get; set; }
        public int PageRouteVersionId { get; set; }
        public string EnName { get; set; }
        public string ArName { get; set; }
        public int? Order { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public string NavItemEnName { get; set; }
        public string ControllerName { get; set; }
        public string NavItemArName { get; set; }
        public int? StatusId { get; set; }
        public string StatusName { get { return VersionStatusEnum.ToString(); } }
        public string ContentStatusName { get { return ContentStatusEnum.ToString(); } }
        public string PageType { get; set; }
        public bool HasSections { get; set; }
        public VersionStatusEnum VersionStatusEnum { get; set; }
        public VersionStatusEnum ContentStatusEnum { get; set; }
        public string CreatedById { get; set; }
        public bool IsAvailable { get; set; } = true;
        public bool IsApplyable { get; set; } = true;
        public bool CanViewDP_BI { get; set; } = true;
        public ChangeActionEnum? ChangeActionEnum { get; set; }
    }
}
