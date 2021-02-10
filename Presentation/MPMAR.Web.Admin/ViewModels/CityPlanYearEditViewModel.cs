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
    public class CityPlanYearEditViewModel : ActionInfo
    {
        public int Id { get; set; }
        public int CityPlanId { get; set; }
        public string GovName { get; set; }
        public string GovYear { get; set; }
        public bool IsMapActive { get; set; }
        public string EnFileUrl { get; set; }
        public IFormFile EnFile { get; set; }
        public string ArFileUrl { get; set; }
        public IFormFile ArFile { get; set; }
        public int? StatusId { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public ChangeActionEnum? ChangeActionEnum { get; set; }
        public VersionStatusEnum? VersionStatusEnum { get; set; }
        public int? CityPlanYearId { get; set; }
        public int? DFGovId { get; set; }
    }


    public class CityPlanYearListViewModel : ActionInfo
    {
        public int Id { get; set; }
        public int CityPlanId { get; set; }
        public string GovName { get; set; }
        public string GovYear { get; set; }
        public bool IsMapActive { get; set; }
        public string EnFileUrl { get; set; }
        public string ArFileUrl { get; set; }
        public int? StatusId { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

    }
  
}
