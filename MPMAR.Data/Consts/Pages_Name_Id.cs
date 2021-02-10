using MPMAR.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPMAR.Data.Consts
{
    public class Pages_Name_Id
    {
        //todo: add dynaamic, hpbasicinfo, 
        public static readonly Dictionary<int, string> Name_Id = new Dictionary<int, string>() {
            {StaticPagesIdsConst.CitizenPlan,PagesNamesConst.CityPlan },
            {StaticPagesIdsConst.ContactUs,PagesNamesConst.PageContact },
            {StaticPagesIdsConst.EconomicIndicators,PagesNamesConst.EconomicIndicators },
            {StaticPagesIdsConst.EgyptVision2030,PagesNamesConst.EgyptVision },
            {StaticPagesIdsConst.FormerMinistries,PagesNamesConst.FormerMinistries },
            {StaticPagesIdsConst.News,PagesNamesConst.News },
            {StaticPagesIdsConst.PicturesLibrary,PagesNamesConst.PhotoArchive },
            {StaticPagesIdsConst.MinisterySpeech,PagesNamesConst.MinistersSpeech },
            {StaticPagesIdsConst.MinistryMission,PagesNamesConst.MinistryMission },
            {StaticPagesIdsConst.MinistryVision,PagesNamesConst.MinistryVision },
            {(int)PrivilegesPageType.ActivityCurrent,PagesNamesConst.ActivityCurrent },
            {(int)PrivilegesPageType.ComponentConstant,PagesNamesConst.ComponentConst },
            {(int)PrivilegesPageType.ComponentCurrent,PagesNamesConst.ComponentCurrent },
            {(int)PrivilegesPageType.ContactUs,PagesNamesConst.PageContact },
            {(int)PrivilegesPageType.FooterMenuItems,PagesNamesConst.FooterItems },
            {(int)PrivilegesPageType.FooterMenuTitles,PagesNamesConst.FooterMenuTitle },
            {(int)PrivilegesPageType.Governorate,PagesNamesConst.Governorate },
            {(int)PrivilegesPageType.HPAffiliates,PagesNamesConst.HPAffiliates },
            {(int)PrivilegesPageType.HPCitizenPlan,PagesNamesConst.CitizenPlanHP },
            {(int)PrivilegesPageType.HPEconomicDevelopment,PagesNamesConst.EconomicDevelopment },
            {(int)PrivilegesPageType.HPLogoLinks,PagesNamesConst.HPLogoLink },
            {(int)PrivilegesPageType.HPMinistryMessage,PagesNamesConst.MinistryVisionHP },
            {(int)PrivilegesPageType.HPMonitoringAndPlanning,PagesNamesConst.Monitring },
            {(int)PrivilegesPageType.HPPhotos,PagesNamesConst.HPPhotos },
            {(int)PrivilegesPageType.HpPhotoSlider,PagesNamesConst.HPPhotoSlider },
            {(int)PrivilegesPageType.HPPublications,PagesNamesConst.Publication },
            {(int)PrivilegesPageType.HPVideo,PagesNamesConst.HPVideo },
            {(int)PrivilegesPageType.Investment,PagesNamesConst.Investment },
            {(int)PrivilegesPageType.LeftMenuItems,PagesNamesConst.LeftMenuItem },
            {(int)PrivilegesPageType.NavItems,PagesNamesConst.NavItems },
            {(int)PrivilegesPageType.RGDP,PagesNamesConst.RGDP },
            {(int)PrivilegesPageType.SectorGrowthRates,PagesNamesConst.SectorGrowthRate },
            {(int)PrivilegesPageType.SocialMediaLinks,PagesNamesConst.SocialMedia },

        };
    }
}
