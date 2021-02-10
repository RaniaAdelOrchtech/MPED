using MPMAR.Analytics.Data;
using MPMAR.Analytics.Data.Models;
using MPMAR.Business.Services.Analytics.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPMAR.Business.Interfaces.Analytics
{
    public interface IActivityCurrentRepository
    {
        /// <summary>
        /// add new activity current
        /// </summary>
        /// <param name="activityConstant"></param>
        void Add(ActivityCurrent activityCurrent);
        /// <summary>
        /// get all activity current in some range
        /// </summary>
        /// <param name="searchValue"></param>
        /// <param name="sortColumnName">coulmn name to sort data depend on it</param>
        /// <param name="sortDirection">descending(desc) or ascending(asc)</param>
        /// <param name="start">strarting page</param>
        /// <param name="lenght">number of rows in the page</param>
        /// <param name="totalCount">total rows count</param>
        /// <returns>IEnumerable of activity view model</returns>
        IEnumerable<ActivityVM> GetAll(string searchValue, string sortColumnName, string sortDirection, int start, int lenght, out int totalCount, string role = "");
        /// <summary>
        /// get specific current activity by id
        /// </summary>
        /// <param name="id">activity id</param>
        /// <returns></returns>
        ActivityCurrent GetById(int id);
        /// <summary>
        /// delete current activity by id
        /// </summary>
        /// <param name="id">activity id</param>
        /// <returns>true if deleted successfully false otherwise</returns>
        bool Delete(int id);
        /// <summary>
        /// update current activity
        /// </summary>
        /// <param name="activityConstant"></param>
        void Update(ActivityCurrent activityCurrent);
        void AddVer(ActivityCurrentVersion activityCurrentVersion);
        ActivityCurrentVersion GetVerById(int id, bool disableTracking = true);
        ActivityCurrentVersion GetByActivityCurrentId(int v);
        void UpdateVer(ActivityCurrentVersion rgdpVersionModel);
        IEnumerable<ActivityCurrentVersion> GetAllSubmited();
    }
}
