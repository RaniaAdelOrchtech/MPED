using Microsoft.EntityFrameworkCore;
using MPMAR.Business.Interfaces;
using MPMAR.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MPMAR.Business.Services
{
    public class FooterMenuTitleRepository : IFooterMenuTitleRepository
    {
        private readonly ApplicationDbContext _db;

        public FooterMenuTitleRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Get All Footer menu title objects
        /// </summary>
        /// <returns>IEnumerable from footer menu title objects</returns>
        public IEnumerable<FooterMenuTitle>  GetAll()
        {

            return _db.FooterMenuTitles.Where(x=>!x.IsDeleted).ToList();
        }

        /// <summary>
        /// Get footer menu title object by id
        /// </summary>
        /// <param name="id">footer menu title id</param>
        /// <returns>footer menu title object</returns>
        public FooterMenuTitle GetById(int id)
        {
            return _db.FooterMenuTitles.Find(id);
        }

        /// <summary>
        /// update footer menu title from database
        /// </summary>
        /// <param name="model">footer menu title object new data</param>
        /// <returns>true if ypdated false otherwise</returns>
        public bool Update(FooterMenuTitle model)
        {
            try
            {
                _db.Update(model);
                _db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// delete footer menu title by id
        /// </summary>
        /// <param name="id">footer menu title id</param>
        /// <returns>true if deleted false otherwise</returns>
        public bool Delete(int id)
        {
            try
            {
                var item = _db.FooterMenuTitles.FirstOrDefault(x => x.Id == id);
                item.IsDeleted = true;

                _db.FooterMenuTitles.Attach(item);
                _db.Entry(item).State = EntityState.Modified;
                _db.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// get footer menu title version by footer menu title id
        /// </summary>
        /// <param name="id">footer menu title id</param>
        /// <returns>footer menu title version object</returns>
        public FooterMenuTitleVersions GetByFooterMenuTitleId(int id)
        {
            return _db.FooterMenuTitles.Where(i => i.Id == id).Select(
                pgMinisty => new FooterMenuTitleVersions()
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
                    Order = pgMinisty.Order,
                }).FirstOrDefault();
        }
    }
}
