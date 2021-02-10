using Microsoft.AspNetCore.Identity;
using MPMAR.Business.Interfaces;
using MPMAR.Business.ViewModels;
using MPMAR.Common;
using MPMAR.Data;
using MPMAR.Data.Enums;
using MPMAR.Web.Admin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static MPMAR.Web.Admin.AuthRequirement.BEUsersPrivilegesRequirementAttribute;

namespace MPMAR.Web.Admin.Services
{
    public class BEUsersPrivilegesService : IBEUsersPrivilegesService
    {
        private readonly IBEUsersPrivilegesRepository _bEUsersPrivilegesRepository;
        private readonly IPageRouteVersionRepository _pageRouteVersionRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public BEUsersPrivilegesService(IBEUsersPrivilegesRepository bEUsersPrivilegesRepository, IPageRouteVersionRepository pageRouteVersionRepository, UserManager<ApplicationUser> userManager)
        {
            _bEUsersPrivilegesRepository = bEUsersPrivilegesRepository;
            _pageRouteVersionRepository = pageRouteVersionRepository;
            _userManager = userManager;
        }

        public bool ValidateIBEUsersPrivilegesService(BEUsersPrivilegesRequirementModel bEUsersPrivilegesRequirementModel, string userId)
        {

            var bEUsersPrivileges = _bEUsersPrivilegesRepository.GetUserPrivileges(userId);

            return ValidatePrivileges(bEUsersPrivilegesRequirementModel, bEUsersPrivileges);

        }

        private bool ValidatePrivileges(BEUsersPrivilegesRequirementModel bEUsersPrivilegesRequirementModel, IEnumerable<BEUsersPrivileges> bEUsersPrivileges)
        {
            BEUsersPrivileges bEUsersPrivilegesCurrentModel;
            //check if the user can approve any page to access the approval notifications
            if (bEUsersPrivilegesRequirementModel.PageType == PrivilegesPageType.Approval)
            {
                return bEUsersPrivileges.Any(x => x.CanApprove);
            }

            if (bEUsersPrivilegesRequirementModel.PageId == -1)
            {
                bEUsersPrivilegesRequirementModel.PageId = null;
            }

            //check if the user can access any static page
            if (bEUsersPrivilegesRequirementModel.PageType == PrivilegesPageType.StaticPage && bEUsersPrivilegesRequirementModel.PageId == null && bEUsersPrivilegesRequirementModel.PageActions.Contains(PrivilegesActions.CanView))
            {
                return bEUsersPrivileges.Any(x => x.PageTypeId == PrivilegesPageType.StaticPage && x.CanView);

            }
            //check if the user can access any dynamic page
            if (bEUsersPrivilegesRequirementModel.PageType == PrivilegesPageType.DynamicPage && bEUsersPrivilegesRequirementModel.PageId == null && bEUsersPrivilegesRequirementModel.PageActions.Contains(PrivilegesActions.CanView))
            {
                return bEUsersPrivileges.Any(x => x.PageTypeId == PrivilegesPageType.DynamicPage && x.CanView);

            }

            //check if the user can access dynamic page details
            if (bEUsersPrivilegesRequirementModel.PageType == PrivilegesPageType.DynamicPage && bEUsersPrivilegesRequirementModel.PageId == null && bEUsersPrivilegesRequirementModel.PageActions.Contains(PrivilegesActions.CanViewDP_BI))
            {
                return bEUsersPrivileges.Any(x => x.PageTypeId == PrivilegesPageType.DynamicPage && x.PageRouteId == null && x.CanView);
            }

            //check if the user can access static page details
            if (bEUsersPrivilegesRequirementModel.PageType == PrivilegesPageType.StaticPage && bEUsersPrivilegesRequirementModel.PageId == null && bEUsersPrivilegesRequirementModel.PageActions.Contains(PrivilegesActions.CanViewSP_BI))
            {
                return bEUsersPrivileges.Any(x => x.PageTypeId == PrivilegesPageType.StaticPage && x.PageRouteId == null && x.CanView);
            }


            if (bEUsersPrivilegesRequirementModel.PageId != null)
            {

                bEUsersPrivilegesCurrentModel = bEUsersPrivileges.FirstOrDefault(x => x.PageTypeId == bEUsersPrivilegesRequirementModel.PageType && x.PageRouteId == bEUsersPrivilegesRequirementModel.PageId);
            }
            else
            {
                bEUsersPrivilegesCurrentModel = bEUsersPrivileges.FirstOrDefault(x => x.PageTypeId == bEUsersPrivilegesRequirementModel.PageType && x.PageRouteId == null);

            }
            var isAuthorized = false;

            //check if the user has any permission that the action allow
            foreach (var item in bEUsersPrivilegesRequirementModel.PageActions)
            {

                switch (item)
                {
                    case PrivilegesActions.CanView:
                        isAuthorized = bEUsersPrivilegesCurrentModel.CanView;
                        break;
                    case PrivilegesActions.CanEdit:
                        isAuthorized = bEUsersPrivilegesCurrentModel.CanEdit;
                        break;
                    case PrivilegesActions.CanDelete:
                        isAuthorized = bEUsersPrivilegesCurrentModel.CanDelete;
                        break;
                    case PrivilegesActions.CanApprove:
                        isAuthorized = bEUsersPrivilegesCurrentModel.CanApprove;
                        break;
                    case PrivilegesActions.CanAdd:
                        isAuthorized = bEUsersPrivilegesCurrentModel.CanAdd;
                        break;
                }
                if (isAuthorized)
                    break;
            }
            return isAuthorized;
        }

