using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MPMAR.Business.Interfaces;
using MPMAR.Common;
using MPMAR.Data.Consts;
using MPMAR.Data.Enums;
using MPMAR.Web.Admin.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using static MPMAR.Web.Admin.AuthRequirement.BEUsersPrivilegesRequirementAttribute;

namespace MPMAR.Web.Admin.AuthHandler
{
    public class BEUsersPrivilegesRequirementFilter : IAuthorizationFilter
    {
        private readonly BEUsersPrivilegesRequirementModel _bEUsersPrivilegesRequirementModel;
        private readonly IBEUsersPrivilegesService _bEUsersPrivilegesService;
        private readonly IPageRouteVersionRepository _pageRouteVersionRepository;

        public BEUsersPrivilegesRequirementFilter(BEUsersPrivilegesRequirementModel bEUsersPrivilegesRequirementModel, IBEUsersPrivilegesService bEUsersPrivilegesService, IPageRouteVersionRepository pageRouteVersionRepository)
        {
            _bEUsersPrivilegesRequirementModel = bEUsersPrivilegesRequirementModel;
            _bEUsersPrivilegesService = bEUsersPrivilegesService;
            _pageRouteVersionRepository = pageRouteVersionRepository;
        }

        /// <summary>
        /// check user privileges for all actions and allow only who has the permission to access the action
        /// </summary>
        /// <param name="context"></param>
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;
            if (user.IsInRole(UserRolesConst.SuperAdmin))
            {
                return;
            }
            var bEUsersPrivilegesRequirementModel = new BEUsersPrivilegesRequirementModel(_bEUsersPrivilegesRequirementModel.PageType, _bEUsersPrivilegesRequirementModel.PageActions, _bEUsersPrivilegesRequirementModel.PageId);

            DynamicPageSectionCheck(context, bEUsersPrivilegesRequirementModel);
            PageMinistryCheck(context, bEUsersPrivilegesRequirementModel);
            EconomicIndicatorCheck(context, bEUsersPrivilegesRequirementModel);


            if (!_bEUsersPrivilegesService.ValidateIBEUsersPrivilegesService(bEUsersPrivilegesRequirementModel, user.FindFirstValue(ClaimTypes.NameIdentifier)))
                context.Result = new ForbidResult();
        }
        /// <summary>
        /// special check for dynamic page section as it depends on page Route Version Id
        /// </summary>
        /// <param name="context"></param>
        /// <param name="bEUsersPrivilegesRequirementModel"></param>
        private void DynamicPageSectionCheck(AuthorizationFilterContext context, BEUsersPrivilegesRequirementModel bEUsersPrivilegesRequirementModel)
        {
            if (_bEUsersPrivilegesRequirementModel.PageType == PrivilegesPageType.DynamicPageSection)
            {
                var pageRouteVersionId = context.HttpContext.Request.Query["pageRouteVersionId"];
                var pageRouteVersionIdInt = 0;
                if (!string.IsNullOrWhiteSpace(pageRouteVersionId))
                {
                    pageRouteVersionIdInt = int.Parse(pageRouteVersionId[0]);
                }
                bEUsersPrivilegesRequirementModel.PageType = PrivilegesPageType.DynamicPage;

                var pageRouteVersion = _pageRouteVersionRepository.GetById(pageRouteVersionIdInt);

                if (pageRouteVersion != null && pageRouteVersion.PageRouteId != null)
                {
                    bEUsersPrivilegesRequirementModel.PageId = pageRouteVersion.PageRouteId;
                }
                else
                {
                    bEUsersPrivilegesRequirementModel.PageId = null;
                }
            }
        }
        /// <summary>
        /// special check for Page Ministry (ministry vision, ministry mission, ministry speech) as it depends on page Route 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="bEUsersPrivilegesRequirementModel"></param>
        private void PageMinistryCheck(AuthorizationFilterContext context, BEUsersPrivilegesRequirementModel bEUsersPrivilegesRequirementModel)
        {
            if (_bEUsersPrivilegesRequirementModel.PageType == PrivilegesPageType.PageMinistry)
            {
                var pageRouteId = context.HttpContext.Request.Query["pageRouteId"];
                var pageRouteIdInt = 0;
                if (!string.IsNullOrWhiteSpace(pageRouteId))
                {
                    pageRouteIdInt = int.Parse(pageRouteId[0]);
                }
                bEUsersPrivilegesRequirementModel.PageType = PrivilegesPageType.StaticPage;
                bEUsersPrivilegesRequirementModel.PageId = pageRouteIdInt;
            }
        }
        /// <summary>
        /// special check for Economic Indicators as it depends on sheet Type
        /// </summary>
        /// <param name="context"></param>
        /// <param name="bEUsersPrivilegesRequirementModel"></param>
        private void EconomicIndicatorCheck(AuthorizationFilterContext context, BEUsersPrivilegesRequirementModel bEUsersPrivilegesRequirementModel)
        {
            if (_bEUsersPrivilegesRequirementModel.PageType == PrivilegesPageType.EconomicIndicator)
            {
                var sheetType = context.HttpContext.Request.Query["sheetType"];
                var sheetTypeInt = 0;
                if (!string.IsNullOrWhiteSpace(sheetType))
                {
                    sheetTypeInt = int.Parse(sheetType[0]);
                }
                bEUsersPrivilegesRequirementModel.PageType = SheetType_PrivilegeType.SheetType_PrivilegeType_Map.GetValueOrDefault(sheetTypeInt);
            }
        }
    }
}
