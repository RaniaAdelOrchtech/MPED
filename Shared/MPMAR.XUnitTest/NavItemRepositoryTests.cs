using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using MPMAR.Business;
using MPMAR.Data;
using MPMAR.Web.Admin.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using System.Linq;
using MPMAR.Business.Interfaces;
using static MPMAR.Data.Enums.Enums;

namespace MPMAR.XUnitTest
{
    public class NavItemRepositoryTests
    {
        public NavItemRepositoryTests()
        {
            MockNavItemRepository = MockData.CreateMockNavItemRepository();
        }

        public readonly INavItemRepository MockNavItemRepository;

        [Fact]
        public void NavItem_AddNavItem_ReturnsNewAddedNavItem()
        {
            int maxId = MockNavItemRepository.Get().Max(x => x.Id) + 1;
            // Create a new navItem
            NavItem navItem = new NavItem
            {
                ArName = "عنصر قائمة جديد" + " " + maxId,
                EnName = "New Nav Item" + " " + maxId,
                Order = maxId,
                CreationDate = DateTime.Now.AddDays(-1),
                CreatedById = new Guid().ToString(),
                IsActive = true,
                IsDeleted = false
            };

            int navItemCountBeforeAdding = MockNavItemRepository.Get().Count();
            int expectedId = MockNavItemRepository.Get().Max(x => x.Id) + 1;

            // try saving our new navItem
            MockNavItemRepository.Add(navItem);

            // demand a recount
            int navItemCountAfterAdding = MockNavItemRepository.Get().Count();
            Assert.Equal(navItemCountBeforeAdding + 1, navItemCountAfterAdding); // Verify the expected Number post-insert

            // verify that our new navItem has been saved
            NavItem testNavItem = MockNavItemRepository.Get(expectedId);
            Assert.NotNull(testNavItem); // Test if null
            Assert.IsType<NavItem>(testNavItem); // Test type
            Assert.Equal(expectedId, testNavItem.Id); // Verify it has the expected navItemId
        }

        [Fact]
        public void NavItem_UpdateNavItem_ReturnsEditedNavItem()
        {
            // Get navItem to update
            NavItem navItem = MockNavItemRepository.Get(1);

            string expectedNavItemEnName = "Edit New Nav Item 1";
            navItem.EnName = expectedNavItemEnName;

            // try updating our new navItem
            MockNavItemRepository.Update(navItem);

            // verify that our navItem has been updated
            NavItem testNavItem = MockNavItemRepository.Get(1);
            Assert.NotNull(testNavItem); // Test if null
            Assert.IsType<NavItem>(testNavItem); // Test type
            Assert.Equal(expectedNavItemEnName, testNavItem.EnName); // Verify it has the expected navItemId
        }

        [Fact]
        public void NavItem_DeleteNavItem_ReturnsDeletedNavItem()
        {
            // Get navItem to delete
            NavItem navItemToDelete = MockNavItemRepository.Get(3);

            // try deleting our new navItem
            NavItem expectedNavItem = MockNavItemRepository.Delete(navItemToDelete.Id);

            // verify that our navItem has been deleted
            NavItem testNavItem = MockNavItemRepository.Get(3);
            Assert.Null(testNavItem);
            Assert.IsType<NavItem>(expectedNavItem); // Test type
            Assert.Equal(expectedNavItem.Id, navItemToDelete.Id); // Verify it has the expected navItemId
        }

        [Fact]
        public void NavItem_GetNavItemWithNotExistId_ReturnsNull()
        {
            // Get navItem with not exist Id
            NavItem expectedNavItem = MockNavItemRepository.Get(100);

            // verify that our navItem is null
            Assert.Null(expectedNavItem); // Test if null
        }
    }
}
