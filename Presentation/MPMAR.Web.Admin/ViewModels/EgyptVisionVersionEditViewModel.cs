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
    public class EgyptVisionVersionEditViewModel : PageSeo
    {
        public int Id { get; set; }
        public int PageRouteVersionId { get; set; }
        public string EnEgyptVisionName { get; set; }
        public string ArEgyptVisionName { get; set; }
        public string BgColor { get; set; }
        public string LineColor { get; set; }

   
        public string EnEgyptVisionSmallDesc { get; set; }
       

        public string ArEgyptVisionSmallDesc { get; set; }

        public string EnEgyptVisionDesc { get; set; }
        public string ArEgyptVisionDesc { get; set; }
        public int? StatusId { get; set; }

        public IFormFile EnImageFile { get; set; }
        public string EnImagePath { get; set; }

        public IFormFile ArImageFile { get; set; }
        public string ArImagePath { get; set; }

        public bool ImagePositionIsRight { get; set; }
        
        public int? Order { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        public ChangeActionEnum? ChangeActionEnum { get; set; }
        public VersionStatusEnum? VersionStatusEnum { get; set; }
        public int? EgyptVisionId { get; set; }

    }


    public class EgyptVisionVersionIndexViewModel : PageSeo
    {
        public int Id { get; set; }
        public string EnEgyptVisionName { get; set; }
        public string ArEgyptVisionName { get; set; }
        public string EnEgyptVisionSmallDesc { get; set; }
        public string ArEgyptVisionSmallDesc { get; set; }
        public string BgColor { get; set; }
        public string LineColor { get; set; }
        public int? StatusId { get; set; }
        public string EnImagePath  { get; set; }
        public string ArImagePath { get; set; }
        public bool ImagePositionIsRight { get; set; }
         public int? Order { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        public ChangeActionEnum? ChangeActionEnum { get; set; }
        public VersionStatusEnum? VersionStatusEnum { get; set; }
        public int? EgyptVisionId { get; set; }
    }
}
