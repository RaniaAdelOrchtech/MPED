using Moq;
using MPMAR.Business;
using MPMAR.Business.Interfaces;
using MPMAR.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static MPMAR.Data.Enums.Enums;

namespace MPMAR.XUnitTest
{
    class MockData
    {
        public static List<NavItem> navItems = new List<NavItem>
            {
                new NavItem
                {
                    Id = 1,
                    ArName = "عنصر قائمة جديد 1",
                    EnName = "New Nav Item 1",
                    Order = 1,
                    CreationDate = DateTime.Now.AddDays(-3),
                    CreatedById = new Guid().ToString(),
                    IsActive = true,
                    IsDeleted = false
                },
                new NavItem
                {
                    Id = 2,
                    ArName = "عنصر قائمة جديد 2",
                    EnName = "New Nav Item 2",
                    Order = 2,
                    CreationDate = DateTime.Now.AddDays(-2),
                    CreatedById = new Guid().ToString(),
                    IsActive = true,
                    IsDeleted = false
                },
                new NavItem
                {
                    Id = 3,
                    ArName = "عنصر قائمة جديد 3",
                    EnName = "New Nav Item",
                    Order = 3,
                    CreationDate = DateTime.Now.AddDays(-1),
                    CreatedById = new Guid().ToString(),
                    IsActive = true,
                    IsDeleted = false
                }
            };

        public static INavItemRepository CreateMockNavItemRepository()
        {
            Mock<INavItemRepository> mockNavItemRepository = new Mock<INavItemRepository>();

            mockNavItemRepository.Setup(x => x.Get()).Returns(navItems);

            mockNavItemRepository.Setup(x => x.Get(It.IsAny<int>())).Returns((int i) => navItems.FirstOrDefault(
                x => x.Id == i));

            mockNavItemRepository.Setup(x => x.Update(It.IsAny<NavItem>()));

            mockNavItemRepository.Setup(x => x.Delete(It.IsAny<int>())).Returns((int i) =>
            {
                var navItem = navItems.Where(x => x.Id == i).Single();
                navItems.Remove(navItem);

                return navItem;
            });

            mockNavItemRepository.Setup(mr => mr.Add(It.IsAny<NavItem>())).Returns(
                (NavItem target) =>
                {
                    DateTime now = DateTime.Now;

                    if (target.Id.Equals(default(int)))
                    {
                        target.CreationDate = now;
                        target.Id = navItems.Max(x => x.Id) + 1;
                        navItems.Add(target);
                    }
                    else
                    {
                        var original = navItems.Where(
                            q => q.Id == target.Id).Single();

                        if (original == null)
                        {
                            return null;
                        }

                        original.EnName = target.EnName;
                        original.ArName = target.ArName;
                        original.Order = target.Order;
                        original.IsActive = target.IsActive;
                        original.IsDeleted = target.IsDeleted;
                    }

                    return target;
                });

            return mockNavItemRepository.Object;
        }

        public static List<NavItemVersion> navItemVersions = new List<NavItemVersion>
            {
                new NavItemVersion
                {
                    Id = 1,
                    ArName = "عنصر قائمة جديد 1",
                    EnName = "New Nav Item 1",
                    Order = 1,
                    CreationDate = DateTime.Now.AddDays(-3),
                    CreatedById = new Guid().ToString(),
                    IsActive = true,
                    IsDeleted = false
                },
                new NavItemVersion
                {
                    Id = 2,
                    ArName = "عنصر قائمة جديد 2",
                    EnName = "New Nav Item 2",
                    Order = 2,
                    CreationDate = DateTime.Now.AddDays(-2),
                    CreatedById = new Guid().ToString(),
                    IsActive = true,
                    IsDeleted = false
                },
                new NavItemVersion
                {
                    Id = 3,
                    ArName = "عنصر قائمة جديد 3",
                    EnName = "New Nav Item",
                    Order = 3,
                    CreationDate = DateTime.Now.AddDays(-1),
                    CreatedById = new Guid().ToString(),
                    IsActive = true,
                    IsDeleted = false
                }
            };

        public static INavItemVersionRepository CreateMockNavItemVersionRepository()
        {
            List<NavItemVersion> navItemVersions = MockData.navItemVersions;

            Mock<INavItemVersionRepository> mockNavItemVersionRepository = new Mock<INavItemVersionRepository>();

            mockNavItemVersionRepository.Setup(x => x.Get()).Returns(navItemVersions);

            mockNavItemVersionRepository.Setup(x => x.Get(It.IsAny<int>())).Returns((int i) => navItemVersions.FirstOrDefault(
                x => x.Id == i));

            mockNavItemVersionRepository.Setup(x => x.Update(It.IsAny<NavItemVersion>()));

            mockNavItemVersionRepository.Setup(x => x.Delete(It.IsAny<int>())).Returns((int i) =>
            {
                var navItem = navItemVersions.Where(x => x.Id == i).Single();
                navItemVersions.Remove(navItem);

                return navItem;
            });

            mockNavItemVersionRepository.Setup(mr => mr.Add(It.IsAny<NavItemVersion>())).Returns(
                (NavItemVersion target) =>
                {
                    DateTime now = DateTime.Now;

                    if (target.Id.Equals(default(int)))
                    {
                        target.CreationDate = now;
                        target.Id = navItemVersions.Max(x => x.Id) + 1;
                        navItemVersions.Add(target);
                    }
                    else
                    {
                        var original = navItemVersions.Where(
                            q => q.Id == target.Id).Single();

                        if (original == null)
                        {
                            return null;
                        }

                        original.EnName = target.EnName;
                        original.ArName = target.ArName;
                        original.Order = target.Order;
                        original.IsActive = target.IsActive;
                        original.IsDeleted = target.IsDeleted;
                    }

                    return target;
                });

            return mockNavItemVersionRepository.Object;
        }
    }
}