        public BEUsersPrivilegesViewModel ValidateIBEUsersPrivilegesViewService(PrivilegesPageType pageTyp, string userId, bool isSuperAdmin, int pageId = -1, int approvalId = 0)
        {
            var bEUsersPrivilegesViewModel = new BEUsersPrivilegesViewModel();

            var userPrivileges = _bEUsersPrivilegesRepository.GetUserPrivileges(userId);


            if (pageTyp == PrivilegesPageType.DynamicPage && pageId != -1)
            {
                var pageRouteVersion = _pageRouteVersionRepository.GetById(pageId);
                if (pageRouteVersion != null && pageRouteVersion.PageRouteId != null)
                {
                    pageId = pageRouteVersion.PageRouteId ?? 0;
                }
                else
                {
                    pageId = -1;
                }
            }


            bEUsersPrivilegesViewModel.CanAdd = isSuperAdmin || ValidatePrivileges(new BEUsersPrivilegesRequirementModel(pageTyp, new PrivilegesActions[] { PrivilegesActions.CanAdd }, pageId), userPrivileges);

            bEUsersPrivilegesViewModel.CanEdit = isSuperAdmin || ValidatePrivileges(new BEUsersPrivilegesRequirementModel(pageTyp, new PrivilegesActions[] { PrivilegesActions.CanEdit }, pageId), userPrivileges);

            bEUsersPrivilegesViewModel.CanDelete = isSuperAdmin || ValidatePrivileges(new BEUsersPrivilegesRequirementModel(pageTyp, new PrivilegesActions[] { PrivilegesActions.CanDelete }, pageId), userPrivileges);

            bEUsersPrivilegesViewModel.CanApprove = isSuperAdmin || ValidatePrivileges(new BEUsersPrivilegesRequirementModel(pageTyp, new PrivilegesActions[] { PrivilegesActions.CanApprove }, pageId), userPrivileges);


            bEUsersPrivilegesViewModel.CanSubmit = isSuperAdmin || ValidatePrivileges(new BEUsersPrivilegesRequirementModel(pageTyp, new PrivilegesActions[] { PrivilegesActions.CanAdd, PrivilegesActions.CanEdit, PrivilegesActions.CanDelete }, pageId), userPrivileges);


            bEUsersPrivilegesViewModel.IsFromApprovalPage = approvalId > 0;

            return bEUsersPrivilegesViewModel;
        }


