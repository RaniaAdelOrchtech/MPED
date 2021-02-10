using MPMAR.Analytics.Data;
using MPMAR.Analytics.Data.Models;
using MPMAR.Business.Services.Analytics.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPMAR.Business.Interfaces.Analytics
{
    public interface IRGDPRepository
    {
        /// <summary>
        /// get all RGDP with pagging options 
        /// </summary>
        /// <param name="searchValue"></param>
        /// <param name="sortColumnName"></param>
        /// <param name="sortDirection"></param>
        /// <param name="start"></param>
        /// <param name="lenght"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        IEnumerable<RGDPViewModel> GetAll(string searchValue, string sortColumnName, string sortDirection, int start, int lenght, out int totalCount, string role = "");
        /// <summary>
        /// get RGDP by id
        /// </summary>
        /// <param name="id">RGDP id</param>
        /// <returns></returns>
        RGDPGrowthRate GetById(int id);
        /// <summary>
        /// delete RGDP 
        /// </summary>
        /// <param name="id">RGDP id</param>
        /// <returns></returns>
        bool Delete(int id);
        /// <summary>
        /// add new RGDP
        /// </summary>
        /// <param name="rgdp"></param>
        void Add(RGDPGrowthRate rgdp);
        /// <summary>
        /// update RGDP
        /// </summary>
        /// <param name="rgdp"></param>
        void Update(RGDPGrowthRate rgdp);
        void AddVer(RGDPGrowthRateVersion rGDPGrowthRate);
        RGDPGrowthRateVersion GetVerById(int id, bool disableTracking = true);
        RGDPGrowthRateVersion GetByRGDPId(int v);
        void UpdateVer(RGDPGrowthRateVersion rgdpVersionModel);
        IEnumerable<RGDPGrowthRateVersion> GetAllSubmited();
    }
}
