using System;
using System.Collections.Generic;
using System.Text;
using static MPMAR.Data.Enums.Enums;

namespace MPMAR.Data.Helpers
{
    public class InitialData
    {
        public static List<NavItemVersion> GetNavItemVersions()
        {
            return new List<NavItemVersion>
            {
                new NavItemVersion
                {
                    ArName = "عن الوزارة",
                    EnName = "About Minstry",
                    Order = 1,
                    ParentNavItemId = null,
                    IsActive = true,
                    IsDeleted = false,
                    CreationDate = DateTime.Now,
                },
                new NavItemVersion
                {
                    ArName = "التخطيط والمتابعة",
                    EnName = "Planning and Follow-up",
                    Order = 2,
                    ParentNavItemId = null,
                    IsActive = true,
                    IsDeleted = false,
                    CreationDate = DateTime.Now,
                },
                new NavItemVersion
                {
                    ArName = "إصدارات وتقارير",
                    EnName = "Publications and reports",
                    Order = 3,
                    ParentNavItemId = null,
                    IsActive = true,
                    IsDeleted = false,
                    CreationDate = DateTime.Now,
                },
                new NavItemVersion
                {
                    ArName = "التنمية الاقتصادية",
                    EnName = "Economical development",
                    Order = 4,
                    ParentNavItemId = null,
                    IsActive = true,
                    IsDeleted = false,
                    CreationDate = DateTime.Now,
                },
                new NavItemVersion
                {
                    ArName = "الإعلام",
                    EnName = "Media",
                    Order = 5,
                    ParentNavItemId = null,
                    IsActive = true,
                    IsDeleted = false,
                    CreationDate = DateTime.Now,
                },
                new NavItemVersion
                {
                    ArName = "إصدارات خاصة بالوزارة",
                    EnName = "Ministry-specific issues",
                    Order = 1,
                    ParentNavItemId = 3,
                    IsActive = true,
                    IsDeleted = false,
                    CreationDate = DateTime.Now,
                }
            };
        }

