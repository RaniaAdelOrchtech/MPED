using MPMAR.Analytics.Data;
using MPMAR.Analytics.Data.Models;
using MPMAR.Business.Services.Analytics.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MPMAR.Business.Interfaces
{
    public interface IGovernorateRepository
    {
        /// <summary>
        /// get all GovernoratesViewModel in some range
        /// </summary>
        /// <param name="searchValue"></param>
        /// <param name="sortColumnName"></param>
        /// <param name="sortDirection"></param>
        /// <param name="start"></param>
        /// <param name="lenght"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        List<GovernoratesViewModel> GetAll(string searchValue, string sortColumnName, string sortDirection,int start,int lenght,out int totalCount,string role="");
        /// <summary>
        /// get Governorate by condition
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        IEnumerable<Governorate> GetByCondition(Expression<Func<Governorate, bool>> expression);
        /// <summary>
        /// get count of Governorate
        /// </summary>
        /// <returns></returns>
        int GetCount();
        /// <summary>
        /// add new Governorate
        /// </summary>
        /// <param name="governorate"></param>
        void Add(Governorate governorate);
        /// <summary>
        /// get report data 
        /// </summary>
        /// <param name="regionsandgov"></param>
        /// <param name="years"></param>
        /// <param name="activities"></param>
        /// <param name="lang"></param>
        /// <returns></returns>
        GovernorateViewModel GetFilterdGovernoratesForGrid(int[] regionsandgov, int[] years, int[] activities, string lang);
        /// <summary>
        /// get line chart data 
        /// </summary>
        /// <param name="regions"></param>
        /// <param name="governorates"></param>
        /// <param name="years"></param>
        /// <param name="lang"></param>
        /// <returns></returns>
        LineGovModelParent GetFilterForLineChart(int[] regions, int[] governorates, int[] years, int[] activities, string lang);
        /// <summary>
        /// get bar chart data 
        /// </summary>
        /// <param name="regions"></param>
        /// <param name="governorates"></param>
        /// <param name="years"></param>
        /// <param name="activities"></param>
        /// <param name="lang"></param>
        /// <returns></returns>
        BarGovModelParent GetFilterForBarChart(int[] regions, int[] governorates, int[] years, int[] activities, string lang);
        /// <summary>
        /// get pie chart data
        /// </summary>
        /// <param name="regions"></param>
        /// <param name="governorates"></param>
        /// <param name="year"></param>
        /// <param name="lang"></param>
        /// <returns></returns>
        List<GovPieModel> GetFilterForPieChart(int[] regions, int[] governorates, int year, int[] activities, string lang);
        /// <summary>
        /// get Governorate by id
        /// </summary>
        /// <param name="id">Governorate id</param>
        /// <returns></returns>
        Governorate findById(int id);
        /// <summary>
        /// update Governorate
        /// </summary>
        /// <param name="governorate"></param>
        public void Update(Governorate governorate);
        /// <summary>
        /// delete Governorate by id
        /// </summary>
        /// <param name="id">Governorate id</param>
        /// <returns></returns>
        public bool SoftDelete(int id);
        void AddVer(GovernorateVersion governorate);
        GovernorateVersion GetVerById(int id, bool disableTracking = true);
        GovernorateVersion GetByGovId(int v);
        void UpdateVer(GovernorateVersion govVersionModel);
        IEnumerable<GovernorateVersion> GetAllSubmited();
    }
}