        public List<PageRouteListViewModel> FilterStaticPages(List<PageRouteListViewModel> pageRoutes, string userId)
        {
            var pageRouteListViewModels = new List<PageRouteListViewModel>();
            var bEUsersPrivileges = _bEUsersPrivilegesRepository.GetUserPrivileges(userId).Where(x => x.PageTypeId == PrivilegesPageType.StaticPage);
            var canViewSP_BI = bEUsersPrivileges.FirstOrDefault(x => x.PageRouteId == null)?.CanView;
            var user = _userManager.FindByIdAsync(userId).Result;
            var roles = _userManager.GetRolesAsync(user).Result;
            if (bEUsersPrivileges.Any() && !roles.Contains(UserRolesConst.SuperAdmin))
            {
                foreach (var item in pageRoutes)
                {
                    item.CanViewDP_BI = canViewSP_BI ?? false;
                    if (bEUsersPrivileges.Any(x => x.PageRouteId == item.Id && x.CanView))
                    {
                        pageRouteListViewModels.Add(item);
                    }
                    else
                    {
                        item.IsAvailable = false;
                    }
                }
                if (bEUsersPrivileges.FirstOrDefault(x => x.PageRouteId == null && x.CanView) != null)
                    return pageRoutes;

                return pageRouteListViewModels;
            }
            else
            {
                return pageRoutes;
            }
        }

        public List<ApprovalNotification> FilterApprovalPages(List<ApprovalNotification> allApprovalNotifications, string userId)
        {
            var filterdApprovalNotifications = new List<ApprovalNotification>();
            var bEUsersPrivileges = _bEUsersPrivilegesRepository.GetUserPrivileges(userId);
            var user = _userManager.FindByIdAsync(userId).Result;
            var roles = _userManager.GetRolesAsync(user).Result;
            if (bEUsersPrivileges.Any() && !roles.Contains(UserRolesConst.SuperAdmin))
            {
                //static pages basic info
                if (bEUsersPrivileges.FirstOrDefault(x => x.PageTypeId == PrivilegesPageType.StaticPage && x.PageRouteId == null && x.CanApprove) != null)
                {
                    filterdApprovalNotifications = allApprovalNotifications.Where(x => x.PageType == PageType.Static && x.ChangeType == ChangeType.BasicInfo).ToList();

                }

                //Dynamic pages
                if (bEUsersPrivileges.FirstOrDefault(x => x.PageTypeId == PrivilegesPageType.DynamicPage && x.PageRouteId == null && x.CanApprove) != null)
                {
                    filterdApprovalNotifications.AddRange(allApprovalNotifications.Where(x => x.PageType == PageType.Dynamic).ToList());

                }

                //reset of pages
                foreach (var item in allApprovalNotifications)
                {
                    var data = bEUsersPrivileges.FirstOrDefault(x => x.PageName == item.PageName && x.CanApprove && !(item.ChangeType == ChangeType.BasicInfo && item.PageType == PageType.Static));
                    if (data != null)
                        filterdApprovalNotifications.Add(item);
                }


                return filterdApprovalNotifications;
            }
            else
            {
                return allApprovalNotifications;
            }
        }

        public List<PageRouteListViewModel> FilterDynamicPages(List<PageRouteListViewModel> pageRoutes, string userId)
        {
            var pageRouteListViewModels = new List<PageRouteListViewModel>();
            var bEUsersPrivileges = _bEUsersPrivilegesRepository.GetUserPrivileges(userId).Where(x => x.PageTypeId == PrivilegesPageType.DynamicPage);
            var canViewDP_BI = bEUsersPrivileges.FirstOrDefault(x => x.PageRouteId == null)?.CanView;


            var user = _userManager.FindByIdAsync(userId).Result;
            var roles = _userManager.GetRolesAsync(user).Result;

            if (bEUsersPrivileges.Any() && !roles.Contains(UserRolesConst.SuperAdmin))
            {
                foreach (var item in pageRoutes)
                {
                    item.CanViewDP_BI = canViewDP_BI ?? false;
                    item.IsApplyable = bEUsersPrivileges.Any(x => item.Id != null && x.PageRouteId == item.Id && (x.CanEdit || x.CanDelete));

                    if (bEUsersPrivileges.Any(x => item.Id != null && x.PageRouteId == item.Id && x.CanView))
                    {
                        pageRouteListViewModels.Add(item);
                    }
                    else
                    {
                        item.IsAvailable = false;
                    }
                }
                if (bEUsersPrivileges.FirstOrDefault(x => x.PageRouteId == null && x.CanView) != null)
                    return pageRoutes;

                return pageRouteListViewModels;
            }
            else
            {
                return pageRoutes;
            }
        }

        public void UpdateWithNewDynamicPages(int pageId, bool isRemove = false)
        {
            _bEUsersPrivilegesRepository.UpdateWithNewDynamicPages(pageId, isRemove);
        }
    }
}
