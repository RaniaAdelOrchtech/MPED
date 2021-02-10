using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MPMAR.Business.Interfaces;
using MPMAR.Data;
using MPMAR.Data.Consts;
using MPMAR.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MPMAR.Business
{
    public class BEUsersPrivilegesRepository : IBEUsersPrivilegesRepository
    {
        private readonly ApplicationDbContext _db;


        public BEUsersPrivilegesRepository(ApplicationDbContext db)
        {
            _db = db;

        }
        public void AddDefaultPrivileges(string userId)
        {
            //remove old privileges id exist
            var oldUserPrivileges = _db.BEUsersPrivileges.Where(x => x.ApplicationUserId == userId);
            if (oldUserPrivileges.Any())
                _db.RemoveRange(oldUserPrivileges);

            var userPrivileges = GetuserPrivilegesList(userId);

            _db.BEUsersPrivileges.AddRange(userPrivileges);
            _db.SaveChanges();
        }


        private List<BEUsersPrivileges> GetuserPrivilegesList(string userId)
        {

            var pages = _db.PageRoutes.Where(x => !x.IsDeleted).ToList();
            var staticPages = pages.Where(x => !x.IsDynamicPage);
            var dynamicPages = pages.Where(x => x.IsDynamicPage);

            List<BEUsersPrivileges> userPrivileges = new List<BEUsersPrivileges>();
            //dynamic pages for every page separately
            foreach (var item in dynamicPages)
            {
                userPrivileges.Add(new BEUsersPrivileges()
                {
                    PageTypeId = PrivilegesPageType.DynamicPage,
                    PageRouteId = item.Id,
                    ApplicationUserId = userId,
                    PageName = PagesNamesConst.Dynamic
                });
            }
            //dynamic pages for all pages
            userPrivileges.Add(new BEUsersPrivileges()
            {
                PageTypeId = PrivilegesPageType.DynamicPage,
                ApplicationUserId = userId,
                PageName = PagesNamesConst.Dynamic
            });
            //static pages for every page separately
            foreach (var item in staticPages)
            {

                userPrivileges.Add(new BEUsersPrivileges()
                {
                    PageTypeId = PrivilegesPageType.StaticPage,
                    PageRouteId = item.Id,
                    ApplicationUserId = userId,
                    PageName = Pages_Name_Id.Name_Id.GetValueOrDefault(item.Id)
                });

            }
            //static pages for all pages
            userPrivileges.Add(new BEUsersPrivileges()
            {
                PageTypeId = PrivilegesPageType.StaticPage,
                ApplicationUserId = userId,
            });
            //rest of pages
            var PrivilegesPageTypeValues = Enum.GetValues(typeof(PrivilegesPageType)).Cast<PrivilegesPageType>().Where(x => !(x == PrivilegesPageType.StaticPage || x == PrivilegesPageType.DynamicPage || x > PrivilegesPageType.Governorate));
            foreach (var item in PrivilegesPageTypeValues)
            {
                userPrivileges.Add(new BEUsersPrivileges()
                {
                    PageTypeId = item,
                    ApplicationUserId = userId,
                    PageName = Pages_Name_Id.Name_Id.GetValueOrDefault((int)item)
                });
            }

            return userPrivileges;
        }

        public IEnumerable<ApplicationUser> GetNotSuperAdminUsers()
        {
            // get super admins users
            var superAdminUsers = _db.UserRoles.Where(x => x.RoleId == "21d4b4aa-6150-48a1-9e82-d46983632678").Select(x => x.UserId);
            return _db.ApplicationUsers.Where(x => !superAdminUsers.Contains(x.Id));
        }

        public IEnumerable<BEUsersPrivileges> GetUserPrivileges(string userId)
        {
            return _db.BEUsersPrivileges.Where(x => !x.IsDeleted && x.ApplicationUserId == userId).Include(x => x.PageRoute);
        }

        public void ResetUsersPrivileges()
        {
            var allBEUsersPrivileges = _db.BEUsersPrivileges;

            _db.RemoveRange(allBEUsersPrivileges);


            var allNotSuperAdminUsers = GetNotSuperAdminUsers().ToList();
            foreach (var item in allNotSuperAdminUsers)
            {
                AddDefaultPrivileges(item.Id);
            }
        }

        public void UpdateUserPrivileges(List<BEUsersPrivileges> bEUsersPrivileges)
        {
            bEUsersPrivileges.ForEach(x => x.CanView = x.CanView || x.CanEdit || x.CanDelete || x.CanAdd || x.CanApprove);
            var globalDynamicPagePrivilege = bEUsersPrivileges.FirstOrDefault(x => x.PageTypeId == PrivilegesPageType.DynamicPage && x.PageRouteId == null);
            if (globalDynamicPagePrivilege.CanApprove || globalDynamicPagePrivilege.CanDelete)
            {
                bEUsersPrivileges.Where(x => x.PageTypeId == PrivilegesPageType.DynamicPage && x.PageRouteId != null).ToList().ForEach(x => x.CanView = true);
            }

            foreach (var item in bEUsersPrivileges)
            {
                if (item.PageTypeId == PrivilegesPageType.DynamicPage && item.PageRouteId != null)
                {
                    var dpPrivilege = _db.PageRoutes.Find(item.PageRouteId);
                    if (dpPrivilege != null && !dpPrivilege.IsDeleted)
                        _db.BEUsersPrivileges.Update(item);
                    else
                        _db.Entry(item).State = EntityState.Detached;
                }
                else
                    _db.BEUsersPrivileges.Update(item);

            }

            _db.SaveChanges();


        }

        public void UpdateWithNewDynamicPages(int pageId, bool isRemove = false)
        {
            var allNotSuperAdminUsers = GetNotSuperAdminUsers().ToList();
            if (!isRemove)
            {
                foreach (var item in allNotSuperAdminUsers)
                {
                    var dynamicPageGlobal = _db.BEUsersPrivileges.FirstOrDefault(x => x.ApplicationUserId == item.Id && x.PageTypeId == PrivilegesPageType.DynamicPage && x.PageRouteId == null);
                    var canView = dynamicPageGlobal.CanApprove;

                    _db.BEUsersPrivileges.Add(new BEUsersPrivileges()
                    {
                        ApplicationUserId = item.Id,
                        PageRouteId = pageId,
                        PageTypeId = PrivilegesPageType.DynamicPage,
                        CanView = canView
                    });
                }
            }
            else
            {
                var userPriviliges = _db.BEUsersPrivileges.Where(x => x.PageRouteId == pageId);

                _db.RemoveRange(userPriviliges);
            }

            _db.SaveChanges();

        }

        public Dictionary<PrivilegesPageType, string> GetHomePageSectionsNames()
        {
            var HPNames = new Dictionary<PrivilegesPageType, string>();

            var logoLinks = "Logo Links";
            HPNames.Add(PrivilegesPageType.HPLogoLinks, logoLinks);

            var basicInfo = "Basic Info";
            HPNames.Add(PrivilegesPageType.HPBasicInfo, basicInfo);

            var photos = "Photos";
            HPNames.Add(PrivilegesPageType.HPPhotos, photos);

            var photosSlider = "Photos Slider";
            HPNames.Add(PrivilegesPageType.HpPhotoSlider, photosSlider);

            var video = "Video";
            HPNames.Add(PrivilegesPageType.HPVideo, video);

            var ministryMessage = _db.MinistryVissions.FirstOrDefault().EnTitle;
            HPNames.Add(PrivilegesPageType.HPMinistryMessage, ministryMessage);

            var publications = _db.Publications.FirstOrDefault().EnMainTitle;
            HPNames.Add(PrivilegesPageType.HPPublications, publications);

            var economicDevelopments = _db.EconomicDevelopments.FirstOrDefault().EnMainTitle;
            HPNames.Add(PrivilegesPageType.HPEconomicDevelopment, economicDevelopments);

            var monitoring = _db.Monitoring.FirstOrDefault().EnMainTitle;
            HPNames.Add(PrivilegesPageType.HPMonitoringAndPlanning, monitoring);

            var citizenPlan = _db.CitizenPlan.FirstOrDefault().EnMainTitle;
            HPNames.Add(PrivilegesPageType.HPCitizenPlan, citizenPlan);

            var affiliates = _db.HomePageAffiliates.FirstOrDefault(x => x.Type == Data.HomePageModels.AffiliatesType.Title).EnDescription;
            HPNames.Add(PrivilegesPageType.HPAffiliates, affiliates);

            return HPNames;
        }
    }
}
