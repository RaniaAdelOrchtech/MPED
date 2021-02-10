using Microsoft.AspNetCore.Mvc.Rendering;
using MPMAR.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MPMAR.Web.Admin.ViewModels
{
    public class PageNewsTypeEditViewModel
    {
        
        public PageNewsTypeEditViewModel(NewsTypeViewModel newsTypes)
        {
            NewsType = newsTypes;
        }
        public PageNewsTypeEditViewModel()
        {

        }
        public NewsTypeViewModel NewsType { get; set; }


    }
}
