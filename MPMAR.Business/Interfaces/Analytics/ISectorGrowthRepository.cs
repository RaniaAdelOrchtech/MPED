using MPMAR.Analytics.Data;
using MPMAR.Analytics.Data.Models;
using MPMAR.Business.Services.Analytics.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPMAR.Business.Interfaces.Analytics
{
    public interface ISectorGrowthRepository
    {
        /// <summary>
        ///add new  SectorGrowthRate
        /// </summary>
        /// <param name="sectorGrowthRate"></param>
        void Add(SectorGrowthRate sectorGrowthRate);
        /// <summary>
        /// get all SectorGrowthRate in some range
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
        /// get specific SectorGrowthRate by id
        /// </summary>
        /// <param name="id">SectorGrowthRate id</param>
        /// <returns></returns>
        SectorGrowthRate GetById(int id);
        /// <summary>
        /// delete SectorGrowthRate by id
        /// </summary>
        /// <param name="id">SectorGrowthRate id</param>
        /// <returns>true if deleted successfully false otherwise</returns>
        bool Delete(int id);
        /// <summary>
        /// update SectorGrowthRate
        /// </summary>
        /// <param name="activityConstant"></param>
        void Update(SectorGrowthRate sectorGrowthRate);
        void AddVer(SectorGrowthRateVersion sectorGrowthRate);
        SectorGrowthRateVersion GetVerById(int id, bool disableTracking = true);
        SectorGrowthRateVersion GetBySRGDPId(int v, bool disableTracking = true);
        void UpdateVer(SectorGrowthRateVersion rgdpVersionModel);
        IEnumerable<SectorGrowthRateVersion> GetAllSubmited();
    }
}
