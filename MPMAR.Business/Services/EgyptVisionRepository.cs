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
    public class EgyptVisionRepository : IEgyptVisionRepository
    {
        private readonly ApplicationDbContext _db;

        public EgyptVisionRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Add new egypt vision object
        /// </summary>
        /// <param name="EgyptVisionItem">egypt vision model</param>
        /// <returns>Added Object</returns>
        public EgyptVision Add(EgyptVision EgyptVisionItem)
        {
            try
            {
                _db.EgyptVision.Add(EgyptVisionItem);
                _db.SaveChanges();
                //return _db.FooterMenuItem.Include(x => x.PageRouteVersion).FirstOrDefault(c => c.Id == footerMenuItem.Id);
                return _db.EgyptVision.FirstOrDefault(c => c.Id == EgyptVisionItem.Id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// update an egypt vision object
        /// </summary>
        /// <param name="EgyptVisionItem">egypt vision model</param>
        /// <returns>Updated object</returns>
        public EgyptVision Update(EgyptVision EgyptVisionItem)
        {
            try
            {
                
                _db.EgyptVision.Attach(EgyptVisionItem);
                _db.Entry(EgyptVisionItem).State = EntityState.Modified;
                _db.SaveChanges();
   
                return _db.EgyptVision.FirstOrDefault(c => c.Id == EgyptVisionItem.Id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        

        /// <summary>
        /// Get all Egypt Vision
        /// </summary>
        /// <returns>IEnumerable of egypt vision</returns>
        public IEnumerable<EgyptVision> GetEgyptVisionId()
        {
            var EgyptVisionItem = _db.EgyptVision.OrderBy(s => s.Id).ToList();
            // !(s.IsDeleted && s.PageRouteVersion.StatusId == (int)RequestStatus.Approved) &&
            return EgyptVisionItem;
        }

        /// <summary>
        /// Delete an egypt vision object
        /// </summary>
        /// <param name="id">egypt vision id</param>
        /// <returns>deleted object</returns>
        public EgyptVision Delete(int id)
        {
            try
            {
                var item = _db.EgyptVision.FirstOrDefault(x => x.Id == id);
                item.IsActive = false;

                _db.EgyptVision.Attach(item);
                _db.Entry(item).State = EntityState.Modified;
                _db.SaveChanges();

                return _db.EgyptVision.FirstOrDefault(c => c.Id == id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// get egypt vision object by id
        /// </summary>
        /// <param name="id">egypt vision id</param>
        /// <returns>Single egypt vision object</returns>
        public EgyptVision Get(int id)
        {

            return _db.EgyptVision.FirstOrDefault(p => p.Id == id);
        }

        /// <summary>
        /// Get all egypt vision objects
        /// </summary>
        /// <returns>IEnumerable of egypt vision objects</returns>
        public IEnumerable<EgyptVision> Get()
        {

            return _db.EgyptVision.OrderBy(i => i.Id);
        }

        /// <summary>
        /// Details of single egypt vision object
        /// </summary>
        /// <param name="id">egypt vision id</param>
        /// <returns>Single egypt vision object</returns>
        public EgyptVision GetDetail(int id)
        {

            return _db.EgyptVision.FirstOrDefault(p => p.Id == id);
        }

        /// <summary>
        /// check if egypt vision object exist ot not
        /// </summary>
        /// <param name="id">egypt vision id</param>
        /// <returns>true if exist false otherwise</returns>
        public bool ifEgyptVisionExist(int id)
        {
            var ps = _db.EgyptVision.Find(id);
            if (ps == null)
                return false;
            return true;
        }

        /// <summary>
        /// Get egypt vision version by egypt vision id
        /// </summary>
        /// <param name="id">egypt vision id</param>
        /// <returns>egypt vision version object</returns>
        public EgyptVisionVersion GetByEgyptVisionId(int id)
        {
            return _db.EgyptVision.Where(i => i.Id == id).Select(
                pgMinisty => new EgyptVisionVersion()
                {
                    Id = pgMinisty.Id,
                    EnEgyptVisionName = pgMinisty.EnEgyptVisionName,
                    ArEgyptVisionName = pgMinisty.ArEgyptVisionName,
                    EnEgyptVisionSmallDesc = pgMinisty.EnEgyptVisionSmallDesc,
                    ArEgyptVisionSmallDesc = pgMinisty.ArEgyptVisionSmallDesc,
                    EnEgyptVisionDesc = pgMinisty.EnEgyptVisionDesc,
                    ArEgyptVisionDesc = pgMinisty.ArEgyptVisionDesc,
                    IsActive = pgMinisty.IsActive,
                    IsDeleted = pgMinisty.IsDeleted,
                    EnImagePath = pgMinisty.EnImagePath,
                    ArImagePath = pgMinisty.ArImagePath,
                    SeoTitleEN = pgMinisty.SeoTitleEN,
                    SeoTitleAR = pgMinisty.SeoTitleAR,
                    SeoDescriptionEN = pgMinisty.SeoDescriptionEN,
                    SeoDescriptionAR = pgMinisty.SeoDescriptionAR,
                    SeoOgTitleEN = pgMinisty.SeoOgTitleEN,
                    SeoOgTitleAR = pgMinisty.SeoOgTitleAR,
                    SeoTwitterCardEN = pgMinisty.SeoTwitterCardEN,
                    SeoTwitterCardAR = pgMinisty.SeoTwitterCardAR,
                    BgColor = pgMinisty.BgColor,
                    LineColor = pgMinisty.LineColor,
                    Order = pgMinisty.Order,
                    ImagePositionIsRight = pgMinisty.ImagePositionIsRight,
                    CreatedById = pgMinisty.CreatedById,
                    CreationDate = pgMinisty.CreationDate,
                    ApprovalDate = pgMinisty.ApprovalDate,
                    ApprovedById = pgMinisty.ApprovedById,
                    ModifiedById = pgMinisty.ModifiedById,
                    ModificationDate = pgMinisty.ModificationDate,
                    StatusId = pgMinisty.StatusId,
                    PageRouteVersionId = pgMinisty.PageRouteVersionId
                }).FirstOrDefault();
        }

        /// <summary>
        /// Delete egypt vision object
        /// </summary>
        /// <param name="id">egypt vision id</param>
        /// <returns>True if deleted otherwise false</returns>
        public bool SoftDelete(int id)
        {
            try
            {
                var model = Get(id);
                if (model != null)
                {
                    model.IsDeleted = true;

                    Update(model);

                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }


        }
    }
}
