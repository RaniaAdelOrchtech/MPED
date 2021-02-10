using MPMAR.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPMAR.Business.Interfaces
{
    public interface ILeftMenuItemRepository
    {
        /// <summary>
        /// add new LeftMenuItem
        /// </summary>
        /// <param name="leftMenuItem"></param>
        /// <returns></returns>
        LeftMenuItem Add(LeftMenuItem leftMenuItem);
        /// <summary>
        /// update LeftMenuItem
        /// </summary>
        /// <param name="leftMenuItem"></param>
        /// <returns></returns>
        LeftMenuItem Update(LeftMenuItem leftMenuItem);
        /// <summary>
        /// get LeftMenuItem by id 
        /// </summary>
        /// <param name="leftMenuItemId"></param>
        /// <returns></returns>
        IEnumerable<LeftMenuItem> GetLeftMenuItemId(int leftMenuItemId);
        /// <summary>
        /// delete LeftMenuItem 
        /// </summary>
        /// <param name="id">LeftMenuItem id</param>
        /// <returns></returns>
        LeftMenuItem Delete(int id);
        /// <summary>
        /// get LeftMenuItem by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        LeftMenuItem Get(int id);
        /// <summary>
        /// get LeftMenuItem by id
        /// </summary>
        /// <param name="id">LeftMenuItem id</param>
        /// <returns></returns>
        LeftMenuItem GetDetail(int id);
        /// <summary>
        /// get LeftMenuItemVersions by LeftMenuItem id
        /// </summary>
        /// <param name="id">LeftMenuItem id</param>
        /// <returns></returns>
        LeftMenuItemVersions GetByLeftMenuItemId(int id);
    }
}
