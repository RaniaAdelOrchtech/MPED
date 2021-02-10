using Microsoft.EntityFrameworkCore;
using MPMAR.Business.Interfaces;
using MPMAR.Data;
using MPMAR.Data.HomePageModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MPMAR.Business.Services
{
    public class EconomicIndicatorReopsitory : IEconomicIndicatorRepository
    {
        private readonly ApplicationDbContext _db;
        public EconomicIndicatorReopsitory(ApplicationDbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Get all economic indicators screens
        /// </summary>
        /// <returns>IEnumerable of economic indicators</returns>
        public IEnumerable<EconomicIndicators> GetAll()
        {
            return _db.EconomicIndicators.ToList();
        }

        /// <summary>
        /// Get economic indicator by id
        /// </summary>
        /// <param name="id">economic indicator id</param>
        /// <returns>Single economic indicator object</returns>
        public EconomicIndicators GetById(int id)
        {
            return _db.EconomicIndicators.FirstOrDefault(x => x.Id == id);
        }

        /// <summary>
        /// Update economic indicator object from database
        /// </summary>
        /// <param name="economicIndicators">economic indicator new data</param>
        /// <returns></returns>
        public void Update(EconomicIndicators economicIndicators)
        {
            _db.EconomicIndicators.Attach(economicIndicators);
            _db.Entry(economicIndicators).State = EntityState.Modified;
            _db.SaveChanges();
        }

        /// <summary>
        /// Get economic indicator version object by id
        /// </summary>
        /// <param name="id">economic indicator id</param>
        /// <returns>Single economic indicators version</returns>
        public EconomicIndicatorsVersion GetByEcoIndiId(int id)
        {
            return _db.EconomicIndicators.Where(i => i.Id == id).Select(
                pgMinisty => new EconomicIndicatorsVersion()
                {
                    Id = pgMinisty.Id,
                    ApprovalDate = pgMinisty.ApprovalDate,
                    ApprovedById = pgMinisty.ApprovedById,
                    CreatedById = pgMinisty.CreatedById,
                    CreationDate = pgMinisty.CreationDate,
                    ModificationDate = pgMinisty.ModificationDate,
                    ModifiedById = pgMinisty.ModifiedById,
                    ImageDiscriptionAr1 = pgMinisty.ImageDiscriptionAr1,
                    ImageDiscriptionAr2 = pgMinisty.ImageDiscriptionAr2,
                    ImageDiscriptionAr3 = pgMinisty.ImageDiscriptionAr3,
                    ImageDiscriptionEn1 = pgMinisty.ImageDiscriptionEn1,
                    ImageDiscriptionEn2 = pgMinisty.ImageDiscriptionEn2,
                    ImageDiscriptionEn3 = pgMinisty.ImageDiscriptionEn3,
                    ImageTitleAr1 = pgMinisty.ImageTitleAr1,
                    ImageTitleAr2 = pgMinisty.ImageTitleAr2,
                    ImageTitleAr3 = pgMinisty.ImageTitleAr3,
                    ImageTitleEn1 = pgMinisty.ImageTitleEn1,
                    ImageTitleEn2 = pgMinisty.ImageTitleEn2,
                    ImageTitleEn3 = pgMinisty.ImageTitleEn3,
                    ImageUrl1 = pgMinisty.ImageUrl1,
                    ImageUrl2 = pgMinisty.ImageUrl2,
                    ImageUrl3 = pgMinisty.ImageUrl3,
                    Link1=pgMinisty.Link1,
                    Link2=pgMinisty.Link2,
                    Link3=pgMinisty.Link3,
                    MainDiscriptionAr = pgMinisty.MainDiscriptionAr,
                    MainDiscriptionEn = pgMinisty.MainDiscriptionEn
                }).FirstOrDefault();
        }
    }
}
