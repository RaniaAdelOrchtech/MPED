using Microsoft.AspNetCore.Razor.TagHelpers;
using MPMAR.Business.Interfaces;
using MPMAR.Data;
using MPMAR.Data.Enums;
using MPMAR.Web.Admin.Const;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPMAR.Web.Admin.TagHelpers
{
    public class PrivilegeTagHelper : TagHelper
    {


        [HtmlAttributeName("page-type-id")]
        public PrivilegesPageType PageTypeId { get; set; }
        [HtmlAttributeName("user-privileges")]
        public List<BEUsersPrivileges> UserPrivileges { get; set; }
        [HtmlAttributeName("sp-controller-name")]
        public string SPControllerName { get; set; }
        [HtmlAttributeName("ul-privilegs")]
        public string UlPrivilegs { get; set; }
        [HtmlAttributeName("is-super-admin")]
        public bool IsSuperAdmin { get; set; }

        /// <summary>
        /// validate if the user has the privilege to access a specific list or list item
        /// </summary>
        /// <param name="context"></param>
        /// <param name="output"></param>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = null;
            if (!IsSuperAdmin)
            {
                //if the user check for a list
                if (!string.IsNullOrEmpty(UlPrivilegs))
                {
                    switch (UlPrivilegs)
                    {
                        case UlPrivilegsConst.Approval:
                            if (UserPrivileges != null && UserPrivileges.Any(x => x.CanApprove))
                            {
                                return;
                            }
                            output.SuppressOutput();
                            return;
                        case UlPrivilegsConst.HomePage:
                            if (UserPrivileges != null && UserPrivileges.Any(x => x.PageTypeId >= PrivilegesPageType.HPBasicInfo && x.PageTypeId <= PrivilegesPageType.HPAffiliates && x.CanView))
                            {
                                return;
                            }
                            output.SuppressOutput();
                            return;
                        case UlPrivilegsConst.Definitions:
                            if (UserPrivileges != null && UserPrivileges.Any(x =>
                            (x.PageTypeId == PrivilegesPageType.NewsType ||
                            x.PageTypeId == PrivilegesPageType.DynamicPage ||
                            x.PageTypeId == PrivilegesPageType.StaticPage ||
                            x.PageTypeId == PrivilegesPageType.LeftMenuItems ||
                            x.PageTypeId == PrivilegesPageType.FooterMenuItems ||
                            x.PageTypeId == PrivilegesPageType.FooterMenuTitles ||
                            x.PageTypeId == PrivilegesPageType.NavItems)
                            && x.CanView))
                            {
                                return;
                            }
                            output.SuppressOutput();
                            return;
                        case UlPrivilegsConst.EconomicIndicators:
                            if (UserPrivileges != null && UserPrivileges.Any(x =>
                            (x.PageTypeId == PrivilegesPageType.ComponentConstant ||
                            x.PageTypeId == PrivilegesPageType.ComponentCurrent ||
                            x.PageTypeId == PrivilegesPageType.ActivityCurrent ||
                            x.PageTypeId == PrivilegesPageType.SectorGrowthRates ||
                            x.PageTypeId == PrivilegesPageType.RGDP ||
                            x.PageTypeId == PrivilegesPageType.Investment ||
                            x.PageTypeId == PrivilegesPageType.Governorate)
                            && x.CanView))
                            {
                                return;
                            }
                            output.SuppressOutput();
                            return;
                        case UlPrivilegsConst.Component:
                            if (UserPrivileges != null && UserPrivileges.Any(x =>
                            (x.PageTypeId == PrivilegesPageType.ComponentConstant ||
                            x.PageTypeId == PrivilegesPageType.ComponentCurrent) &&
                            x.CanView))
                            {
                                return;
                            }
                            output.SuppressOutput();
                            return;
                        case UlPrivilegsConst.Activity:
                            if (UserPrivileges != null && UserPrivileges.Any(x =>
                            (x.PageTypeId == PrivilegesPageType.ActivityCurrent ||
                            x.PageTypeId == PrivilegesPageType.SectorGrowthRates) &&
                            x.CanView))
                            {
                                return;
                            }
                            output.SuppressOutput();
                            return;
                    }
                }
                //if the user check for controller
                if (!string.IsNullOrEmpty(SPControllerName))
                {
                    if (UserPrivileges != null && UserPrivileges.Any(x => x.PageRoute?.ControllerName?.ToLower().Trim() == SPControllerName.ToLower().Trim() && x.PageTypeId == PageTypeId && x.CanView))
                    {
                        return;
                    }

                }
                else
                {
                    if (UserPrivileges != null && UserPrivileges.Any(x => x.PageTypeId == PageTypeId && x.CanView))
                    {
                        return;
                    }
                }

                output.SuppressOutput();
            }
            return;
        }


    }
}