        public static List<PageRoute> GetPageRoutes()
        {
            return new List<PageRoute>
            {
                new PageRoute
                {
                    ArName = "الرئيسية",
                    EnName = "Home",
                    ControllerName = "Main",
                    Order = 1,
                    IsActive = true,
                    IsDeleted = false,
                    CreationDate = DateTime.Now,
                    NavItemId = null,
                    HasNavItem = false,
                    IsDynamicPage = false
                },
                new PageRoute
                {
                    ArName = "تواصل معنا",
                    EnName = "Contact Us",
                    ControllerName = "ContactUs",
                    Order = 2,
                    IsActive = true,
                    IsDeleted = false,
                    CreationDate = DateTime.Now,
                    NavItemId = null,
                    HasNavItem = false,
                    IsDynamicPage = false
                },
                new PageRoute
                {
                    ArName = "رؤية الوزارة",
                    EnName = "Ministry Vision",
                    ControllerName = "MinistryVision",
                    Order = 1,
                    IsActive = true,
                    IsDeleted = false,
                    CreationDate = DateTime.Now,
                    NavItemId = 1,
                    HasNavItem = true,
                    IsDynamicPage = false
                },
                new PageRoute
                {
                    ArName = "مهام الوزارة",
                    EnName = "Ministry Mission",
                    ControllerName = "MinistryMission",
                    SectionName = "mission",
                    Order = 2,
                    IsActive = true,
                    IsDeleted = false,
                    CreationDate = DateTime.Now,
                    NavItemId = 1,
                    HasNavItem = true,
                    IsDynamicPage = false
                },
                new PageRoute
                {
                    ArName = "خطاب الوزيرة",
                    EnName = "Minister's Speech",
                    ControllerName = "MinisterSpeech",
                    SectionName = "speech",
                    Order = 3,
                    IsActive = true,
                    IsDeleted = false,
                    CreationDate = DateTime.Now,
                    NavItemId = 1,
                    HasNavItem = true,
                    IsDynamicPage = false
                },
                new PageRoute
                {
                    ArName = "الهيكل التنظيمي",
                    EnName = "Organizational Structure",
                    ControllerName = "OrganizationalStructure",
                    Order = 4,
                    IsActive = true,
                    IsDeleted = false,
                    CreationDate = DateTime.Now,
                    NavItemId = 1,
                    HasNavItem = true,
                    IsDynamicPage = false
                },
                new PageRoute
                {
                    ArName = "عن الوزيرة",
                    EnName = "About The Minister",
                    ControllerName = "AboutMinister",
                    Order = 5,
                    IsActive = true,
                    IsDeleted = false,
                    CreationDate = DateTime.Now,
                    NavItemId = 1,
                    HasNavItem = true,
                    IsDynamicPage = false
                },
                new PageRoute
                {
                    ArName = "تاريخ الوزارة ووزراء سابقون",
                    EnName = "History Of The Ministry And Former Ministers",
                    ControllerName = "HistoryOfMinistry",
                    Order = 6,
                    IsActive = true,
                    IsDeleted = false,
                    CreationDate = DateTime.Now,
                    NavItemId = 1,
                    HasNavItem = true,
                    IsDynamicPage = false
                },

                new PageRoute
                {
                    ArName = "رؤية مصر 2030",
                    EnName = "Egypt Vision 2030",
                    ControllerName = "EgyptVision",
                    Order = 1,
                    IsActive = true,
                    IsDeleted = false,
                    CreationDate = DateTime.Now,
                    NavItemId = 2,
                    HasNavItem = true,
                    IsDynamicPage = false
                },
                new PageRoute
                {
                    ArName = "برامج وخطط",
                    EnName = "Programs And Plans",
                    ControllerName = "ProgramsAndPlans",
                    Order = 2,
                    IsActive = true,
                    IsDeleted = false,
                    CreationDate = DateTime.Now,
                    NavItemId = 2,
                    HasNavItem = true,
                    IsDynamicPage = false
                },
                new PageRoute
                {
                    ArName = "منظومة الآداء",
                    EnName = "Performance System",
                    ControllerName = "PerformanceSystem",
                    Order = 3,
                    IsActive = true,
                    IsDeleted = false,
                    CreationDate = DateTime.Now,
                    NavItemId = 2,
                    HasNavItem = true,
                    IsDynamicPage = false
                },
                new PageRoute
                {
                    ArName = "المنظومة المتكاملة لإعداد ومتابعة الخطة الاستثمارية",
                    EnName = "The Integrated System For Preparing And Following Up The Investment Plan",
                    ControllerName = "IntegratedSystem",
                    Order = 4,
                    IsActive = true,
                    IsDeleted = false,
                    CreationDate = DateTime.Now,
                    NavItemId = 2,
                    HasNavItem = true,
                    IsDynamicPage = false
                },

                new PageRoute
                {
                    ArName = "مصر في التقارير الدولية",
                    EnName = "Egypt In International Reports",
                    ControllerName = "EgyptInInternationalReports",
                    Order = 1,
                    IsActive = true,
                    IsDeleted = false,
                    CreationDate = DateTime.Now,
                    NavItemId = 3,
                    HasNavItem = true,
                    IsDynamicPage = false
                },

                new PageRoute
                {
                    ArName = "منظومة الحسابات القومية",
                    EnName = "System Of National Accounts",
                    ControllerName = "SystemOfNationalAccounts",
                    Order = 1,
                    IsActive = true,
                    IsDeleted = false,
                    CreationDate = DateTime.Now,
                    NavItemId = 4,
                    HasNavItem = true,
                    IsDynamicPage = false
                },
                new PageRoute
                {
                    ArName = "المعيار الدولي لنشر البيانات",
                    EnName = "International Standard For Data Dissemination",
                    ControllerName = "InternationalStandard",
                    Order = 2,
                    IsActive = true,
                    IsDeleted = false,
                    CreationDate = DateTime.Now,
                    NavItemId = 4,
                    HasNavItem = true,
                    IsDynamicPage = false
                },
                new PageRoute
                {
                    ArName = "المؤشرات الاقتصادية الكلية",
                    EnName = "Macroeconomic Indicators",
                    ControllerName = "MacroeconomicIndicators",
                    Order = 3,
                    IsActive = true,
                    IsDeleted = false,
                    CreationDate = DateTime.Now,
                    NavItemId = 4,
                    HasNavItem = true,
                    IsDynamicPage = false
                },

                new PageRoute
                {
                    ArName = "بيانات صحفية",
                    EnName = "Press Releases",
                    ControllerName = "PressReleases",
                    Order = 1,
                    IsActive = true,
                    IsDeleted = false,
                    CreationDate = DateTime.Now,
                    NavItemId = 5,
                    HasNavItem = true,
                    IsDynamicPage = false
                },
                new PageRoute
                {
                    ArName = "مكتبة الصور",
                    EnName = "Pictures Library",
                    ControllerName = "PicturesLibrary",
                    Order = 2,
                    IsActive = true,
                    IsDeleted = false,
                    CreationDate = DateTime.Now,
                    NavItemId = 5,
                    HasNavItem = true,
                    IsDynamicPage = false
                },
                new PageRoute
                {
                    ArName = "فعاليات وورش عمل المثال 1",
                    EnName = "Example 1 Events And Workshops",
                    ControllerName = "EventsAndWorkshopsOne",
                    Order = 3,
                    IsActive = true,
                    IsDeleted = false,
                    CreationDate = DateTime.Now,
                    NavItemId = 5,
                    HasNavItem = true,
                    IsDynamicPage = false
                },
                new PageRoute
                {
                    ArName = "فعاليات وورش عمل المثال 2",
                    EnName = "Example 1 Events And Workshops",
                    ControllerName = "EventsAndWorkshopsTwo",
                    Order = 4,
                    IsActive = true,
                    IsDeleted = false,
                    CreationDate = DateTime.Now,
                    NavItemId = 5,
                    HasNavItem = true,
                    IsDynamicPage = false
                }
            };
        }

