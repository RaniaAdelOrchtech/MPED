using Microsoft.AspNetCore.Mvc.Rendering;
using MPMAR.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MPMAR.Web.Admin.ViewModels
{
    public class PageNewsTypeCreateViewModel
    {
        public PageNewsTypeCreateViewModel()
        {

        }
        public NewsTypeViewModel NewsType { get; set; }
       

    }
}
