using Microsoft.EntityFrameworkCore;
using MPMAR.Business.Interfaces;
using MPMAR.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static MPMAR.Data.Enums.Enums;

namespace MPMAR.Business.Services
{
    public class FooterMenuItemRepository : IFooterMenuItemRepository
    {
        private readonly ApplicationDbContext _db;

        public FooterMenuItemRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Add fotter menu item object to database
        /// </summary>
        /// <param name="footerMenuItem">footer menu item data</param>
        /// <returns>Added object</returns>
        public FooterMenuItem Add(FooterMenuItem footerMenuItem)
        {
            try
            {
                _db.FooterMenuItem.Add(footerMenuItem);
                _db.SaveChanges();
                //return _db.FooterMenuItem.Include(x => x.PageRouteVersion).FirstOrDefault(c => c.Id == footerMenuItem.Id);
                return _db.FooterMenuItem.FirstOrDefault(c => c.Id == footerMenuItem.Id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Update footer menu item object from database
        /// </summary>
        /// <param name="footerMenuItem">footer menu item new data</param>
        /// <returns>updated object</returns>
        public FooterMenuItem Update(FooterMenuItem footerMenuItem)
        {
            try
            {

                _db.FooterMenuItem.Attach(footerMenuItem);
                _db.Entry(footerMenuItem).State = EntityState.Modified;
                _db.SaveChanges();
                
                return _db.FooterMenuItem.FirstOrDefault(c => c.Id == footerMenuItem.Id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

     

        /// <summary>
        /// Get all footer menu items
        /// </summary>
        /// <returns>Ienumarable of footer menu item objects</returns>
        public IEnumerable<FooterMenuItem> GetFooterMenuItemId()
        {
            var footerMenuItems = _db.FooterMenuItem.OrderBy(s => s.Id).ToList();
            // !(s.IsDeleted && s.PageRouteVersion.StatusId == (int)RequestStatus.Approved) &&
            return footerMenuItems;
        }

      

        /// <summary>
        /// Delete footer menu item object
        /// </summary>
        /// <param name="id">footer menu item id</param>
        /// <returns>true if deleted false otherwise</returns>
        public bool Delete(int id)
        {
            try
            {
                var item = _db.FooterMenuItem.FirstOrDefault(x => x.Id == id);
                
                _db.FooterMenuItem.Remove(item);
                _db.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Get footer menu item object
        /// </summary>
        /// <param name="id">footer menu item id</param>
        /// <returns>footer menu item object</returns>
        public FooterMenuItem Get(int id)
        {

            return _db.FooterMenuItem.FirstOrDefault(p => p.Id == id);
        }

        /// <summary>
        /// Get footer menu item object
        /// </summary>
        /// <param name="id">footer menu item id</param>
        /// <returns>footer menu item object</returns>
        public FooterMenuItem GetDetail(int id)
        {

            return _db.FooterMenuItem.FirstOrDefault(p => p.Id == id);
        }

        /// <summary>
        /// Get footer menu item object with no tracking for id
        /// </summary>
        /// <param name="id">footer menu item id</param>
        /// <returns>footer menu item object</returns>
        public FooterMenuItem GetByIdWithNoTracking(int id)
        {
            return _db.FooterMenuItem.AsNoTracking().FirstOrDefault(p => p.Id == id);
        }
    }
}
