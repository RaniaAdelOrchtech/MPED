using MPMAR.Data;
using MPMAR.Data.HomePageModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPMAR.Business.Interfaces
{
    public interface ILeftMenuItemsVersionsRepository
    {
        /// <summary>
        /// add new LeftMenuItemVersions
        /// </summary>
        /// <param name="model"></param>
        void Add(LeftMenuItemVersions model);
        /// <summary>
        /// update LeftMenuItemVersions
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool Update(LeftMenuItemVersions model);
        /// <summary>
        /// get list of LeftMenuItemVersions
        /// </summary>
        /// <returns></returns>
        List<LeftMenuItemVersions> GetLeftMenuItemVersions();
        /// <summary>
        /// get LeftMenuItemVersions by id
        /// </summary>
        /// <param name="id">LeftMenuItemVersions id</param>
        /// <returns></returns>
        LeftMenuItemVersions Get(int id);
        /// <summary>
        /// get LeftMenuItemVersions by LeftMenuItem id
        /// </summary>
        /// <param name="id">LeftMenuItem id</param>
        /// <returns></returns>
        LeftMenuItemVersions GetByLeftMenuItemId(int id);
        /// <summary>
        /// get list of drafts LeftMenuItemVersions
        /// </summary>
        /// <returns></returns>
        IEnumerable<LeftMenuItemVersions> GetAllDrafts();
        /// <summary>
        /// get list of submited LeftMenuItemVersions
        /// </summary>
        /// <returns></returns>
        IEnumerable<LeftMenuItemVersions> GetAllSubmitted();
    }
}
