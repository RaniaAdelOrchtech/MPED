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
    public class HP_VideoVersionRepository : IHP_VideoVersionRepository
    {
        private readonly ApplicationDbContext _db;

        public HP_VideoVersionRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public void Add(HomePageVideoVersions model)
        {
            _db.HomePageVideoVersions.Add(model);
            _db.SaveChanges();
        }

        public bool Update(HomePageVideoVersions model)
        {
            try
            {
                _db.HomePageVideoVersions.Update(model);
                _db.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public List<HomePageVideoVersions> GetVideosVersions()
        {
            //join between version and non version homePageVideos take the value from non version if
            //the version wasn't exist or was draft or submited
            var queryright = (from vd in _db.homePageVideos.Where(d => !d.IsDeleted)
                              from vdv in _db.HomePageVideoVersions.Where(d => d.HomePageVideoId == vd.Id && !d.IsDeleted && d.VersionStatusEnum != VersionStatusEnum.Ignored)
                              .OrderByDescending(d => d.Id).DefaultIfEmpty().Take(1)
                              select new HomePageVideoVersions
                              {
                                  Id = vd.Id,
                                  VideoUrl = (vdv != null && (vdv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || vdv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? vdv.VideoUrl : vd.VideoUrl,
                                  IsActive = (vdv != null && (vdv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || vdv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? vdv.IsActive : vd.IsActive,
                                  IsDeleted = (vdv != null && (vdv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || vdv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? vdv.IsDeleted : vd.IsDeleted,
                                  HomePageVideoId = vd.Id,
                                  VersionStatusEnum = vdv.VersionStatusEnum ?? VersionStatusEnum.Draft,
                                  ChangeActionEnum = vdv.ChangeActionEnum ?? ChangeActionEnum.New
                              });

            return queryright.ToList();
        }

        public HomePageVideoVersions Get(int id)
        {
            return _db.HomePageVideoVersions.FirstOrDefault(i => i.Id == id);
        }

        public HomePageVideoVersions GetByVideoId(int id)
        {
            return _db.HomePageVideoVersions.OrderByDescending(i => i.Id).AsNoTracking().FirstOrDefault(i => i.HomePageVideoId == id);
        }

        public IEnumerable<HomePageVideoVersions> GetAllDrafts()
        {
            return _db.HomePageVideoVersions.Where(e => e.VersionStatusEnum == VersionStatusEnum.Draft).ToList();
        }

        public IEnumerable<HomePageVideoVersions> GetAllSubmitted()
        {
            return _db.HomePageVideoVersions.Where(e => e.VersionStatusEnum == VersionStatusEnum.Submitted).ToList();
        }
    }
}
