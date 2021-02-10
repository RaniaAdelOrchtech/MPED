using Microsoft.EntityFrameworkCore;
using MPMAR.Business.Interfaces;
using MPMAR.Data;
using MPMAR.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace MPMAR.Business.Services
{
    public class MinistryTimeLineVersionsRepository : IMinistryTimeLineVersionsRepository
    {
        private readonly ApplicationDbContext _db;

        public MinistryTimeLineVersionsRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public void Add(MinistryTimeLineVersions model)
        {
            _db.MinistryTimeLineVersions.Add(model);
            _db.SaveChanges();
            _db.Entry(model).State = EntityState.Detached;
        }

        public IEnumerable<MinistryTimeLineVersions> GetAllByPageInfo(FormerMinistriesPageInfoVersions pageInfo)
        {

            //get data from MinistryTimeLine if pageInfo has no MinistryTimeLine or its status is approver pr ignored
            if ((pageInfo.MinistryTimeLineVersions != null && !pageInfo.MinistryTimeLineVersions.Any()) || pageInfo.VersionStatusEnum == VersionStatusEnum.Approved || pageInfo.VersionStatusEnum == VersionStatusEnum.Ignored)
            {
                var mtlv = _db.MinistryTimeLine.Where(x => !x.IsDeleted).Select(x => new MinistryTimeLineVersions
                {
                    Id = x.Id,
                    MinistryTimeLineId = x.Id,
                    ArName = x.ArName,
                    EnName = x.EnName,
                    VersionStatusEnum = VersionStatusEnum.Draft,
                    IsActive = x.IsActive,
                    Order = x.Order,
                    EnDescription = x.EnDescription,
                    ArDescription = x.ArDescription,
                    ProfileImageUrl = x.ProfileImageUrl,
                    IsDeleted = x.IsDeleted,
                    EventSocialLinks = x.EventSocialLinks,
                    StartDate = x.StartDate,
                    EndDate = x.EndDate,
                    PeriodAr = x.PeriodAr,
                    Facebook = x.Facebook,
                    Twitter = x.Twitter,
                    Email = x.Email,
                    PeriodEn = x.PeriodEn,
                    StatusId = x.StatusId,
                    ChangeActionEnum = ChangeActionEnum.Update,
                    CreationDate = x.CreationDate,
                    CreatedById = x.CreatedById,
                    ApprovalDate = x.ApprovalDate,
                    ApprovedById = x.ApprovedById
                }).ToList();
                return mtlv;
            }
            else
            {
                return _db.MinistryTimeLineVersions.Where(x => x.FormerMinistriesPageInfoVersionsId == pageInfo.Id && !x.IsDeleted).Select(x => new MinistryTimeLineVersions
                {
                    Id = x.Id,
                    MinistryTimeLineId = x.MinistryTimeLineId,
                    ArName = x.ArName,
                    EnName = x.EnName,
                    VersionStatusEnum = VersionStatusEnum.Draft,
                    IsActive = x.IsActive,
                    Order = x.Order,
                    EnDescription = x.EnDescription,
                    ArDescription = x.ArDescription,
                    ProfileImageUrl = x.ProfileImageUrl,
                    IsDeleted = x.IsDeleted,
                    EventSocialLinks = x.EventSocialLinks,
                    StartDate = x.StartDate,
                    EndDate = x.EndDate,
                    PeriodAr = x.PeriodAr,
                    Facebook = x.Facebook,
                    Twitter = x.Twitter,
                    Email = x.Email,
                    PeriodEn = x.PeriodEn,
                    StatusId = x.StatusId,
                    ChangeActionEnum = ChangeActionEnum.Update,
                    CreationDate = x.CreationDate,
                    CreatedById = x.CreatedById,
                    ApprovalDate = x.ApprovalDate,
                    ApprovedById = x.ApprovedById
                }).ToList();
            }


        }

        public MinistryTimeLineVersions GetByCondition(Expression<Func<MinistryTimeLineVersions, bool>> expression)
        {
            var item = _db.MinistryTimeLineVersions.OrderByDescending(x => x.Id).AsNoTracking().FirstOrDefault(expression);
            return item;
        }

        public MinistryTimeLineVersions GetDetail(int id)
        {
            return _db.MinistryTimeLineVersions.FirstOrDefault(p => p.Id == id);
        }

        public MinistryTimeLineVersions GetDetailWithNoTarcking(int id)
        {
            return _db.MinistryTimeLineVersions.AsNoTracking().FirstOrDefault(p => p.Id == id);
        }

        public void MarkAllAsDeleted()
        {
            var ministriesVersions = _db.MinistryTimeLineVersions.ToList();
            if (ministriesVersions.Any())
            {
                ministriesVersions.ForEach(x => x.IsDeleted = true);
                _db.MinistryTimeLineVersions.UpdateRange(ministriesVersions);
                _db.SaveChanges();
            }
        }

        public void Update(MinistryTimeLineVersions model)
        {
            _db.MinistryTimeLineVersions.Update(model);
            _db.SaveChanges();
        }
    }
}
