using MPMAR.Analytics.Data;
using MPMAR.Business.Services.Analytics.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPMAR.Business.Interfaces.Analytics
{
  public  interface IRGDP1617Repository
    {
        /// <summary>
        /// get all RGDP1617 with pagging options
        /// </summary>
        /// <param name="searchValue"></param>
        /// <param name="sortColumnName"></param>
        /// <param name="sortDirection"></param>
        /// <param name="start"></param>
        /// <param name="lenght"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        IEnumerable<RGDPViewModel> GetAll(string searchValue, string sortColumnName, string sortDirection, int start, int lenght, out int totalCount);
        /// <summary>
        /// get RGDP1617 
        /// </summary>
        /// <param name="id">RGDP1617 id</param>
        /// <returns></returns>
        RGDPGrowthRate1617 GetById(int id);
        /// <summary>
        /// delet RGDP1617
        /// </summary>
        /// <param name="id">RGDP1617 id</param>
        /// <returns></returns>
        bool Delete(int id);
        /// <summary>
        /// add new RGDP1617
        /// </summary>
        /// <param name="rgdp"></param>
        void Add(RGDPGrowthRate1617 rgdp);
        /// <summary>
        /// update RGDP1617
        /// </summary>
        /// <param name="rgdp"></param>
        void Update(RGDPGrowthRate1617 rgdp);
    }
}
