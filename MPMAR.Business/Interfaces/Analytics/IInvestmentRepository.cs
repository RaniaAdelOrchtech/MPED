
using MPMAR.Analytics.Data;
using MPMAR.Analytics.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPMAR.Business.Interfaces
{
    public interface IInvestmentRepository
    {
        /// <summary>
        /// get report data
        /// </summary>
        /// <param name="lang"></param>
        /// <param name="years">years ids</param>
        /// <param name="quarters">quarters ids</param>
        /// <param name="headers"></param>
        /// <returns></returns>
        ReportViewModel GetReport(string lang, int[] years, int[] quarters, List<string> headers);
        /// <summary>
        /// get pie charts data
        /// </summary>
        /// <param name="lang"></param>
        /// <param name="years">years ids</param>
        /// <param name="quarters">quarters ids</param>
        /// <param name="headers"></param>
        /// <returns></returns>
        List<ChartsViewModel.PieViewModel> GetPieReport(string lang, int[] years, int[] quarters, List<string> headers);
        /// <summary>
        /// get bar and line chart data
        /// </summary>
        /// <param name="lang"></param>
        /// <param name="years">years ids</param>
        /// <param name="headers"></param>
        /// <returns></returns>
        ChartsViewModel.ReportViewModel GetChartReport(string lang, int[] years, List<string> headers,string chartType);
        /// <summary>
        /// get all not deleted Investments  orderd by years Descending
        /// </summary>
        /// <returns></returns>
        IEnumerable<Investments> GetAll();
        /// <summary>
        /// get all not deleted Investments  orderd by years Descending,
        /// with pagging options
        /// </summary>
        /// <param name="searchValue"></param>
        /// <param name="sortColumnName"></param>
        /// <param name="sortDirection"></param>
        /// <param name="start"></param>
        /// <param name="lenght"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        IEnumerable<InvestmentViewModel> GetAll(string searchValue, string sortColumnName, string sortDirection, int start, int lenght, out int totalCount, string role = "");
        /// <summary>
        /// get Investments by id
        /// </summary>
        /// <param name="id">Investments id</param>
        /// <returns></returns>
        Investments GetById(int id);


        /// <summary>
        /// delete Investments by id
        /// </summary>
        /// <param name="id">Investments id</param>
        /// <returns></returns>
        bool Delete(int id);
        /// <summary>
        /// add new Investments
        /// </summary>
        /// <param name="investments"></param>
        void Add(Investments investments);
        /// <summary>
        /// update Investments
        /// </summary>
        /// <param name="investments"></param>
        void Update(Investments investments);
        void AddVer(InvestmentVersion investments);
        InvestmentVersion GetVerById(int id, bool disableTracking = true);
        InvestmentVersion GetByInvetmentId(int v);
        void UpdateVer(InvestmentVersion invementVersionModel);
        IEnumerable<InvestmentVersion> GetAllSubmited();
    }
}
