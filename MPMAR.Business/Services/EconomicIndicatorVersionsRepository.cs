using Microsoft.EntityFrameworkCore;
using MPMAR.Business.Interfaces;
using MPMAR.Data;
using MPMAR.Data.Enums;
using MPMAR.Data.HomePageModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MPMAR.Business.Services
{
    public class EconomicIndicatorVersionsRepository : IEconomicIndicatorVersionsRepository
    {
        private readonly ApplicationDbContext _db;

        public EconomicIndicatorVersionsRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Add Economic indecator version object
        /// </summary>
        /// <param name="model">economic indicator version model</param>
        /// <returns></returns>
        public void Add(EconomicIndicatorsVersion model)
        {
            _db.EconomicIndicatorsVersion.Add(model);
            _db.SaveChanges();
        }

        /// <summary>
        /// Update economic indicator object from database
        /// </summary>
        /// <param name="model">economic indicator object new data</param>
        /// <returns>true if updated false otherwise</returns>
        public bool Update(EconomicIndicatorsVersion model)
        {
            try
            {
                _db.EconomicIndicatorsVersion.Update(model);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Get all economic indicator versions
        /// </summary>
        /// <returns>List of economic indicator versions</returns>
        public List<EconomicIndicatorsVersion> GetEcoIndiVersions()
        {
            //join between version and non version EconomicIndicators take the value from non version if
            //the version wasn't exist or was draft or submited
            var queryright = (from ei in _db.EconomicIndicators
                              from eiv in _db.EconomicIndicatorsVersion.Where(d => d.EconomicIndicatorsId == ei.Id && d.VersionStatusEnum != VersionStatusEnum.Ignored)
                              .OrderByDescending(d => d.Id).DefaultIfEmpty().Take(1)
                              select new EconomicIndicatorsVersion
                              {
                                  Id = ei.Id,
                                  ImageDiscriptionAr1 = (eiv != null && (eiv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || eiv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? eiv.ImageDiscriptionAr1 : ei.ImageDiscriptionAr1,
                                  ImageDiscriptionAr2 = (eiv != null && (eiv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || eiv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? eiv.ImageDiscriptionAr2 : ei.ImageDiscriptionAr2,
                                  ImageDiscriptionAr3 = (eiv != null && (eiv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || eiv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? eiv.ImageDiscriptionAr3 : ei.ImageDiscriptionAr3,
                                  ImageDiscriptionEn1 = (eiv != null && (eiv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || eiv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? eiv.ImageDiscriptionEn1 : ei.ImageDiscriptionEn1,
                                  ImageDiscriptionEn2 = (eiv != null && (eiv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || eiv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? eiv.ImageDiscriptionEn2 : ei.ImageDiscriptionEn2,
                                  ImageDiscriptionEn3 = (eiv != null && (eiv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || eiv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? eiv.ImageDiscriptionEn3 : ei.ImageDiscriptionEn3,
                                  ImageTitleAr1 = (eiv != null && (eiv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || eiv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? eiv.ImageTitleAr1 : ei.ImageTitleAr1,
                                  ImageTitleAr2 = (eiv != null && (eiv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || eiv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? eiv.ImageTitleAr2 : ei.ImageTitleAr2,
                                  ImageTitleAr3 = (eiv != null && (eiv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || eiv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? eiv.ImageTitleAr3 : ei.ImageTitleAr3,
                                  ImageTitleEn1 = (eiv != null && (eiv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || eiv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? eiv.ImageTitleEn1 : ei.ImageTitleEn1,
                                  ImageTitleEn2 = (eiv != null && (eiv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || eiv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? eiv.ImageTitleEn2 : ei.ImageTitleEn2,
                                  ImageTitleEn3 = (eiv != null && (eiv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || eiv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? eiv.ImageTitleEn3 : ei.ImageTitleEn3,
                                  ImageUrl1 = (eiv != null && (eiv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || eiv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? eiv.ImageUrl1 : ei.ImageUrl1,
                                  ImageUrl2 = (eiv != null && (eiv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || eiv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? eiv.ImageUrl2 : ei.ImageUrl2,
                                  ImageUrl3 = (eiv != null && (eiv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || eiv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? eiv.ImageUrl3 : ei.ImageUrl3,
                                  Link1 = (eiv != null && (eiv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || eiv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? eiv.Link1 : ei.Link1,
                                  Link2 = (eiv != null && (eiv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || eiv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? eiv.Link2 : ei.Link2,
                                  Link3 = (eiv != null && (eiv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || eiv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? eiv.Link3 : ei.Link3,
                                  MainDiscriptionAr = (eiv != null && (eiv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || eiv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? eiv.MainDiscriptionAr : ei.MainDiscriptionAr,
                                  MainDiscriptionEn = (eiv != null && (eiv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || eiv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? eiv.MainDiscriptionEn : ei.MainDiscriptionEn,
                                  EconomicIndicatorsId = ei.Id,
                                  VersionStatusEnum = eiv.VersionStatusEnum ?? VersionStatusEnum.Draft,
                                  ChangeActionEnum = eiv.ChangeActionEnum ?? ChangeActionEnum.New
                              });

            return queryright.ToList();
        }

        /// <summary>
        /// Get economic indicator object by id
        /// </summary>
        /// <param name="id">economic indicator id</param>
        /// <returns>single economic indicator version object</returns>
        public EconomicIndicatorsVersion Get(int id)
        {
            return _db.EconomicIndicatorsVersion.FirstOrDefault(i => i.Id == id);
        }

        /// <summary>
        /// Get economic indicator version object by economic indicator id
        /// </summary>
        /// <param name="id">economic indicator id</param>
        /// <returns>single economic indecator version object</returns>
        public EconomicIndicatorsVersion GetByEcoIndiId(int id)
        {
            return _db.EconomicIndicatorsVersion.OrderByDescending(i => i.Id).AsNoTracking().FirstOrDefault(i => i.EconomicIndicatorsId == id);
        }

        /// <summary>
        /// Get all drafts economic indicators objects
        /// </summary>
        /// <returns>IEnumerable of economic indicators versions</returns>
        public IEnumerable<EconomicIndicatorsVersion> GetAllDrafts()
        {
            return _db.EconomicIndicatorsVersion.Where(e => e.VersionStatusEnum == VersionStatusEnum.Draft).ToList();
        }

        /// <summary>
        /// Get all submitted economic indicators objects
        /// </summary>
        /// <returns>IEnumerable of economic indicators versions</returns>
        public IEnumerable<EconomicIndicatorsVersion> GetAllSubmitted()
        {
            return _db.EconomicIndicatorsVersion.Where(e => e.VersionStatusEnum == VersionStatusEnum.Submitted).ToList();
        }
    }
}
