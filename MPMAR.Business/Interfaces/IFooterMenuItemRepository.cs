using MPMAR.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPMAR.Business.Interfaces
{
    public interface IFooterMenuItemRepository 
    {
        /// <summary>
        /// Add fotter menu item object to database
        /// </summary>
        /// <param name="footerMenuItem">footer menu item data</param>
        /// <returns></returns>
        FooterMenuItem Add(FooterMenuItem footerMenuItem);
        
        /// <summary>
        /// Update footer menu item object from database
        /// </summary>
        /// <param name="footerMenuItem">footer menu item new data</param>
        /// <returns></returns>
        FooterMenuItem Update(FooterMenuItem footerMenuItem);

        /// <summary>
        /// Get all footer menu items
        /// </summary>
        /// <returns></returns>
        IEnumerable<FooterMenuItem> GetFooterMenuItemId();

        /// <summary>
        /// Delete footer menu item object
        /// </summary>
        /// <param name="id">footer menu item id</param>
        /// <returns></returns>
        bool Delete(int id);

        /// <summary>
        /// Get footer menu item object
        /// </summary>
        /// <param name="id">footer menu item id</param>
        /// <returns></returns>
        FooterMenuItem Get(int id);

        /// <summary>
        /// Get footer menu item object
        /// </summary>
        /// <param name="id">footer menu item id</param>
        /// <returns></returns>
        FooterMenuItem GetDetail(int id);

        /// <summary>
        /// Get footer menu item object with no tracking for id
        /// </summary>
        /// <param name="id">footer menu item id</param>
        /// <returns></returns>
        FooterMenuItem GetByIdWithNoTracking(int id);
    }
}
