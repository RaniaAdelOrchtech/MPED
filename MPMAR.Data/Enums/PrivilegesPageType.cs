using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;

namespace MPMAR.Data.Enums
{
    //keep this enum in this order as some fucntions depend on that, if you want to add more add above "approval"
   public enum PrivilegesPageType
    {
        StaticPage,
        DynamicPage,

        [Description("Basic Info")]
        HPBasicInfo,
        [Description("Photos")]
        HPPhotos,
        [Description("Photo Slider")]
        HpPhotoSlider,
        [Description("Video")]
        HPVideo,
        [Description("Logo Links")]
        HPLogoLinks,
        [Description("Ministry Message")]
        HPMinistryMessage,
        [Description("Publications")]
        HPPublications,
        [Description("Economic Development")]
        HPEconomicDevelopment,
        [Description("Monitoring And Planning")]
        HPMonitoringAndPlanning,
        [Description("Citizen Plan")]
        HPCitizenPlan,
        [Description("Affiliates")]
        HPAffiliates,

        [Description("Contact Us")]
        ContactUs,

        [Description("Social MediaLinks")]
        SocialMediaLinks,

        [Description("Nav Items")]
        NavItems,
        [Description("Footer Menu Titles")]
        FooterMenuTitles,
        [Description("Footer Menu Items")]
        FooterMenuItems,
        [Description("Left Menu Items")]
        LeftMenuItems,
        [Description("News Types")]
        NewsType,

        [Description("Component Constant")]
        ComponentConstant,
        [Description("Component Current")]
        ComponentCurrent,
        [Description("Activity Current")]
        ActivityCurrent,
        [Description("Sector Growth Rate")]
        SectorGrowthRates,
        [Description("RGDP")]
        RGDP,
        [Description("Investment")]
        Investment,
        [Description("Governorate")]
        Governorate,

        Approval,
        DynamicPageSection,
        PageMinistry,
        EconomicIndicator



    }
    public static class PrivilegesPageTypeDescription
    {
        public static string GetDescription(this PrivilegesPageType GenericEnum)
        {
            Type genericEnumType = GenericEnum.GetType();
            MemberInfo[] memberInfo = genericEnumType.GetMember(GenericEnum.ToString());
            if ((memberInfo.Length <= 0)) return GenericEnum.ToString();
            object[] attribs = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attribs.Any() ? ((DescriptionAttribute)attribs.ElementAt(0)).Description : GenericEnum.ToString();
        }
    }
 
}
