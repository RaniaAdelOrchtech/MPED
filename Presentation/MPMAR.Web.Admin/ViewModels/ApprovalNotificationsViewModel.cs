using MPMAR.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPMAR.Web.Admin.ViewModels
{
    public class ApprovalNotificationsViewModel
    {
        public int Id { get; set; }
        public string ContentManagerName { get; set; }
        public string ChangesDate { get; set; }
        public string ChangesTime { get; set; }
        public string PageName { get; set; }
        public string PageLink { get; set; }
        public string ChangeAction { get; set; }
        public string ChangeType { get; set; }
        public string PageType { get; set; }
        public string VersionStatusEnum { get; set; }
    }
}
