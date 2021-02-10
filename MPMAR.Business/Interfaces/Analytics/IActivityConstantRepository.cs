using MPMAR.Analytics.Data;
using MPMAR.Business.Services.Analytics.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPMAR.Business.Interfaces.Analytics
{
    public interface IActivityConstantRepository
    {
        /// <summary>
        /// add new activity constatnt
        /// </summary>
        /// <param name="activityConstant"></param>
        void Add(ActivityConstant activityConstant);
        /// <summary>
        /// get all activity constant in some range
        /// </summary>
        /// <param name="searchValue"></param>
        /// <param name="sortColumnName">coulmn name to sort data depend on it</param>
        /// <param name="sortDirection">descending(desc) or ascending(asc)</param>
        /// <param name="start">strarting page</param>
        /// <param name="lenght">number of rows in the page</param>
        /// <param name="totalCount">total rows count</param>
        /// <returns>IEnumerable of activity view model</returns>
        IEnumerable<ActivityVM> GetAll(string searchValue, string sortColumnName, string sortDirection, int start, int lenght, out int totalCount);
        /// <summary>
        /// get specific constant activity by id
        /// </summary>
        /// <param name="id">activity id</param>
        /// <returns></returns>
        ActivityConstant GetById(int id);
        /// <summary>
        /// delete constant activity by id
        /// </summary>
        /// <param name="id">activity id</param>
        /// <returns>true if deleted successfully false otherwise</returns>
        bool Delete(int id);
        /// <summary>
        /// update constant activity
        /// </summary>
        /// <param name="activityConstant"></param>
        void Update(ActivityConstant activityConstant);
    }
}
