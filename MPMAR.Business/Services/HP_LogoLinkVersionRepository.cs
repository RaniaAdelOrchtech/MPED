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
    public class HP_LogoLinkVersionRepository : IHP_LogoLinkVersionRepository
    {
        private readonly ApplicationDbContext _db;

        public HP_LogoLinkVersionRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public void Add(HomePageLogoLinkVersions model)
        {
            _db.HomePageLogoLinkVersions.Add(model);
            _db.SaveChanges();
        }

        public bool Update(HomePageLogoLinkVersions model)
        {
            try
            {
                _db.HomePageLogoLinkVersions.Update(model);
                _db.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public List<HomePageLogoLinkVersions> GetLogoLinkVersions()
        {
            //join between version and non version HomePageLogoLinks take the value from non version if
            //the version wasn't exist or was draft or submited
            var queryright = (from ll in _db.HomePageLogoLinks
                              from llv in _db.HomePageLogoLinkVersions.Where(d => d.HomePageLogoLinkId == ll.Id && d.VersionStatusEnum != VersionStatusEnum.Ignored)
                              .OrderByDescending(d => d.Id).DefaultIfEmpty().Take(1)
                              select new HomePageLogoLinkVersions
                              {
                                  Id = ll.Id,
                                  EnTitle = (llv != null && (llv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || llv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? llv.EnTitle : ll.EnTitle,
                                  ArTitle = (llv != null && (llv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || llv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? llv.ArTitle : ll.ArTitle,
                                  ImageUrl = (llv != null && (llv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || llv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? llv.ImageUrl : ll.ImageUrl,
                                  Url = (llv != null && (llv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || llv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? llv.Url : ll.Url,
                                  HomePageLogoLinkId = ll.Id,
                                  VersionStatusEnum = llv.VersionStatusEnum ?? VersionStatusEnum.Draft,
                                  ChangeActionEnum = llv.ChangeActionEnum ?? ChangeActionEnum.New,
                              });

            return queryright.ToList();
        }

        public HomePageLogoLinkVersions Get(int id)
        {
            return _db.HomePageLogoLinkVersions.FirstOrDefault(i => i.Id == id);
        }

        public HomePageLogoLinkVersions GetByLogoLinkId(int id)
        {
            return _db.HomePageLogoLinkVersions.OrderByDescending(i => i.Id).AsNoTracking().FirstOrDefault(i => i.HomePageLogoLinkId == id);
        }

        public IEnumerable<HomePageLogoLinkVersions> GetAllDrafts()
        {
            return _db.HomePageLogoLinkVersions.Where(e => e.VersionStatusEnum == VersionStatusEnum.Draft).ToList();
        }

        public IEnumerable<HomePageLogoLinkVersions> GetAllSubmitted()
        {
            return _db.HomePageLogoLinkVersions.Where(e => e.VersionStatusEnum == VersionStatusEnum.Submitted).ToList();
        }
    }
}
