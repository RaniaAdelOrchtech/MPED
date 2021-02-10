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
    public class LeftMenuItemRepository : ILeftMenuItemRepository
    {
        private readonly ApplicationDbContext _db;

        public LeftMenuItemRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public LeftMenuItem Add(LeftMenuItem leftMenuItem)
        {
            try
            {
                _db.LeftMenuItem.Add(leftMenuItem);
                _db.SaveChanges();
                //return _db.LeftMenuItem.Include(x => x.PageRouteVersion).FirstOrDefault(c => c.Id == leftMenuItem.Id);
                return _db.LeftMenuItem.FirstOrDefault(c => c.Id == leftMenuItem.Id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public LeftMenuItem Update(LeftMenuItem leftMenuItem)
        {
            try
            {

                _db.LeftMenuItem.Attach(leftMenuItem);
                _db.Entry(leftMenuItem).State = EntityState.Modified;
                _db.SaveChanges();
           
                return _db.LeftMenuItem.FirstOrDefault(c => c.Id == leftMenuItem.Id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

 
        public IEnumerable<LeftMenuItem> GetLeftMenuItemId(int leftMenuItemId)
        {
            var leftMenuItems = _db.LeftMenuItem.OrderBy(s => s.Id).ToList();
            // !(s.IsDeleted && s.PageRouteVersion.StatusId == (int)RequestStatus.Approved) &&
            return leftMenuItems;
        }

        public LeftMenuItem Delete(int id)
        {
            try
            {
                var item = _db.LeftMenuItem.FirstOrDefault(x => x.Id == id);
                item.IsDeleted = true;

                _db.LeftMenuItem.Attach(item);
                _db.Entry(item).State = EntityState.Modified;
                _db.SaveChanges();

                return _db.LeftMenuItem.FirstOrDefault(c => c.Id == id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public LeftMenuItem Get(int id)
        {

            return _db.LeftMenuItem.FirstOrDefault(p => p.Id == id);
        }
        public LeftMenuItem GetDetail(int id)
        {

            return _db.LeftMenuItem.FirstOrDefault(p => p.Id == id);
        }

        public LeftMenuItemVersions GetByLeftMenuItemId(int id)
        {
            return _db.LeftMenuItem.Where(i => i.Id == id).Select(
                pgMinisty => new LeftMenuItemVersions()
                {
                    Id = pgMinisty.Id,
                    IsActive = pgMinisty.IsActive,
                    IsDeleted = pgMinisty.IsDeleted,
                    ApprovalDate = pgMinisty.ApprovalDate,
                    ApprovedById = pgMinisty.ApprovedById,
                    CreatedById = pgMinisty.CreatedById,
                    CreationDate = pgMinisty.CreationDate,
                    EnTitle = pgMinisty.EnTitle,
                    ArTitle = pgMinisty.ArTitle,
                    ImagePath = pgMinisty.ImagePath,
                    Link = pgMinisty.Link,
                    Order = pgMinisty.Order,
                    LeftMenuType = pgMinisty.LeftMenuType
                }).FirstOrDefault();
        }
    }
}
