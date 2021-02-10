using Microsoft.EntityFrameworkCore;
using MPMAR.Business.Interfaces;
using MPMAR.Data;
using MPMAR.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MPMAR.Business.Services
{
    public class FormerMinistriesPageInfoVersionRepository : IFormerMinistriesPageInfoVersionRepository
    {
        private readonly ApplicationDbContext _db;

        public FormerMinistriesPageInfoVersionRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public void Add(FormerMinistriesPageInfoVersions model)
        {
            _db.FormerMinistriesPageInfoVersions.Add(model);
            _db.SaveChanges();
        }

        public FormerMinistriesPageInfoVersions Get(string userId = "", bool includeFlage = true)
        {


            var fr = _db.FormerMinistriesPageInfos.FirstOrDefault();
            FormerMinistriesPageInfoVersions frv;
            if (includeFlage)
            {
                frv = _db.FormerMinistriesPageInfoVersions.Include(x => x.MinistryTimeLineVersions).Where(x => !x.IsDeleted && x.IsActive).OrderByDescending(x => x.Id).FirstOrDefault();

            }
            else
            {
                frv = _db.FormerMinistriesPageInfoVersions.Where(x => !x.IsDeleted && x.IsActive).OrderByDescending(x => x.Id).FirstOrDefault();
            }
            //if FormerMinistriesPageInfoVersions not exist get from FormerMinistriesPageInfo
            if (frv == null)
            {
                frv = new FormerMinistriesPageInfoVersions()
                {
                    Title1Ar = fr.Title1Ar,
                    Title1En = fr.Title1En,
                    DescriptionAr = fr.DescriptionAr,
                    DescriptionEn = fr.DescriptionEn,
                    Title2Ar = fr.Title2Ar,
                    Title2En = fr.Title2En,
                    IsActive = fr.IsActive,
                    VersionStatusEnum = VersionStatusEnum.Draft,
                    ChangeActionEnum = ChangeActionEnum.Update,
                    CreationDate = DateTime.Now,
                    CreatedById = userId,
                    FormerMinistriesPageInfoId = fr.Id
                };
                _db.FormerMinistriesPageInfoVersions.Add(frv);
                _db.SaveChanges();
                return frv;
            }
            frv.Title1Ar = (fr == null || frv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || frv.VersionStatusEnum == VersionStatusEnum.Submitted) ? frv.Title1Ar : fr.Title1Ar;
            frv.Title1En = (fr == null || frv.VersionStatusEnum == VersionStatusEnum.Draft
            || frv.VersionStatusEnum == VersionStatusEnum.Submitted) ? frv.Title1En : fr.Title1En;
            frv.DescriptionAr = (fr == null || frv.VersionStatusEnum == VersionStatusEnum.Draft
            || frv.VersionStatusEnum == VersionStatusEnum.Submitted) ? frv.DescriptionAr : fr.DescriptionAr;
            frv.DescriptionEn = (fr == null || frv.VersionStatusEnum == VersionStatusEnum.Draft
            || frv.VersionStatusEnum == VersionStatusEnum.Submitted) ? frv.DescriptionEn : fr.DescriptionEn;
            frv.Title2Ar = (fr == null || frv.VersionStatusEnum == VersionStatusEnum.Draft
            || frv.VersionStatusEnum == VersionStatusEnum.Submitted) ? frv.Title2Ar : fr.Title2Ar;
            frv.Title2En = (fr == null || frv.VersionStatusEnum == VersionStatusEnum.Draft
            || frv.VersionStatusEnum == VersionStatusEnum.Submitted) ? frv.Title2En : fr.Title2En;
            frv.IsActive = (fr == null || frv.VersionStatusEnum == VersionStatusEnum.Draft
            || frv.VersionStatusEnum == VersionStatusEnum.Submitted) ? frv.IsActive : fr.IsActive;
            frv.VersionStatusEnum = frv.VersionStatusEnum ?? VersionStatusEnum.Draft;
            frv.ChangeActionEnum = frv.ChangeActionEnum ?? ChangeActionEnum.Update;

            if (includeFlage)
                frv.MinistryTimeLineVersions = frv.MinistryTimeLineVersions;


            return frv;
        }

        public bool Update(FormerMinistriesPageInfoVersions model)
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
    }
}
