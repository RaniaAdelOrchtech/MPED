using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MPMAR.Analytics.Data;

namespace MPMAR.Business.Interfaces
{
    public interface IGrossDomesticRepository
    {
        /// <summary>
        ///  get report data
        /// </summary>
        /// <param name="sheetNum"></param>
        /// <param name="lang"></param>
        /// <param name="years">years ids</param>
        /// <param name="quarters">quarters ids</param>
        /// <param name="prices">prices ids</param>
        /// <param name="sectors">sectors ids</param>
        /// <param name="headers"></param>
        /// <returns></returns>
        ReportViewModel GetReport(int sheetNum, string lang, int[] years, int[] quarters, string[] prices
         , int[] sectors, List<string> headers);

        /// <summary>
        /// get pie chart data
        /// </summary>
        /// <param name="sheetNum"></param>
        /// <param name="lang"></param>
        /// <param name="years">years ids</param>
        /// <param name="quarters">quarters ids</param>
        /// <param name="prices">prices ids</param>
        /// <param name="sectors">sectors ids</param>
        /// <param name="headers"></param>
        /// <returns></returns>
        List<ChartsViewModel.PieViewModel> GetPieReport(int sheetNum, string lang, int[] years, int[] quarters, string[] prices
       , int[] sectors, List<string> headers);
        /// <summary>
        /// get line and bar chart data
        /// </summary>
        /// <param name="sheetNum"></param>
        /// <param name="lang"></param>
        /// <param name="years">years ids</param>
        /// <param name="quarters">quarters ids</param>
        /// <param name="prices">prices ids</param>
        /// <param name="sectors">sectors ids</param>
        /// <param name="headers"></param>
        /// <returns></returns>
        ChartsViewModel.ReportViewModel GetChartReport(int sheetNum, string lang, int[] years, int[] quarters, string[] prices
       , int[] sectors, List<string> headers, string chartType = "");

      //  List<ExcelSheetModel> GetExcelReport(int GDPType, string lang, int[] years, int[] quarters, string[] prices
      //, bool RGDP, bool RGDP1617, bool sectorRGDP, int[] sectors, List<string> headers);


    }
}
