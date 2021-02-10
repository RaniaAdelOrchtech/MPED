using MPMAR.Business.ViewModels;
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
    public interface IBEUsersPrivilegesService
    {
        /// <summary>
        /// validate BE Users Privilege for a specific user
        /// </summary>
        /// <param name="bEUsersPrivilegesRequirementModel"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        bool ValidateIBEUsersPrivilegesService(BEUsersPrivilegesRequirementModel bEUsersPrivilegesRequirementModel, string userId);
        /// <summary>
        /// validate BE Users Privilege for a specific user in views
        /// </summary>
        /// <param name="pageTyp"></param>
        /// <param name="userId"></param>
        /// <param name="isSuperAdmin"></param>
        /// <param name="pageId"></param>
        /// <param name="approvalId"></param>
        /// <returns></returns>

        BEUsersPrivilegesViewModel ValidateIBEUsersPrivilegesViewService(PrivilegesPageType pageTyp, string userId, bool isSuperAdmin, int pageId = -1, int approvalId = 0);
        /// <summary>
        /// filter static pages according to the privileges that the given user have
        /// </summary>
        /// <param name="pageRoutes"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        List<PageRouteListViewModel> FilterStaticPages(List<PageRouteListViewModel> pageRoutes, string userId);
        /// <summary>
        /// filter approval pages according to the privileges that the given user have
        /// </summary>
        /// <param name="approvalNotifications"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        List<ApprovalNotification> FilterApprovalPages(List<ApprovalNotification> approvalNotifications, string userId);
        /// <summary>
        /// filter dynamic pages according to the privileges that the given user have
        /// </summary>
        /// <param name="pageRouteViewModels"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        List<PageRouteListViewModel> FilterDynamicPages(List<PageRouteListViewModel> pageRouteViewModels, string id);
        /// <summary>
        /// update dynamic page privileges for all users
        /// </summary>
        /// <param name="pageId"></param>
        /// <param name="isRemove">true in case of adding a new DP false otherwise</param>
        void UpdateWithNewDynamicPages(int pageId, bool isRemove = false);
    }
}
