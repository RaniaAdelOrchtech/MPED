using Microsoft.EntityFrameworkCore;
using MPMAR.Analytics.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using MPMAR.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MPMAR.Business.Interfaces
{
    public interface IDFYearsRepository
    {
        /// <summary>
        /// get all df years
        /// </summary>
        /// <returns></returns>
        IEnumerable<DFYear> GetAll();
    }
}