        public static List<PageSectionType> GetPageSectionTypes()
        {
            return new List<PageSectionType>
            {
                new PageSectionType
                {
                    EnName = "Section 1",
                    ArName = "قسم 1",
                    MediaType = "Image",
                    ThemeImage = null,
                    HasCards = false
                },
                new PageSectionType
                {
                    EnName = "Section 2",
                    ArName = "قسم 2",
                    MediaType = "Video",
                    ThemeImage = null,
                    HasCards = false
                },
                new PageSectionType
                {
                    EnName = "Section 3",
                    ArName = "قسم 3",
                    MediaType = "Image",
                    ThemeImage = null,
                    HasCards = false
                },
                new PageSectionType
                {
                    EnName = "Section 4",
                    ArName = "قسم 4",
                    MediaType = "Image",
                    ThemeImage = null,
                    HasCards = false
                },
                new PageSectionType
                {
                    EnName = "Section 5",
                    ArName = "قسم 5",
                    MediaType = "Video",
                    ThemeImage = null,
                    HasCards = false
                },
                new PageSectionType
                {
                    EnName = "Section 6",
                    ArName = "قسم 6",
                    MediaType = "Image",
                    ThemeImage = null,
                    HasCards = false
                },
                new PageSectionType
                {
                    EnName = "Section 7",
                    ArName = "قسم 7",
                    MediaType = "None",
                    ThemeImage = null,
                    HasCards = false
                },
                new PageSectionType
                {
                    EnName = "Section 8",
                    ArName = "قسم 8",
                    MediaType = "None",
                    ThemeImage = null,
                    HasCards = true
                }
            };
        }

        public static List<Status> GetStatuses()
        {
            return new List<Status>
            {
                new Status
                {
                    Name = "Draft"
                },
                new Status
                {
                    Name = "Submitted"
                },
                new Status
                {
                    Name = "Approved"
                },
                new Status
                {
                    Name = "Ignored"
                }
            };
        }
    }
}
