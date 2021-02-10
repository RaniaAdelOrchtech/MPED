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
    public interface IDFIndicatorRepository
    {
        /// <summary>
        /// get df indicator by id
        /// </summary>
        /// <param name="id">df indicator id</param>
        /// <returns></returns>
        DFIndicator GetByID(int id);
    }
}
