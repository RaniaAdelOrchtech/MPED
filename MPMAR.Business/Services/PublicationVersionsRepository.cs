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
    public class PublicationVersionsRepository : IPublicationVersionsRepository
    {
        private readonly ApplicationDbContext _db;

        public PublicationVersionsRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public void Add(PublicationVersions model)
        {
            _db.PublicationVersions.Add(model);
            _db.SaveChanges();
        }

        public bool Update(PublicationVersions model)
        {
            try
            {
                _db.PublicationVersions.Update(model);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<PublicationVersions> GetpublicationVersions()
        {
            //join between version and non version Publications take the value from non version if
            //the version wasn't exist or was draft or submited
            var queryright = (from p in _db.Publications.Where(d => !d.IsDeleted)
                              from pv in _db.PublicationVersions.Where(d => d.PublicationId == p.Id && !d.IsDeleted && d.VersionStatusEnum != VersionStatusEnum.Ignored)
                              .OrderByDescending(d => d.Id).DefaultIfEmpty().Take(1)
                              select new PublicationVersions
                              {
                                  Id = p.Id,
                                  ArMainTitle = (pv != null && (pv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || pv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? pv.ArMainTitle : p.ArMainTitle,
                                  EnMainTitle = (pv != null && (pv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || pv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? pv.EnMainTitle : p.EnMainTitle,
                                  ArTitle1 = (pv != null && (pv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || pv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? pv.ArTitle1 : p.ArTitle1,
                                  EnTitle1 = (pv != null && (pv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || pv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? pv.EnTitle1 : p.EnTitle1,
                                  ArDescription1 = (pv != null && (pv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || pv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? pv.ArDescription1 : p.ArDescription1,
                                  EnDescription1 = (pv != null && (pv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || pv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? pv.EnDescription1 : p.EnDescription1,
                                  ArTitle2 = (pv != null && (pv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || pv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? pv.ArTitle2 : p.ArTitle2,
                                  EnTitle2 = (pv != null && (pv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || pv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? pv.EnTitle2 : p.EnTitle2,
                                  ArDescription2 = (pv != null && (pv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || pv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? pv.ArDescription2 : p.ArDescription2,
                                  EnDescription2 = (pv != null && (pv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || pv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? pv.EnDescription2 : p.EnDescription2,
                                  ArTitle3 = (pv != null && (pv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || pv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? pv.ArTitle3 : p.ArTitle3,
                                  EnTitle3 = (pv != null && (pv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || pv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? pv.EnTitle3 : p.EnTitle3,
                                  ArDescription3 = (pv != null && (pv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || pv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? pv.ArDescription3 : p.ArDescription3,
                                  EnDescription3 = (pv != null && (pv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || pv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? pv.EnDescription3 : p.EnDescription3,
                                  Link1 = (pv != null && (pv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || pv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? pv.Link1 : p.Link1,
                                  Link2 = (pv != null && (pv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || pv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? pv.Link2 : p.Link2,
                                  Link3 = (pv != null && (pv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || pv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? pv.Link3 : p.Link3,
                                  Image1 = (pv != null && (pv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || pv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? pv.Image1 : p.Image1,
                                  Image2 = (pv != null && (pv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || pv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? pv.Image2 : p.Image2,
                                  Image3 = (pv != null && (pv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || pv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? pv.Image3 : p.Image3,
                                  IsActive = (pv != null && (pv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || pv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? pv.IsActive : p.IsActive,
                                  IsDeleted = (pv != null && (pv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || pv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? pv.IsDeleted : p.IsDeleted,
                                  PublicationId = p.Id,
                                  VersionStatusEnum = pv.VersionStatusEnum ?? VersionStatusEnum.Draft,
                                  ChangeActionEnum = pv.ChangeActionEnum ?? ChangeActionEnum.New
                              });

            return queryright.ToList();
        }

        public PublicationVersions Get(int id)
        {
            return _db.PublicationVersions.FirstOrDefault(i => i.Id == id);
        }

        public PublicationVersions GetByPublicationId(int id)
        {
            return _db.PublicationVersions.OrderByDescending(i => i.Id).AsNoTracking().FirstOrDefault(i => i.PublicationId == id);
        }

        public IEnumerable<PublicationVersions> GetAllDrafts()
        {
            return _db.PublicationVersions.Where(e => e.VersionStatusEnum == VersionStatusEnum.Draft).ToList();
        }

        public IEnumerable<PublicationVersions> GetAllSubmitted()
        {
            return _db.PublicationVersions.Where(e => e.VersionStatusEnum == VersionStatusEnum.Submitted).ToList();
        }
    }
}
