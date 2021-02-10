using MPMAR.Analytics.Data;
using MPMAR.Analytics.Data.Models;
using MPMAR.Business.Services.Analytics.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPMAR.Business.Interfaces.Analytics
{
    public interface IComponentConstantRepository
    {
        //IEnumerable<ComponentViewModel> GetAll();

        /// <summary>
        /// get all component constant in some range
        /// </summary>
        /// <param name="searchValue"></param>
        /// <param name="sortColumnName">coulmn name to sort data depend on it</param>
        /// <param name="sortDirection">descending(desc) or ascending(asc)</param>
        /// <param name="start">strarting page</param>
        /// <param name="lenght">number of rows in the page</param>
        /// <param name="totalCount">total rows count</param>
        /// <returns>IEnumerable of component view model</returns>
        IEnumerable<ComponentViewModel> GetAll(string searchValue, string sortColumnName, string sortDirection, int start, int lenght, out int totalCount, string role = "");

        /// <summary>
        /// get specific component const by id
        /// </summary>
        /// <param name="id">component id</param>
        /// <returns></returns>
        ComponentConstant GetById(int id);

        /// <summary>
        /// delete constant component by id
        /// </summary>
        /// <param name="id">component id</param>
        /// <returns>true if deleted successfully false otherwise</returns>
        bool Delete(int id);

        /// <summary>
        /// add new component constatnt
        /// </summary>
        /// <param name="componentConstant"></param>
        void Add(ComponentConstant componentConstant);

        /// <summary>
        /// add new component constatnt version
        /// </summary>
        /// <param name="componentConstant"></param>
        void AddVer(ComponentConstantVersion componentConstantVersion);

        /// <summary>
        /// update constant component
        /// </summary>
        /// <param name="componentConstant"></param>
        void Update(ComponentConstant componentConstant);
        ComponentConstantVersion GetVerById(int id, bool disableTracking = true);
        void UpdateVer(ComponentConstantVersion componentConstantVersion);
        ComponentConstantVersion GetByComponentConstId(int v);
        IEnumerable<ComponentConstantVersion> GetAllSubmited();
    }
}
