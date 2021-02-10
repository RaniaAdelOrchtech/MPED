using MPMAR.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPMAR.Business.ViewModels
{
    public class PageNewsListViewModel
    {
        public int Id { get; set; }
        public string EnTitle { get; set; }
        public string EnglishDescription { get; set; }
        public string EnglishShortDescription { get; set; }
        public string ArTitle { get; set; }
        public string arabicShortDescription { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public VersionStatusEnum VersionStatusEnum { get; set; }
        public ChangeActionEnum ChangeActionEnum { get; set; }
        public int VerId { get; internal set; }
        public string StatusName { get { return VersionStatusEnum.ToString(); } }
        public DateTime? Date { get; set; }
        public string CreatedById { get; set; }

    }
}
