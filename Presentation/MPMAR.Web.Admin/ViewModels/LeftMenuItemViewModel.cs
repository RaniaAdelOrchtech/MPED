using Microsoft.AspNetCore.Http;
using MPMAR.Data;
using MPMAR.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MPMAR.Web.Admin.ViewModels
{
    public class LeftMenuItemViewModel : ActionInfoVersion
    {
        public int Id { get; set; }
        public string LeftMenuType { get; set; }
        public string EnTitle { get; set; }
        public string ArTitle { get; set; }
        public string Link { get; set; }
        public int? Order { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public string ImagePath { get; set; }
        public IFormFile ImageFile { get; set; }
        public int? LeftMenuItemId { get; set; }
        public ChangeActionEnum? ChangeActionEnum { get; set; }
        public VersionStatusEnum? VersionStatusEnum { get; set; }

    }
}
