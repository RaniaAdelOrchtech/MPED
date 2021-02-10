using MPMAR.Analytics.Data;
using MPMAR.Business.Services.Analytics.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPMAR.Business.Interfaces.Analytics
{
   public interface IComponentCurrenttRepository
    {
        //IEnumerable<ComponentViewModel> GetAll();

        /// <summary>
        /// get all component current in some range
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
        /// get specific component current by id
        /// </summary>
        /// <param name="id">component id</param>
        /// <returns></returns>
        ComponentCurrent GetById(int id);

        /// <summary>
        /// delete current component by id
        /// </summary>
        /// <param name="id">component id</param>
        /// <returns>true if deleted successfully false otherwise</returns>
        bool Delete(int id);

        /// <summary>
        /// add new component current
        /// </summary>
        /// <param name="componentCurrent"></param>
        void Add(ComponentCurrent componentCurrent);

        /// <summary>
        /// update current component
        /// </summary>
        /// <param name="componentCurrent"></param>
        void Update(ComponentCurrent componentCurrent);
        void AddVer(ComponentCurrentVersion componentCurrentVersion);
        ComponentCurrentVersion GetVerById(int id, bool disableTracking = true);
        ComponentCurrentVersion GetByComponentCurrentId(int v);
        void UpdateVer(ComponentCurrentVersion componentCurrentVersionModel);
        IEnumerable<ComponentCurrentVersion> GetAllSubmited();
    }
}
