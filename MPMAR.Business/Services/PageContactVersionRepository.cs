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
    public class PageContactVersionRepository : IPageContactVersionRepository
    {
        private readonly ApplicationDbContext _db;

        public PageContactVersionRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public void Add(PageContactVersions model)
        {
            try
            {

            _db.PageContactVersions.Add(model);
            _db.SaveChanges();
            }
            catch (Exception ex)
            {

            }
        }

        public bool Update(PageContactVersions model)
        {
            try
            {
                _db.PageContactVersions.Update(model);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<PageContactVersions> GetPageContactVersions()
        {
            //join between version and non version PageContact take the value from non version if
            //the version wasn't exist or was draft or submited
            var queryright = (from pc in _db.PageContact.Where(d => !d.IsDeleted)
                              from pcv in _db.PageContactVersions.Where(d => d.PageContactId == pc.Id && !d.IsDeleted && d.VersionStatusEnum != VersionStatusEnum.Ignored)
                              .OrderByDescending(d => d.Id).DefaultIfEmpty().Take(1)
                              select new PageContactVersions
                              {
                                  Id = pc.Id,
                                  Order = (pcv != null && (pcv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || pcv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? pcv.Order : pc.Order,
                                  ArParticipateTitle = (pcv != null && (pcv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || pcv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? pcv.ArParticipateTitle : pc.ArParticipateTitle,
                                  EnParticipateTitle = (pcv != null && (pcv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || pcv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? pcv.EnParticipateTitle : pc.EnParticipateTitle,
                                  EnPageName = (pcv != null && (pcv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || pcv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? pcv.EnPageName : pc.EnPageName,
                                  ArPageName = (pcv != null && (pcv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || pcv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? pcv.ArPageName : pc.ArPageName,
                                  ArMapTitle = (pcv != null && (pcv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || pcv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? pcv.ArMapTitle : pc.ArMapTitle,
                                  EnMapTitle = (pcv != null && (pcv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || pcv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? pcv.EnMapTitle : pc.EnMapTitle,
                                  ArAddress = (pcv != null && (pcv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || pcv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? pcv.ArAddress : pc.ArAddress,
                                  EnAddress = (pcv != null && (pcv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || pcv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? pcv.EnAddress : pc.EnAddress,
                                  PageRouteVersionId = (pcv != null && (pcv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || pcv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? pcv.PageRouteVersionId : pc.PageRouteVersionId,
                                  FormParticipateActive = (pcv != null && (pcv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || pcv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? pcv.FormParticipateActive : pc.FormParticipateActive,
                                  MapUrl = (pcv != null && (pcv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || pcv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? pcv.MapUrl : pc.MapUrl,
                                  PhoneNumber = (pcv != null && (pcv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || pcv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? pcv.PhoneNumber : pc.PhoneNumber,
                                  FaxNumber = (pcv != null && (pcv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || pcv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? pcv.FaxNumber : pc.FaxNumber,
                                  EmailParticipateEmail = (pcv != null && (pcv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || pcv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? pcv.EmailParticipateEmail : pc.EmailParticipateEmail,
                                  SeoDescriptionAR = (pcv != null && (pcv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || pcv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? pcv.SeoDescriptionAR : pc.SeoDescriptionAR,
                                  SeoDescriptionEN = (pcv != null && (pcv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || pcv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? pcv.SeoDescriptionEN : pc.SeoDescriptionEN,
                                  SeoOgTitleAR = (pcv != null && (pcv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || pcv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? pcv.SeoOgTitleAR : pc.SeoOgTitleAR,
                                  SeoOgTitleEN = (pcv != null && (pcv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || pcv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? pcv.SeoOgTitleEN : pc.SeoOgTitleEN,
                                  SeoTitleAR = (pcv != null && (pcv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || pcv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? pcv.SeoTitleAR : pc.SeoTitleAR,
                                  SeoTitleEN = (pcv != null && (pcv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || pcv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? pcv.SeoTitleEN : pc.SeoTitleEN,
                                  SeoTwitterCardAR = (pcv != null && (pcv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || pcv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? pcv.SeoTwitterCardAR : pc.SeoTwitterCardAR,
                                  SeoTwitterCardEN = (pcv != null && (pcv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || pcv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? pcv.SeoTwitterCardEN : pc.SeoTwitterCardEN,
                                  IsActive = (pcv != null && (pcv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || pcv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? pcv.IsActive : pc.IsActive,
                                  IsDeleted = (pcv != null && (pcv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || pcv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? pcv.IsDeleted : pc.IsDeleted,
                                  PageContactId = pc.Id,
                                  VersionStatusEnum = pcv.VersionStatusEnum ?? VersionStatusEnum.Draft,
                                  ChangeActionEnum = pcv.ChangeActionEnum ?? ChangeActionEnum.New,
                              });

            return queryright.ToList();
        }

        public PageContactVersions Get(int id)
        {
            return _db.PageContactVersions.FirstOrDefault(i => i.Id == id);
        }

        public PageContactVersions GetByPageContactId(int id)
        {
            return _db.PageContactVersions.OrderByDescending(i => i.Id).AsNoTracking().FirstOrDefault(i => i.PageContactId == id);
        }

        public IEnumerable<PageContactVersions> GetAllDrafts()
        {
            return _db.PageContactVersions.Where(e => e.VersionStatusEnum == VersionStatusEnum.Draft).ToList();
        }

        public IEnumerable<PageContactVersions> GetAllSubmitted()
        {
            return _db.PageContactVersions.Where(e => e.VersionStatusEnum == VersionStatusEnum.Submitted).ToList();
        }
    }
}
