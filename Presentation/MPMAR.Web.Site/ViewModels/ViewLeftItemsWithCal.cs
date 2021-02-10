using MPMAR.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MPMAR.Web.Site.ViewModels
{
    public class ViewLeftItemsWithCal 
    {
       public List<LeftMenuItem> LeftMenuItem { get; set; }

        public List<PageEventVersions> PageEventVersions { get; set; }
    }
   
}
