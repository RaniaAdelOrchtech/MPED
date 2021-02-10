using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MPMAR.Business;
using MPMAR.Common;
using MPMAR.Common.Interfaces;
using MPMAR.Data;
using MPMAR.Web.Admin.Helpers;

namespace MPMAR.Web.Admin.Controllers
{
    [Auth2FactorFilter]
    public class LogsController : Controller
    {
        private readonly ILogRepository _logRepository;
        private readonly IEventLogger<LogSessionActivityAttribute> _eventLogger;
        public LogsController(ILogRepository logRepository,IEventLogger<LogSessionActivityAttribute> eventLogger)
        {
            this._eventLogger = eventLogger;
            _logRepository = logRepository;
        }
        
        /// <summary>
        /// Index for griding all log objects
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public IActionResult Index()
        {
           
            return View();
        }

        /// <summary>
        /// Get all log objects for index
        /// </summary>
        /// <returns></returns>
         [Authorize]
        public JsonResult GetLogsData()
        {
            var log = _logRepository.Get();
            return Json(new { data = log });
        }

        /// <summary>
        /// Get all log objects for index with server side
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public IActionResult GetLogData()
        {
            //serverSideParams
            int start = Convert.ToInt32(Request.Form["start"]);
            int lenght = Convert.ToInt32(Request.Form["length"]);
            string searchValue = Request.Form["search[value]"];
            string sortColumnName = Request.Form["columns[" + Request.Form["order[0][column]"] + "][name]"];
            string sortDirection = Request.Form["order[0][dir]"];
            int totalCount = 0;
            var governorates = _logRepository.GetAll(searchValue, sortColumnName, sortDirection, start, lenght, out totalCount);
            int totalRawsAfterFiltering = governorates.Count();
            //sorting
            return Json(new
            {
                data = governorates,
                draw = Request.Form["draw"],
                recordsTotal = totalCount,
                recordsFiltered = totalCount
            });
        }

    }
}