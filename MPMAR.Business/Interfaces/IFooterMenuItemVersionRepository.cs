using MPMAR.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPMAR.Business.Interfaces
{
   public interface IFooterMenuItemVersionRepository
    {
        /// <summary>
        /// Add footer menu item version object to database
        /// </summary>
        /// <param name="footerMenuItem">footer menu item version data</param>
        /// <returns></returns>
        FooterMenuItemVersion Add(FooterMenuItemVersion footerMenuItem);

        /// <summary>
        /// update footer menu item version object from database
        /// </summary>
        /// <param name="footerMenuItem">footer menu item version data</param>
        /// <returns></returns>
        FooterMenuItemVersion Update(FooterMenuItemVersion footerMenuItem);

        /// <summary>
        /// Get all footer menu item versions objects
        /// </summary>
        /// <returns></returns>
        IEnumerable<FooterMenuItemVersion> GetFooterMenuItemId();

        /// <summary>
        /// delete footer menu item version
        /// </summary>
        /// <param name="id">footer menu item version id</param>
        /// <returns></returns>
        bool Delete(int id);

        /// <summary>
        /// Get footer menu item version object
        /// </summary>
        /// <param name="id">footer menu item version id</param>
        /// <returns></returns>
        FooterMenuItemVersion Get(int id);

        /// <summary>
        /// Get footer menu item version object
        /// </summary>
        /// <param name="id">footer menu item version id</param>
        /// <returns></returns>
        FooterMenuItemVersion GetDetail(int id);

        /// <summary>
        /// Get all drafts
        /// </summary>
        /// <returns></returns>
        IEnumerable<FooterMenuItemVersion> GetAllDrafts();

        /// <summary>
        /// Get all submitted
        /// </summary>
        /// <returns></returns>
        IEnumerable<FooterMenuItemVersion> GetAllSubmitted();

        /// <summary>
        /// get footer menu item version which contain item id sent in parameter
        /// </summary>
        /// <param name="itemId">item id</param>
        /// <returns></returns>
        FooterMenuItemVersion GetByItemId(int itemId);
    }
}
